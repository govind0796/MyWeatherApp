using System;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace MyWeatherApp
{
    public static class Program
    {
        
        public static async Task Main()
        {
            //NOTE : Change connection string as per your server
            const string connectionString = @"Data Source=GOVIND-OJHA\SQLEXPRESS;Initial Catalog=MyWeatherAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            Console.Write("Enter city name: ");

            var cityName = Console.ReadLine();

            if (!string.IsNullOrEmpty(cityName))
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


                    Console.WriteLine($"Current weather report of {cityName.ToUpper()}");
                    Console.WriteLine($"Temperature = {weatherDataJson.current_weather.temperature} degree Celsius");
                    Console.WriteLine($"Wind Speed = {weatherDataJson.current_weather.windspeed} km/hr");
                }
                else
                {
                    Console.WriteLine("City not found.");
                }
            }
        }
    }
}
