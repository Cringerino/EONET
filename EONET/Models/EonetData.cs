namespace EONET.Models
{
}

public class EonetData
{
    public List<Rootobject> Rootobjects { get; set; }
}

public class Rootobject
{
    public string title { get; set; }
    public string description { get; set; }
    public string link { get; set; }
    public Event[] events { get; set; }
}

public class Event
{
    public string id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string link { get; set; }
    public object closed { get; set; }
    public Category[] categories { get; set; }
    public Source[] sources { get; set; }
    public Geometry[] geometry { get; set; }
}

public class Category
{
    public string? id { get; set; }
    public string? title { get; set; }
}

public class Source
{
    public string? id { get; set; }
    public string? url { get; set; }
}

public class Geometry
{
    public float? magnitudeValue { get; set; }
    public string? magnitudeUnit { get; set; }
    public DateTime? date { get; set; }
    public string? type { get; set; }
    public float[]? coordinates { get; set; }
}

