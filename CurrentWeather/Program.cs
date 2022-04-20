using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace CurrentWeather
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = File.ReadAllText("appsettings.json");

            // We want this to be private and ignored from git
            var apiKey = JObject.Parse(key).GetValue("DefaultKey").ToString();

            Console.WriteLine("Enter zip code: ");
            var zipCode = Console.ReadLine();

            var weatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={zipCode}&units=imperial&appid={apiKey}";

            var client = new HttpClient();

            //Call API
            var weather = client.GetStringAsync(weatherURL).Result;

            //Get data
            var cityName = JObject.Parse(weather)["name"];

            var weatherSky = JObject.Parse(weather)["weather"][0]["main"].ToString(); //need to get "main" from "weather"

            var temp = double.Parse(JObject.Parse(weather)["main"]["temp"].ToString());

            var feelsLike = double.Parse(JObject.Parse(weather)["main"]["feels_like"].ToString());

            var minTemp = double.Parse(JObject.Parse(weather)["main"]["temp_min"].ToString());
            var maxTemp = double.Parse(JObject.Parse(weather)["main"]["temp_max"].ToString());

            Console.WriteLine($"Current weather in {cityName}:\n");
            Console.WriteLine($"It's {temp} degrees Fahrenheit and {weatherSky}.\n");
            Console.WriteLine($"Today's high is {maxTemp} with a low of {minTemp}.\n");

            Console.WriteLine($"It feels like {feelsLike} degrees out.");

        }
    }
}
