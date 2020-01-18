using Forrest.Models;
using Forrest.Models.WeatherForecastGeoposition;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Forrest.Managers
{
    public class WeatherForecastManager
    {
        public async Task<WeatherForecastGeoposition> GetPlaceId(double latitude, double longitude)
        {
            //TODO: Hide api key
            string requestUri = "http://dataservice.accuweather.com/locations/v1/cities/geoposition/search?apikey=EofA5piB0DPoEZNDdGRzAohuIIdurBHI&q=" +
                $"{latitude}%2C{longitude}&language=pl-pl";
            WeatherForecastGeoposition result;

            using (var client = new HttpClient())
            {
                var response = await Task.Run(() => client.GetStringAsync(requestUri));
                result = JsonConvert.DeserializeObject<WeatherForecastGeoposition>(response);
            }
            return result;
        }

        public async Task<WeatherForecast> GetHourlyForecast(string placeId)
        {
            //TODO: Hide api key
            string requestUri = "http://dataservice.accuweather.com/forecasts/v1/hourly/1hour/" +
                                $"{placeId}?apikey=EofA5piB0DPoEZNDdGRzAohuIIdurBHI&language=pl-pl&metric=true";
            List<WeatherForecast> result;

            using (var client = new HttpClient())
            {
                var response = await Task.Run(() => client.GetStringAsync(requestUri));
                result = JsonConvert.DeserializeObject<List<WeatherForecast>>(response);
            }
            return result.FirstOrDefault();
        }
    }
}
