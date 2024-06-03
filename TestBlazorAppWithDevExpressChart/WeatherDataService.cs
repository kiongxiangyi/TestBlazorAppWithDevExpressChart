using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class WeatherDataService : BackgroundService
{
    private readonly IHubContext<WeatherHub> _hubContext;

    public WeatherDataService(IHubContext<WeatherHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var random = new Random();

        while (!stoppingToken.IsCancellationRequested)
        {
            var forecast = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = random.Next(-20, 55),
                Precipitation = random.NextDouble() * 10
            };

            Console.WriteLine($"Sending update: {forecast.Date}, {forecast.TemperatureC}C, {forecast.Precipitation}mm");
            await _hubContext.Clients.All.SendAsync("ReceiveWeatherUpdate", forecast);

            await Task.Delay(5000, stoppingToken); // Send update every 5 seconds
        }
    }
}
