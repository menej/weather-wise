﻿using FuzzySharp;
using FuzzySharp.Extractor;
using WeatherWise;
using WeatherWise.Entities;

namespace WeatherWise.Utilities;

public class ConsoleMenu
{
    public User AppUser { get; set; }

    public ConsoleMenu()
    {
        AppUser = new User();
    }

    public void DisplayMainMenu()
    {
        DisplayWelcomeMessage();

        while (true)
        {
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. General info");
            Console.WriteLine("2. Temperature and humidity");
            Console.WriteLine("3. Precipitation probability");
            Console.WriteLine("4. Wind info");

            if (AppUser.DefaultLocation == null) Console.WriteLine("6. Set default location");
            else Console.WriteLine("5. Change or unset default location");

            Console.WriteLine("6. Exit application");
            Console.WriteLine();

            Console.Write("Enter option: ");

            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    DisplayGeneralInfo();
                    break;
                case "2":
                    DisplayTemperatureAndHumidity();
                    break;
                case "3":
                    DisplayPrecipitationProbability();
                    break;
                case "4":
                    DisplayWindInfo();
                    break;
                case "5":
                    SetOrChangeDefaultLocation();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    private void DisplayTemperatureAndHumidity()
    {
        var userCity = ChooseLocation();
        if (userCity == null)
        {
            Console.WriteLine();
            return;
        }
        
        Console.WriteLine("Displaying temperature and humidity.");
        Console.WriteLine();
    }

    private static string? ChooseLocation()
    {
        var cities = CityCoordinates.Coordinates;
        var cityNames = cities.Keys;

        string? userChoice = null;

        while (userChoice == null)
        {
            Console.WriteLine("Available cities: " + string.Join(", ", cityNames));
            Console.WriteLine("Type \"back\" to return to main menu.");
            Console.Write("Please enter a City from the available list: ");

            var userInput = Console.ReadLine();

            if (userInput is "back" or "\"back\"")
            {
                return null;
            }

            var matchedCity =
                cityNames.FirstOrDefault(city => city.Equals(userInput, StringComparison.OrdinalIgnoreCase));

            if (matchedCity != null)
            {
                userChoice = matchedCity;
            }
            // If there was no match, try to check for similarities
            else
            {
                ExtractedResult<string> probableCity = Process.ExtractOne(userInput, cityNames);
                
                // Check if the city name is really similar to the users input
                if (probableCity.Score < 80)
                {
                    Console.WriteLine("Please provide a city that is on the list.\n");
                    continue;
                }

                Console.WriteLine($"Did you mean: {probableCity.Value}? (Y/N)");
                Console.Write("Enter option: ");
                
                var userDecision = Console.ReadLine();

                if (string.Equals(userDecision, "y", StringComparison.OrdinalIgnoreCase))
                {
                    userChoice = probableCity.Value;
                }
            }
        }

        if (userChoice == null)
        {
            throw new InvalidOperationException("User choice should never be null here.");
        }

        Console.WriteLine($"You have selected: {userChoice}");
        return userChoice;
    }

    private void SetOrChangeDefaultLocation()
    {
        throw new NotImplementedException();
    }

    private void DisplayWindInfo()
    {
        throw new NotImplementedException();
    }

    private void DisplayPrecipitationProbability()
    {
        throw new NotImplementedException();
    }

    private void DisplaySunriseAndSunset()
    {
        throw new NotImplementedException();
    }


    private void DisplayGeneralInfo()
    {
        throw new NotImplementedException();
    }

    private static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Hello there, welcome to the best Weather app!");
    }
}