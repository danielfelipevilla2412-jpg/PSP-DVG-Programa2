public class MetricsResult
{
    public int FileCount { get; set; }

    public int TotalPhysicalLines { get; set; }

    public int TotalLogicalLines { get; set; }

    public List<ClassMetrics> Classes { get; set; } = new();

    public int TotalClasses => Classes.Count;

    public int TotalMethods => Classes.Sum(c => c.MethodCount);
}
