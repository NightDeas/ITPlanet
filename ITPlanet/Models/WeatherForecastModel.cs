namespace ITPlanet.Models
{
    public class WeatherForecastModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public float Temperature { get; set; }
        public string WeatherCondition
        {
            get => WeatherCondition;
            set
            {
                if (value == "CLEAR" || value == "CLOUDY" || value == "RAIN" || value == "SNOW" || value == "FOG" || value == "STORM")
                    WeatherCondition = value;
                else
                    WeatherCondition = "";
            }
        }
        public long RegionId;

    }
}
