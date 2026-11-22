namespace Programa2.Models;

public class ClassMetrics
{
    public string Name { get; set; } = string.Empty;

    public int MethodCount { get; set; }

    public int PhysicalLines { get; set; }

    public int LogicalLines { get; set; }
}
