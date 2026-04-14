namespace EONET.Models
{
    /// <summary>
    /// class representing the structure of the data returned by the EONET API, including properties for the root object, events, categories, sources, and geometry. This class is used for deserializing the JSON response from the EONET API into C# objects for easier manipulation and display in the application.
    /// </summary>
    public class EonetData
    {
        public List<EonetRoot> Rootobjects { get; set; }
    }

    public class EonetRoot
    {
        public string title { get; set; }
        public string link { get; set; }
        public List<Event> events { get; set; }
    }

    public class Event
    {
        public string id { get; set; }
        public string title { get; set; }
        public string? description { get; set; }
        public string link { get; set; }
        public DateTime? closed { get; set; }
        public List<Category> categories { get; set; }
        public List<Source> sources { get; set; }
        public List<Geometry> geometry { get; set; }
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
        public double? magnitudeValue { get; set; }
        public string? magnitudeUnit { get; set; }
        public DateTime? date { get; set; }
        public string? type { get; set; }
        public double[]? coordinates { get; set; }
    }
}

