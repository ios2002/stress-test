using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  private static readonly string[] Summaries = new[]
  {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

  private readonly ILogger<WeatherForecastController> _logger;

  public WeatherForecastController(ILogger<WeatherForecastController> logger)
  {
    _logger = logger;
  }

  [HttpGet(Name = "GetWeatherForecast")]
  public IEnumerable<WeatherForecast> Get()
  {
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
      Date = DateTime.Now.AddDays(index),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
  }
  // [(ht = "GetWeatherForecast")]
  [HttpPost("Insert")]
  public async Task<bool> Post([FromBody] Car car)
  {

    // _logger.LogWarning("hit start ");
    var connString = "Host=masterdb-service;Username=dbuser;Password=dbuser123;Database=test";

    await using var conn = new NpgsqlConnection(connString);
    await conn.OpenAsync();

    // Insert some data
    await using (var cmd = new NpgsqlCommand("INSERT INTO car (\"Name\",\"Model\") VALUES (@name, @model)", conn))
    {
      cmd.Parameters.AddWithValue("name", car.Name);
      cmd.Parameters.AddWithValue("model", car.Model);
      await cmd.ExecuteNonQueryAsync();
    }

    // Retrieve all rows
    // await using (var cmd = new NpgsqlCommand("SELECT 'Name' FROM car", conn))
    // await using (var reader = await cmd.ExecuteReaderAsync())
    // {
    //   while (await reader.ReadAsync())
    //     Console.WriteLine(reader.GetString(0));
    // }
    return true;
  }

  public class Car
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Model { get; set; }
  }
}
