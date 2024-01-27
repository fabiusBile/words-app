using Microsoft.AspNetCore.Authorization;
using WordsApp.Authentication;

namespace WordsApp;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("[controller]")]
[Authorize]
public class WeatherForecastController : ControllerBase
{
    private readonly IWordsAppUser _user;

    public WeatherForecastController(IWordsAppUser user)
    {
        _user = user;
    }
    
    private static readonly string[] _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet]
    public object Get()
    {
        return _user;
        // var forecast = Enumerable.Range(1, 5).Select(index =>
        //         new WeatherForecast(
        //             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //             new Random().Next(-20, 55),
        //             _summaries[new Random().Next(_summaries.Length)]
        //         ))
        //     .ToArray();
        //
        // return forecast;
    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}