using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
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
        public string _weatherCondition;
        public string WeatherCondition
        {
            get => _weatherCondition;
            set
            {
                if (value == "CLEAR" || value == "CLOUDY" || value == "RAIN" || value == "SNOW" || value == "FOG" || value == "STORM")
                    _weatherCondition = value;
                else
                    _weatherCondition = "CLEAR";
            }
        }
        public float PrecipitationAmount { get; set; }
        public DateTime MeasurementDateTime { get;set; }
        public List<Models.WeatherForecastModel> WeatherForecast = new List<Models.WeatherForecastModel>();

       

    }
}
