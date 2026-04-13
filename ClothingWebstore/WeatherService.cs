using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClothingWebstore
{
    public class WeatherService
    {
        public static async Task<WeatherResponse?> GetApiData()
        {
            var client = new HttpClient();
            var apiKey = "8a67b1259e039417aad8365c04714f27";
            client.BaseAddress = new Uri("https://api.openweathermap.org/");
            HttpResponseMessage response = await client.GetAsync($"data/2.5/weather?lat=58.3479&lon=11.9558&units=metric&appid={apiKey}");
       
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<WeatherResponse>(responseString)!;
            }
            return null;
        }
    }
    public class WeatherResponse
    {
        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; }

        [JsonPropertyName("main")]
        public Main Main { get; set; }
    }
    public class Main
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
    }
    public class Weather
    {
        [JsonPropertyName("description")]
        public string MainDescription { get; set; }
    }
}
