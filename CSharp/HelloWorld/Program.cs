using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Globalization;
using System.Linq;

/*
16 Segment Display:
 _ _
|\|/|
 - - 
|/|\|
 - - 
*/

namespace HelloWorld
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var display = new SegmentDisplay16();

            const int northbound16 = 5663;
            const int southbound16 = 5664;
            var apiService = new TransseeApiService();
            var result = await apiService.GetStopInfoAsync(northbound16.ToString());

            if (result != null)
            {
                // Parse vehicles array
                var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(result.Vehicles.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Find first vehicle with route "16"
                var vehicle16 = vehicles?.FirstOrDefault(v => v.Route == "16");

                if (vehicle16 != null && !string.IsNullOrEmpty(vehicle16.Actual))
                {
                    // Parse actual time (format: "8:18:49PM")
                    if (DateTime.TryParse(vehicle16.Actual, out DateTime actualTime))
                    {
                        var now = DateTime.Now;
                        var today = now.Date;
                        
                        // Set the date part to today
                        actualTime = today.AddHours(actualTime.Hour).AddMinutes(actualTime.Minute).AddSeconds(actualTime.Second);
                        
                        // If the time is in the past, assume it's for tomorrow
                        if (actualTime < now)
                        {
                            actualTime = actualTime.AddDays(1);
                        }
                        
                        var timeDiff = actualTime - now;
                        var minutes = (int)Math.Round(timeDiff.TotalMinutes);

                        // Format: "16 McCowan to STC in {time} min"
                        var output = $"16 McCowan to STC in {minutes} min";
                        
                        // Display using segment display
                        display.DisplayText(output);
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to get data from API.");
            }
            // insert some whitespace
            result = await apiService.GetStopInfoAsync(southbound16.ToString());
            if (result != null)
            {
                // Parse vehicles array
                var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(result.Vehicles.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Find first vehicle with route "16"
                var vehicle16 = vehicles?.FirstOrDefault(v => v.Route == "16");

                if (vehicle16 != null && !string.IsNullOrEmpty(vehicle16.Actual))
                {
                    // Parse actual time (format: "8:18:49PM")
                    if (DateTime.TryParse(vehicle16.Actual, out DateTime actualTime))
                    {
                        var now = DateTime.Now;
                        var today = now.Date;
                        
                        // Set the date part to today
                        actualTime = today.AddHours(actualTime.Hour).AddMinutes(actualTime.Minute).AddSeconds(actualTime.Second);
                        
                        // If the time is in the past, assume it's for tomorrow
                        if (actualTime < now)
                        {
                            actualTime = actualTime.AddDays(1);
                        }
                        
                        var timeDiff = actualTime - now;
                        var minutes = (int)Math.Round(timeDiff.TotalMinutes);

                        // Format: "16 McCowan to STC in {time} min"
                        var output = $"16 McCowan to Warden in {minutes} min";
                        
                        // Display using segment display
                        display.DisplayText(output);
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to get data from API.");
            }

            apiService.Dispose();
        }
    }

    class SegmentDisplay16
    {
        private Dictionary<char, string[]> letters;

        public SegmentDisplay16()
        {
            letters = SegmentDisplayLetters.GetLetters();
        }

        public void DisplayText(string text)
        {
            Console.WriteLine();
            for (int row = 0; row < 5; row++)
            {
                foreach (char c in text.ToLower())
                {
                    if (letters.ContainsKey(c))
                    {
                        Console.Write(letters[c][row]);
                    }
                    else
                    {
                        Console.Write("     ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
