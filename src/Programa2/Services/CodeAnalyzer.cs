using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Programa2.Infrastructure;
using Programa2.Models;

namespace Programa2.Services;

public class CodeAnalyzer
{
    private readonly SourceFileReader _reader = new();
    private readonly LocCounter _counter = new();

    public MetricsResult Analyze(string path)
    {
        var files = _reader.GetSourceFiles(path);
        var result = new MetricsResult();
        var classes = new Dictionary<string, ClassMetrics>(
            StringComparer.Ordinal);

        foreach (var file in files)
        {
            var content = _reader.ReadFile(file);
            var tree = CSharpSyntaxTree.ParseText(content, path: file);
            var root = tree.GetRoot();

            ValidateSyntax(tree, file);

            result.FileCount++;
            result.TotalPhysicalLines +=
                _counter.CountPhysicalLines(root);

            result.TotalLogicalLines +=
                _counter.CountLogicalLines(root);

            foreach (var declaration in root.DescendantNodes()
                         .OfType<ClassDeclarationSyntax>())
            {
                var name = GetClassName(declaration);

                if (!classes.TryGetValue(name, out var metrics))
                {
                    metrics = new ClassMetrics
                    {
                        Name = name
                    };

                    classes.Add(name, metrics);
                }

                metrics.MethodCount += declaration.Members.Count(
                    member => member is MethodDeclarationSyntax
                        or ConstructorDeclarationSyntax);

                metrics.PhysicalLines +=
                    _counter.CountPhysicalLines(declaration, true);

                metrics.LogicalLines +=
                    _counter.CountLogicalLines(declaration, true);
            }
        }

        result.Classes = classes.Values
            .OrderBy(item => item.Name, StringComparer.Ordinal)
            .ToList();

        return result;
    }

    private static string GetClassName(
        ClassDeclarationSyntax declaration)
    {
        var parts = new List<string>();

        foreach (var node in declaration.AncestorsAndSelf().Reverse())
        {
            if (node is BaseNamespaceDeclarationSyntax ns)
            {
                parts.Add(ns.Name.ToString());
            }
            else if (node is TypeDeclarationSyntax type)
            {
                var name = type.Identifier.ValueText;
                var parameters = type.TypeParameterList?.Parameters.Count ?? 0;

                if (parameters > 0)
                {
                    name += $"`{parameters}";
                }

                parts.Add(name);
            }
        }

        return string.Join(".", parts);
    }

    private static void ValidateSyntax(SyntaxTree tree, string file)
    {
        var error = tree.GetDiagnostics()
            .FirstOrDefault(item =>
                item.Severity == DiagnosticSeverity.Error);

        if (error != null)
        {
            throw new InvalidDataException(
                $"Error de sintaxis en {Path.GetFileName(file)}: {error}");
        }
    }
}
