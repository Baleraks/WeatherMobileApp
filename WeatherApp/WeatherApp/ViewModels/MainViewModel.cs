using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Helpers;

namespace WeatherApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Model.WeatherInfo> _weatherList;
        private double _lotitude;
        private double _longitude;
        private string _city;
        private string _weatherDescription;
        private string _temperature;
        private string _humidity;
        private string _wind;
        private string _image;

        public string WeatherDescription
        {
            get { return _weatherDescription; }
            set
            {
                _weatherDescription= value;
                OnPropertyChanged();
            }
        }
        public string Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }
        public string Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                OnPropertyChanged();
            }
        }
        public string Wind
        {
            get { return _wind; }
            set
            {
                _wind = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Model.WeatherInfo> WeatherList
        {
            get { return _weatherList; }
            set
            {
                _weatherList = value;
                OnPropertyChanged(nameof(WeatherList));
            }
        }
        public double Lotitude
        {
            get { return _lotitude; }
            set
            {
                _lotitude = value;
                OnPropertyChanged();
            }
        }
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }
        public string City
        {
            get { return _city; } 
            set 
            { 
                _city = value;      
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public ICommand CitySearch { get; set; }
        public ICommand MainLocation { get; set; }


        public MainViewModel()
        {
            _ = GetLocation();
            _ = GetWeatherDataByLocation(_lotitude, _longitude);
            CitySearch = new Command(SearchCity);
            MainLocation = new Command(CurrentLocation);
        }

        public async Task GetLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            Lotitude = location.Latitude;
            Longitude = location.Longitude;
        }
        public async Task GetWeatherDataByCity(string city)
        {
            var cityLocation = await ApiHelper.GetInstance().GetWeather(city);
            UpdateForm(cityLocation);
        }

        public async Task GetWeatherDataByLocation(double latitude, double longitude)
        {
            var weatherLocation = await ApiHelper.GetInstance().GetWeather(latitude, longitude);
            UpdateForm(weatherLocation);
        }

        private async void SearchCity()
        {
            var responce = await App.Current.MainPage.DisplayPromptAsync(title: "", message: "", placeholder: "Search city", accept: "Search", cancel: "Cancel");
            if (responce != null)
            {
                await GetWeatherDataByCity(responce);
            }
        }

        public void UpdateForm(dynamic weatherLocation)
        {
            WeatherList = new ObservableCollection<Model.WeatherInfo>(weatherLocation.list);
            Image = weatherLocation.list[0].weather[0].fullIconUrl;
            WeatherDescription = weatherLocation.list[0].weather[0].description;
            Temperature = weatherLocation.list[0].main.temperature + "°C";
            Humidity = weatherLocation.list[0].main.humidity + "%";
            Wind = weatherLocation.list[0].wind.speed + "km/h";
            City = weatherLocation.city.CityName;
        }
        private async void CurrentLocation ()
        {
            await GetLocation();
            await GetWeatherDataByLocation(_lotitude, _longitude);
        }
    }
}
