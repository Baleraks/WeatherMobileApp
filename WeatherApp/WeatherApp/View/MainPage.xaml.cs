using WeatherApp.Helpers;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
    public List<Model.List> WeatherList;
    private double latitude;
    private double longitude;

	public MainPage()
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

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
       var responce = await DisplayPromptAsync(title: "", message: "", placeholder: "Search city", accept: "Search", cancel: "Cancel");
        if (responce != null) 
        {
            await GetWeatherDataByCity(responce);
        }
    }

    private async void ImageButton_Clicked_1(object sender, EventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ApiHelper.GetWeather(latitude, longitude);
        UpdateUI(result);
    }

    public async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiHelper.GetWeatherByCity(city);
        UpdateUI(result);
    }

    public void UpdateUI(dynamic result)
    {
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;




        ImgWeatherIcon.Source = result.list[0].weather[0].fullIconUrl;
        lbCity.Text = result.city.name;
        lbWeatherDescription.Text = result.list[0].weather[0].description;
        lbTemperature.Text = result.list[0].main.temperature + "°C";
        lbHumidity.Text = result.list[0].main.humidity + "%";
        lbWind.Text = result.list[0].wind.speed + "km/h";
    }
}

