using System;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MyWeatherApp
{
    public static class Program
    {
        
        public static async Task Main()
        {
            const string connectionString = @"Data Source=GOVIND-OJHA\SQLEXPRESS;Initial Catalog=MyWeatherAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            while (true)
            {
                Console.Write("Enter city name (Enter 0 to exit): ");
                var cityName = Console.ReadLine();
                if (cityName == "0")
                {
                    break;
                }

                if (!string.IsNullOrEmpty(cityName) && !string.IsNullOrWhiteSpace(cityName))
                {
                    cityName = cityName.Trim();
                    var hasNumber = ContainsNumber(cityName);
                    if (hasNumber)
                    {
                        Console.Write("Numbers not allowed");
                    }
                    else
                    {
                        cityName = cityName.ToLower();

                        const string query = "SELECT * FROM tbl_worldCitiesCoordinates WHERE cityName = @cityName;";
                        await using var command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@cityName", cityName);

                        await using var reader = await command.ExecuteReaderAsync();

                        var dataTable = new DataTable();
                        dataTable.Load(reader);

                        if (dataTable.Rows.Count > 0)
                        {
                            var latitude = (double)dataTable.Rows[0]["Latitude"];
                            var longitude = (double)dataTable.Rows[0]["Longitude"];

                            var weatherService = new WeatherServiceApi();
                            var weatherData = await weatherService.GetWeatherData(latitude, longitude);

                            var weatherDataJson = JsonConvert.DeserializeObject<WeatherData>(weatherData);

                            // Print the data fetched from the API here
                            Console.WriteLine($"Current weather report of {cityName.ToUpper()}");
                            Console.WriteLine($"Temperature = {weatherDataJson.CurrentWeather.Temperature} degree Celsius");
                            Console.WriteLine($"Wind Speed = {weatherDataJson.CurrentWeather.WindSpeed} km/hr");
                        }
                        else
                        {
                            Console.WriteLine("City not found.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No data entered. Please try again.");
                }

                Console.WriteLine();
            }
        }

        public static bool ContainsNumber(string input)
        {
            return input.Any(char.IsDigit);
        }
    }
}
