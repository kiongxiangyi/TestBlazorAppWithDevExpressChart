using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class WeatherHub : Hub
{
    public async Task SendWeatherUpdate(WeatherForecast forecast)
    {
        await Clients.All.SendAsync("ReceiveWeatherUpdate", forecast);
    }
}