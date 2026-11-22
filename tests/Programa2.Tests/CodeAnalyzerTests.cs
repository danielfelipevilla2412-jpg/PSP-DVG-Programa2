using Microsoft.CodeAnalysis.CSharp;
using Programa2.Infrastructure;
using Programa2.Services;

namespace Programa2.Tests;

public class CodeAnalyzerTests
{
    private readonly LocCounter _counter = new();

    [Fact]
    public void ContarLineasFisicas_DebeIgnorarComentariosYVacias()
    {
        const string code = """
            // Comentario
            class Ejemplo
            {
                // Otro comentario

                void Ejecutar() { }
            }
            """;

        var root = CSharpSyntaxTree.ParseText(code).GetRoot();

        Assert.Equal(4, _counter.CountPhysicalLines(root));
    }

    [Fact]
    public void ContarLineasLogicas_DebeContarDeclaraciones()
    {
        const string code = """
            class Ejemplo
            {
                void Ejecutar()
                {
                    int numero = 10;
                }
            }
            """;

        var root = CSharpSyntaxTree.ParseText(code).GetRoot();

        Assert.Equal(3, _counter.CountLogicalLines(root));
    }

    [Fact]
    public void ContarLineasLogicas_DebeIgnorarComentarios()
    {
        const string code = """
            // Comentario
            class Ejemplo
            {
                // Comentario interno
            }
            """;

        var root = CSharpSyntaxTree.ParseText(code).GetRoot();

        Assert.Equal(1, _counter.CountLogicalLines(root));
    }

    [Fact]
    public void AnalizarArchivo_DebeIdentificarClaseYMetodo()
    {
        const string code = """
            namespace Prueba;

            public class Calculadora
            {
                public int Sumar(int a, int b)
                {
                    return a + b;
                }
            }
            """;

        WithTempFile(code, path =>
        {
            var result = new CodeAnalyzer().Analyze(path);

            Assert.Equal(1, result.FileCount);
            Assert.Equal(1, result.TotalClasses);
            Assert.Equal(1, result.TotalMethods);
            Assert.Equal("Prueba.Calculadora",
                result.Classes[0].Name);
            Assert.Equal(1, result.Classes[0].MethodCount);
        });
    }

    [Fact]
    public void AnalizarClasesAnidadas_DebeIdentificarlasPorSeparado()
    {
        const string code = """
            class Exterior
            {
                void MetodoExterior() { }

                class Interior
                {
                    void MetodoInterior() { }
                }
            }
            """;

        WithTempFile(code, path =>
        {
            var result = new CodeAnalyzer().Analyze(path);

            Assert.Equal(2, result.TotalClasses);
            Assert.Equal(2, result.TotalMethods);
            Assert.Contains(result.Classes,
                c => c.Name == "Exterior" && c.MethodCount == 1);
            Assert.Contains(result.Classes,
                c => c.Name == "Exterior.Interior"
                    && c.MethodCount == 1);
        });
    }

    [Fact]
    public void AnalizarArchivoConErrorSintactico_DebeRechazarlo()
    {
        WithTempFile("class {", path =>
        {
            Assert.Throws<InvalidDataException>(
                () => new CodeAnalyzer().Analyze(path));
        });
    }

    [Fact]
    public void RutaInexistente_DebeGenerarExcepcion()
    {
        var path = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid():N}.cs");

        Assert.Throws<DirectoryNotFoundException>(
            () => new SourceFileReader().GetSourceFiles(path));
    }

    [Fact]
    public void ArchivoNoCSharp_DebeSerRechazado()
    {
        var path = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid():N}.txt");

        File.WriteAllText(path, "Texto de prueba");

        try
        {
            Assert.Throws<ArgumentException>(
                () => new SourceFileReader().GetSourceFiles(path));
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void RutaVacia_DebeSerRechazada()
    {
        Assert.Throws<ArgumentException>(
            () => new SourceFileReader().GetSourceFiles(" "));
    }

    private static void WithTempFile(
        string content,
        Action<string> test)
    {
        var path = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid():N}.cs");

        File.WriteAllText(path, content);

        try
        {
            test(path);
        }
        finally
        {
            File.Delete(path);
        }
    }
}
