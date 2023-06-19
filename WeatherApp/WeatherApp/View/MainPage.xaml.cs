using WeatherApp.Helpers;

namespace WeatherApp;

public partial class MainPage : ContentPage
{


	public MainPage()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var result = await ApiHelper.GetWeather(53.6884000, 23.8258000);
        lbCity.Text =  result.city.name;
        lbWeatherDescription.Text = result.list[0].weather[0].description;
        lbTemperature.Text = result.list[0].main.temperature + "°C";
        lbHumidity.Text = result.list[0].main.humidity + "%";
        lbWind.Text = result.list[0].wind.speed + "km/h";
    }


}

