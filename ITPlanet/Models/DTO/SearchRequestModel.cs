namespace ITPlanet.Models.DTO
{
    public class SearchRequestModel
    {
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public long? RegionId { get; set; }
        public string? WeatherCondition
        {
            get => WeatherCondition;
            set
            {
                if (value == "CLEAR" || value == "CLOUDY" || value == "RAIN" || value == "SNOW" || value == "FOG" || value == "STORM" || value == null)
                    WeatherCondition = value;
                else
                    WeatherCondition = "";
            }
        }
        public int From { get; set; } = 0;
        public int Size { get; set; } = 10;

    }
}
