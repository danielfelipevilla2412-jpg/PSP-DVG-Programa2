using System.Text;
using Programa2.Presentation;
using Programa2.Services;

namespace Programa2;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "PSP_02 - Code Metrics Engine";

        var ui = new ConsoleUI();
        var analyzer = new CodeAnalyzer();

        while (true)
        {
            ui.ShowMenu();

            var option = ui.RequestOption()?.Trim();

            switch (option)
            {
                case "1":
                case "01":
                    ProcessPath(ui, analyzer, false);
                    break;

                case "2":
                case "02":
                    ProcessPath(ui, analyzer, true);
                    break;

                case null:
                case "3":
                case "03":
                    return;

                default:
                    ui.ShowError("Seleccione una opción válida.");
                    ui.Pause();
                    break;
            }
        }
    }

    private static void ProcessPath(
        ConsoleUI ui,
        CodeAnalyzer analyzer,
        bool directory)
    {
        try
        {
            var path = ui.RequestPath(directory);

            if (path == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(
                    "La ruta no puede estar vacía.");
            }

            if (directory && !Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(
                    "El directorio no existe.");
            }

            if (!directory && !File.Exists(path))
            {
                throw new FileNotFoundException(
                    "El archivo no existe.");
            }

            var result = analyzer.Analyze(path);

            ui.ShowResults(result);
        }
        catch (Exception ex) when (
            ex is IOException
            or UnauthorizedAccessException
            or ArgumentException
            or InvalidOperationException
            or NotSupportedException)
        {
            ui.ShowError(ex.Message);
        }

        ui.Pause();
    }
}
