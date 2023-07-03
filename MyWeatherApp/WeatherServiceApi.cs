using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyWeatherApp
{
    public class WeatherServiceApi
    {
        private readonly HttpClient _httpClient;

        public WeatherServiceApi()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetWeatherData(double latitude, double longitude)
        {
            var apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
