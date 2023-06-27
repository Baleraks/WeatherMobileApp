using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Helpers;


namespace WeatherApp.ViewModels
{
    public class WeekViewModel : BaseViewModel
    {
        private ObservableCollection<Model.WeatherInfo> _weatherList;
        public ObservableCollection<Model.WeatherInfo> WeatherList
        { 
            get { return _weatherList; }
            set 
            { 
                _weatherList = value;
                OnPropertyChanged(nameof(WeatherList));
            }
        }

        private double _lotitude;
        public double Lotitude
        {
            get { return _lotitude; }
            set
            {
                _lotitude = value;
                OnPropertyChanged();
            }
        }

        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }

        public WeekViewModel() {
            _ = GetLocation();
            _ = GetWeatherDataByLocation(_lotitude,_longitude);
        }

        public async Task GetLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            Lotitude = location.Latitude;
            Longitude = location.Longitude;
        }

        public async Task GetWeatherDataByLocation(double latitude, double longitude)
        { 
            TimeSpan dayTime;
            TimeSpan.TryParse("15:00:00", out dayTime);
            var weatherLocation = await ApiHelper.GetInstance().GetWeather(latitude, longitude);
            WeatherList = new ObservableCollection<Model.WeatherInfo>(weatherLocation.list.Where(dt => dt.WeatherDate.TimeOfDay == dayTime));
        }



    }
}
