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

        public static ApiHelper GetInstance()
        {
            return _instance;
        }
        public async Task<Root> GetWeather(double latitude, double longitude)
        {
            var responce = await _httpClient.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=4280e2f87f6da27bf8dc498af65fa883",latitude,longitude));
            return JsonConvert.DeserializeObject<Root>(responce);
        }


        public async Task<Root> GetWeather(string city)   
        {
            var responce = await _httpClient.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=4280e2f87f6da27bf8dc498af65fa883", city));
            return JsonConvert.DeserializeObject<Root>(responce);
        }




    }
}
