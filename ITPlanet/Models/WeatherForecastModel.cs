namespace ITPlanet.Models
{
    public class WeatherForecastModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public float Temperature { get; set; }
        public string _weatherCondition;
        public string WeatherCondition
        {
            get => _weatherCondition;
            set
            {
                if (value == "CLEAR" || value == "CLOUDY" || value == "RAIN" || value == "SNOW" || value == "FOG" || value == "STORM")
                    _weatherCondition = value;
                else
                    _weatherCondition = "";
            }
        }
        public long RegionId;

    }
}
