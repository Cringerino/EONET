namespace EONET.Models;

public sealed class EonetResponse
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public List<EonetEvent> Events { get; set; } = [];
}

public sealed class EonetEvent
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Link { get; set; } = string.Empty;
    public DateTimeOffset? Closed { get; set; }
    public List<EonetCategory> Categories { get; set; } = [];
    public List<EonetSource> Sources { get; set; } = [];
    public List<EonetGeometry> Geometry { get; set; } = [];
}

public sealed class EonetCategory
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}

public sealed class EonetSource
{
    public string Id { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public sealed class EonetGeometry
{
    public decimal? MagnitudeValue { get; set; }
    public string? MagnitudeUnit { get; set; }
    public string? MagnitudeDescription { get; set; }
    public DateTimeOffset? Date { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal[] Coordinates { get; set; } = [];
}
