namespace ITPlanet.Data.Models
{
    public class WeatherForecast
    {
        public long Id { get; set; }

        public DateTime DateTime { get; set; }

        public float Temperature { get; set; }

        private string _weatherCondition { get; set; }
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

        public long RegionId { get; set; }

        public virtual Region Region { get; set; }

        //public virtual Weather Weather { get; set; }
    }
}
