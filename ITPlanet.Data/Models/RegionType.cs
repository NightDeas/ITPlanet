namespace ITPlanet.Data.Models
{
    public class RegionType
    {
        public long Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
