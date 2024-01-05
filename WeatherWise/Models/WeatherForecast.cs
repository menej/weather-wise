﻿using System.Text.Json.Serialization;

namespace WeatherAnalyzer;

public class WeatherForecast
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("hourly")]
    public HourlyData? Hourly { get; set; }
}
