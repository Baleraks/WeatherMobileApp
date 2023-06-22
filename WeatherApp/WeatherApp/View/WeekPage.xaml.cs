using WeatherApp.Helpers;
using System.Text.RegularExpressions;

namespace WeatherApp.View;

public partial class WeekPage : ContentPage
{
    public List<Model.List> WeatherList;
    private double latitude;
    private double longitude;
    public WeekPage()
	{
		InitializeComponent();
        WeatherList = new List<Model.List>();
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
        CvWeather.ItemsSource = WeatherList;
    }
}