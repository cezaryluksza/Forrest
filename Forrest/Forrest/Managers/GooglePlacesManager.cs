using Newtonsoft.Json;
using Forrest.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Forrest.Models.WeatherForecastGeoposition;

namespace Forrest.Managers
{
    public class GooglePlacesManager
    {

        public List<PlaceDetails> GetPlaceDetails(string placeName)
        {
            var taskResult = GetPlaceDetailsAsync(placeName);
            if (taskResult == null) return null;
            List<PlaceDetails> list = new List<PlaceDetails>();
            list.Add(taskResult.Result?.result);

            return list;
        }

        public async Task<GooglePlaceDetails> GetPlaceDetailsAsync(string placeName)
        {
            WeatherForecastManager weatherForecastManager = new WeatherForecastManager();

            Place googlePlace = await FindPlaceFromText(placeName);

            var placeTask = Task.Run(() => GetPlaceDetailsAPI(googlePlace.place_id));
            var geoPositionTask = Task.Run(() => weatherForecastManager.GetPlaceId(googlePlace.geometry.location.lat, googlePlace.geometry.location.lng));

            GooglePlaceDetails googlePlaceDetails = await placeTask;
            WeatherForecastGeoposition pos = await geoPositionTask;

            googlePlaceDetails.result.WeatherForecast = await weatherForecastManager.GetHourlyForecast(pos.Key);

            return googlePlaceDetails;
        }



        public async Task<GooglePlaceDetails> GetPlaceDetailsAPI(string placeId)
        {
            //TODO: Hide api key
            GooglePlaceDetails result;
            string requestUri = "https://maps.googleapis.com/maps/api/place/details/json?key=AIzaSyAl48sp1G5yP9Iiohv6sfftXypcfY-7hLE&placeid="
                + placeId;


            using (var client = new HttpClient())
            {
                var response = await Task.Run(() => client.GetStringAsync(requestUri));
                result = JsonConvert.DeserializeObject<GooglePlaceDetails>(response);
            }
            return result;
        }


        public async Task<Place> FindPlaceFromText(string placeName)
        {
            //TODO: Hide api key
            string requestUri = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=" +
                $"{placeName}&inputtype=textquery&fields=formatted_address,name,place_id,geometry&key=AIzaSyAl48sp1G5yP9Iiohv6sfftXypcfY-7hLE";
            GooglePlace result;

            using (var client = new HttpClient())
            {
                var response = await Task.Run(() => client.GetStringAsync(requestUri));
                result = JsonConvert.DeserializeObject<GooglePlace>(response);
            }
            return result.candidates.FirstOrDefault();
        }
    }
}
