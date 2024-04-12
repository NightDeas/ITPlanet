namespace ITPlanet.Models
{
    public class RegionModel
    {
        public long? Id { get; set;}
        public long RegionType { get; set;}
        public string Name { get; set; }
        public string ParentRegion { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

    }
}
