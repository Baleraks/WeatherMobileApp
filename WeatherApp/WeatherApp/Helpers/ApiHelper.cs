using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Helpers
{
    public static class ApiHelper
    {
        public static async Task<Root> GetWeather(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=4280e2f87f6da27bf8dc498af65fa883",latitude,longitude));
            return JsonConvert.DeserializeObject<Root>(responce);
        }


        public static async Task<Root> GetWeatherByCity(string city)   
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=4280e2f87f6da27bf8dc498af65fa883", city));
            return JsonConvert.DeserializeObject<Root>(responce);
        }




    }
}
