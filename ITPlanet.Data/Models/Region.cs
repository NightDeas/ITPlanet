using System.Security.Principal;

namespace ITPlanet.Data.Models
{
    public class Region
    {
        public long? Id { get; set; }
        public long RegionTypeId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string ParentRegion { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual RegionType RegionType { get; set; }

        public virtual User User { get; set; }
    }
}
