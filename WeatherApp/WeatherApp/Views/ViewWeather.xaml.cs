using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewWeather : ContentPage
    {
        //private static  double longitude, latitude;
        //Position pos;
        public ViewWeather()
        {
            InitializeComponent();
            //getLocation();
            displayWeather();
        }
        #region getLocation 
        // this code works but for some reason it doesnt work when i use the position class to get the coordinates 
        //it has received from this function. still dont know why... will come back to it.
        /*private async Task<Position> getLocation()
        {
            
            try
            {
                var location = CrossGeolocator.Current;
                location.DesiredAccuracy = 50;

                var position = await location.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

                latitude = position.Latitude;
                longitude = position.Longitude;

                pos = new Position(latitude,longitude);
            }
            catch (Exception ex)
            {

                await DisplayAlert("Location", ex.Message, "Okay");
            }

            return pos;
            
        }*/
        #endregion
        
        #region display weather details
        // displaying the weather details
        public async Task displayWeather()
        {
            try
            {
                // get the location of the user currently
                var location = CrossGeolocator.Current;
                location.DesiredAccuracy = 50;

                var position = await location.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

                // use the open weather class to get the weather details for the current location received
                OpenWeather weather = await  GetWeather(position.Latitude, position.Longitude);

                //  calculate the temperatrure in celcius: 
                //double max = 5.0 / 9.0 * (double.Parse(weather.main.temp_max) - 32);
                //double min = 5.0 / 9.0 * (double.Parse(weather.main.temp_min) - 32);
                // display the details
                lblName.Text = "City Name: "+weather.name;
                lblmax.Text = "Max Temp: "+weather.main.temp_max.ToString()+ "°C";
                lblmix.Text = "Min Temp: "+weather.main.temp_min.ToString()+ "°C";
                weatherICon.Source = "http://openweathermap.org/img/w/"+ weather.weather[0].icon+".png";
                lblDesc.Text ="Weather: "+weather.weather[0].description;
                lblDate.Text = "Today , "+DateTime.Now.Date.ToString();
                lblHumidity.Text = "Humidity: "+weather.main.humidity.ToString();
            }
            catch (Exception ex)
            {
                // displays the error that was caught 
                await DisplayAlert("display Weather error",ex.Message,"Okay");
            }
           
        }
        #endregion


        #region getWeather details
        /// <summary>
        /// gets the weather details from the api and uses the two parameters to specify the 
        /// location and returns the data in a jsom format.
        /// from there it is then converted to the openweather class details 
        /// that has all the necessary fields to support the data received
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns>returns the openweather instance with the weather details deserialized to a readable format to be used later.</returns>
        public async  Task<OpenWeather> GetWeather(double lat, double lon)
        {
            OpenWeather data = null;
            try
            {
                var http = new HttpClient();
                string url = "http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&units=metric&appid=f4f0a01dd170f927829865108b4b396d";
                //string url = "http://api.openweathermap.org/data/2.5/weather?lat=-25.6798701&lon=28.1358576&appid=f4f0a01dd170f927829865108b4b396d";
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var serializer = new DataContractJsonSerializer(typeof(OpenWeather));

                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                data = (OpenWeather)serializer.ReadObject(ms);
            }
            catch (Exception ex)
            {
                // display the exception error for better debugging
                await DisplayAlert("get Weather error", ex.Message, "Okay");
            }
            

            return data;

        }
        #endregion
    }
}