using WeatherApp.Helpers;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace WeatherApp.View;

public partial class WeekPage : ContentPage
{
    public ObservableCollection<Model.List> WeatherList;
    private double latitude;
    private double longitude;
    public WeekPage()
	{
		InitializeComponent();
        WeatherList = new ObservableCollection<Model.List>();
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);

    }

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLastKnownLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ApiHelper.GetWeather(latitude, longitude);
        UpdateUI(result);
    }

    public void UpdateUI(dynamic result)
    {
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }
        IEnumerable<Model.List> readyList = WeatherList.Where(dt => dt.dt_txt.Contains("15:00:00"));
        CvWeather.ItemsSource = readyList;
    }
}