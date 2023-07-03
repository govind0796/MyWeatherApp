using System;
using Newtonsoft.Json;

namespace MyWeatherApp
{
    public class CurrentWeather
    {
        [JsonProperty("temperature")]
        public double Temperature { get; set; }

        [JsonProperty("windspeed")]
        public double WindSpeed { get; set; }

        [JsonProperty("winddirection")]
        public double WindDirection { get; set; }

        [JsonProperty("weathercode")]
        public int WeatherCode { get; set; }

        [JsonProperty("is_day")]
        public int IsDay { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }
    }

    public class WeatherData
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public double GenerationTimeMs { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string TimezoneAbbreviation { get; set; }

        [JsonProperty("elevation")]
        public double Elevation { get; set; }

        [JsonProperty("current_weather")]
        public CurrentWeather CurrentWeather { get; set; }
    }

}
