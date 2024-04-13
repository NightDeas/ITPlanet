namespace ITPlanet.Data.Models
{
    public class Weather
    {
        public long Id { get; set; }

        public long RegionId { get; set; }

        public string RegionName { get; set; }

        public float Temperature { get; set; }

        public float Humidity { get; set; }

        public float WindSpeed { get; set; }

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

        public float PrecipitationAmount { get; set; }

        public DateTime MeasurementDateTime { get; set; }

        public virtual Region Region { get; set; }

        //public virtual ICollection<Models.WeatherForecast> WeatherForecast { get; set; }
    }
}
