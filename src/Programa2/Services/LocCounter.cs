using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Programa2.Services;

public class LocCounter
{
    public int CountPhysicalLines(
        SyntaxNode node,
        bool excludeNestedClasses = false)
    {
        var lines = new HashSet<int>();
        var source = node.SyntaxTree.GetText();

        foreach (var token in node.DescendantTokens())
        {
            if (token.IsMissing || token.Span.Length == 0)
            {
                continue;
            }

            if (excludeNestedClasses &&
                token.Parent?.AncestorsAndSelf()
                    .OfType<ClassDeclarationSyntax>()
                    .FirstOrDefault() != node)
            {
                continue;
            }

            int startLine = source.Lines
                .GetLineFromPosition(token.SpanStart).LineNumber;

            int endLine = source.Lines
                .GetLineFromPosition(token.Span.End - 1).LineNumber;

            for (int line = startLine; line <= endLine; line++)
            {
                if (!string.IsNullOrWhiteSpace(
                    source.Lines[line].ToString()))
                {
                    lines.Add(line);
                }
            }
        }

        return lines.Count;
    }

    public int CountLogicalLines(
        SyntaxNode node,
        bool excludeNestedClasses = false)
    {
        var nodes = node.DescendantNodesAndSelf();

        if (excludeNestedClasses)
        {
            nodes = nodes.Where(item =>
                item.AncestorsAndSelf()
                    .OfType<ClassDeclarationSyntax>()
                    .FirstOrDefault() == node);
        }

        return nodes.Count(IsLogicalInstruction);
    }

    private static bool IsLogicalInstruction(SyntaxNode node)
    {
        return node switch
        {
            BaseTypeDeclarationSyntax => true,
            DelegateDeclarationSyntax => true,
            MethodDeclarationSyntax => true,
            ConstructorDeclarationSyntax => true,
            DestructorDeclarationSyntax => true,
            OperatorDeclarationSyntax => true,
            ConversionOperatorDeclarationSyntax => true,
            FieldDeclarationSyntax => true,
            EventFieldDeclarationSyntax => true,
            EventDeclarationSyntax => true,
            PropertyDeclarationSyntax => true,
            IndexerDeclarationSyntax => true,
            LocalFunctionStatementSyntax => true,
            ElseClauseSyntax => true,
            CatchClauseSyntax => true,
            FinallyClauseSyntax => true,
            SwitchLabelSyntax => true,
            BlockSyntax => false,
            EmptyStatementSyntax => false,
            LabeledStatementSyntax => false,
            StatementSyntax => true,
            _ => false
        };
    }
}
