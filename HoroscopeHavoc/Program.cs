using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace HoroscopeHavoc
{
    internal class Program
    {
        static readonly HttpClient client = new HttpClient();
        static readonly IList<string> starSigns = new List<string> {"aries", "taurus", "gemini", "cancer", "leo", "virgo", "libra", "scorpio", "sagitarius", "capricorn", "aquarius", "pisces"};

        static async Task Main(string[] args)
        {
            // Load required environment variables
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path to the current directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Load the appsettings.json

            IConfiguration configuration = builder.Build();

            // Error handling and input to receive Zodiac sign
            string input = "";
            bool isValidInput = false;

            while (!isValidInput)
            {
                // Initial prompts to collect information
                Console.WriteLine("Enter your Zodiac sign to get going: ");

                input = Console.ReadLine();
                input = input.ToLower();

                // Check if input is valid via loop before calling the endpoint
                if(!string.IsNullOrWhiteSpace(input) && starSigns.Contains(input))
                {
                    Console.WriteLine($"Great so your sign is {input}!. Now, let's get your daily horoscope!");
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("The input you entered does not represent a valid zodiac, please try again!");
                }
            }

            // Addition of API 
            client.DefaultRequestHeaders.Add("X-Rapidapi-Key", configuration["ApiSettings:ApiKey"]);
            client.DefaultRequestHeaders.Add("X-Rapidapi-Host", "daily-horoscope-api.p.rapidapi.com");

            // Make an asyncronous GET request to the following endpoint in order to identify what the stars say

            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://daily-horoscope-api.p.rapidapi.com/api/Daily-Horoscope-English/?zodiacSign={input}&timePeriod=weekly");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    using (JsonDocument doc = JsonDocument.Parse(responseBody))
                    {
                        string prediction = doc.RootElement.GetProperty("prediction").GetString();
                        Console.WriteLine(prediction);
                    }
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request Error: {e.Message}");
            }
            catch (JsonException e)
            {
                Console.WriteLine($"JSON Parsing Error: {e.Message}");
            }
        }
    }
}