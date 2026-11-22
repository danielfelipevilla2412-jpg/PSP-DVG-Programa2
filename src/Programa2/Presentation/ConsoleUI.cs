using Programa2.Models;

namespace Programa2.Presentation;

public class ConsoleUI
{
    public void ShowMenu()
    {
        Console.Clear();

        WriteStyled(new string('=', 60), ConsoleColor.Red);
        WriteStyled("             PSP_02", ConsoleColor.Red);
        WriteStyled("        CODE METRICS ENGINE", ConsoleColor.White);
        WriteStyled(new string('=', 60), ConsoleColor.Red);

        Console.WriteLine();
        WriteStyled("[01] Analizar archivo", ConsoleColor.White);
        WriteStyled("[02] Analizar directorio", ConsoleColor.White);
        WriteStyled("[03] Salir", ConsoleColor.White);
        Console.WriteLine();

        WriteStyled(new string('-', 60), ConsoleColor.Red);
    }

    public string? RequestOption()
    {
        Console.Write("Seleccione una opción: ");
        return Console.ReadLine();
    }

    public string? RequestPath(bool directory)
    {
        Console.WriteLine();

        Console.Write(directory
            ? "Ingrese la ruta del directorio: "
            : "Ingrese la ruta del archivo .cs: ");

        return Console.ReadLine()?.Trim().Trim('"');
    }

    public void ShowResults(MetricsResult result)
    {
        Console.Clear();

        WriteStyled(new string('=', 72), ConsoleColor.Red);
        WriteStyled("RESULTADOS DEL ANÁLISIS", ConsoleColor.Red);
        WriteStyled(new string('=', 72), ConsoleColor.Red);

        Console.WriteLine($"Archivos analizados: {result.FileCount}");
        Console.WriteLine($"Clases identificadas: {result.TotalClasses}");
        Console.WriteLine($"Métodos identificados: {result.TotalMethods}");
        Console.WriteLine();

        WriteStyled(
            $"{"CLASE",-42} {"MÉTODOS",9} {"LOC",7} {"LLOC",7}",
            ConsoleColor.Red);

        WriteStyled(new string('-', 72), ConsoleColor.DarkGray);

        foreach (var item in result.Classes)
        {
            var name = item.Name;

            while (name.Length > 42)
            {
                Console.WriteLine(name[..42]);
                name = name[42..];
            }

            Console.WriteLine(
                $"{name,-42} {item.MethodCount,9} " +
                $"{item.PhysicalLines,7} {item.LogicalLines,7}");
        }

        WriteStyled(new string('-', 72), ConsoleColor.Red);

        WriteStyled(
            $"TOTAL LOC:  {result.TotalPhysicalLines}",
            ConsoleColor.White);

        WriteStyled(
            $"TOTAL LLOC: {result.TotalLogicalLines}",
            ConsoleColor.White);

        WriteStyled(new string('=', 72), ConsoleColor.Red);
    }

    public void ShowError(string message)
    {
        Console.WriteLine();
        WriteStyled($"[ERROR] {message}", ConsoleColor.Red);
    }

    public void Pause()
    {
        Console.WriteLine();
        WriteStyled(
            "Presione ENTER para continuar...",
            ConsoleColor.DarkGray);

        Console.ReadLine();
    }

    private void WriteStyled(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
