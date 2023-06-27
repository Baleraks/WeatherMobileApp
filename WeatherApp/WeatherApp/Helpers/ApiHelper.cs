using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.Helpers
{
    public class ApiHelper
    {
        private static ApiHelper _instance = new ApiHelper();
        private HttpClient _httpClient = new HttpClient();
        private string mainString = "https://api.openweathermap.org/data/2.5/forecast";
        private string apiKey = "units=metric&appid=4280e2f87f6da27bf8dc498af65fa883";

        public static ApiHelper GetInstance()
        {
            return _instance;
        }
        public async Task<MainInfo> GetWeather(double latitude, double longitude)
        {
            var responce = await _httpClient.GetStringAsync(String.Format("{0}?lat={1}&lon={2}&{3}", mainString, latitude, longitude, apiKey));
            return JsonConvert.DeserializeObject<MainInfo>(responce);
        }


        public async Task<MainInfo> GetWeather(string city)   
        {
            var responce = await _httpClient.GetStringAsync(String.Format("{0}?q={1}&{2}", mainString,city,apiKey));
            return JsonConvert.DeserializeObject<MainInfo>(responce);
        }




    }
}
