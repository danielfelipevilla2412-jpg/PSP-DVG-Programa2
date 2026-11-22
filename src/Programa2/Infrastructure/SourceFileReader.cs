namespace Programa2.Infrastructure;

public class SourceFileReader
{
    private static readonly HashSet<string> ExcludedDirectories =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "bin",
            "obj",
            ".git",
            ".vs"
        };

    public List<string> GetSourceFiles(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("La ruta no puede estar vacía.");
        }

        path = Path.GetFullPath(path.Trim().Trim('"'));

        if (File.Exists(path))
        {
            if (!IsSourceFile(path))
            {
                throw new ArgumentException(
                    "El archivo debe ser de código fuente C# no generado.");
            }

            return new List<string> { path };
        }

        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException("La ruta no existe.");
        }

        var files = EnumerateSourceFiles(path)
            .OrderBy(file => file, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (files.Count == 0)
        {
            throw new InvalidOperationException(
                "No se encontraron archivos C# para analizar.");
        }

        return files;
    }

    private IEnumerable<string> EnumerateSourceFiles(string directory)
    {
        var options = new EnumerationOptions
        {
            AttributesToSkip = FileAttributes.ReparsePoint,
            IgnoreInaccessible = false
        };

        foreach (var file in Directory.EnumerateFiles(
                     directory, "*", options))
        {
            if (IsSourceFile(file))
            {
                yield return file;
            }
        }

        foreach (var subdirectory in Directory.EnumerateDirectories(
                     directory, "*", options))
        {
            var name = Path.GetFileName(subdirectory);

            if (ExcludedDirectories.Contains(name))
            {
                continue;
            }

            foreach (var file in EnumerateSourceFiles(subdirectory))
            {
                yield return file;
            }
        }
    }

    private bool IsSourceFile(string path)
    {
        var name = Path.GetFileName(path);

        return name.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)
            && !name.EndsWith(".g.cs", StringComparison.OrdinalIgnoreCase)
            && !name.EndsWith(".g.i.cs", StringComparison.OrdinalIgnoreCase)
            && !name.EndsWith(".generated.cs", StringComparison.OrdinalIgnoreCase)
            && !name.EndsWith(".Designer.cs", StringComparison.OrdinalIgnoreCase);
    }

    public string ReadFile(string path)
    {
        return File.ReadAllText(path);
    }
}
