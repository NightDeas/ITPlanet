using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ITPlanet.Models
{
    public class WeatherModel
    {

        public long Id { get; set; }
        public long RegionId { get;set; }
        public string RegionName { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float WindSpeed { get; set; }
        public string WeatherCondition
        {
            get => WeatherCondition;
            set
            {
                if (value == "CLEAR" || value == "CLOUDY" || value == "RAIN" || value == "SNOW" || value == "FOG" || value == "STORM")
                    WeatherCondition = value;
                else
                    WeatherCondition = "CLEAR";
            }
        }
        public float PrecipitationAmount { get; set; }
        public DateTime MeasurementDateTime { get;set; }
        public List<Models.WeatherForecastModel> WeatherForecast = new List<Models.WeatherForecastModel>();

        public WeatherModel()
        {
        }

    }
}
