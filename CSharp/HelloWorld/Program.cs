using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Globalization;
using System.Linq;
using DotNetEnv;

namespace HelloWorld
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Load .env file
            Env.Load();

            var apiService = new TransseeApiService();

            // Read stop IDs from environment variables
            var stopConfigs = new List<StopConfig>
            {
                new StopConfig { StopId = int.Parse(Environment.GetEnvironmentVariable("STOP_16_NORTHBOUND") ?? "0"), RouteNumber = "16", Message = "16 McCowan to STC in {0} min" },
                new StopConfig { StopId = int.Parse(Environment.GetEnvironmentVariable("STOP_16_SOUTHBOUND") ?? "0"), RouteNumber = "16", Message = "16 McCowan to Warden in {0} min" },
                new StopConfig { StopId = int.Parse(Environment.GetEnvironmentVariable("STOP_MCCOWAN_EAST") ?? "0"), RouteNumber = "95", Message = "95 York Mills to UTSC in {0} min" },
                new StopConfig { StopId = int.Parse(Environment.GetEnvironmentVariable("STOP_MCCOWAN_EAST") ?? "0"), RouteNumber = "995", Message = "995 York Mills to UTSC in {0} min" },
                new StopConfig { StopId = int.Parse(Environment.GetEnvironmentVariable("STOP_MCCOWAN_WEST") ?? "0"), RouteNumber = "38", Message = "38 Highland Creek to UTSC in {0} min" },
                new StopConfig { StopId = int.Parse(Environment.GetEnvironmentVariable("STOP_MCCOWAN_WEST") ?? "0"), RouteNumber = "133", Message = "133 Neilson to Neilson Rd in {0} min" }
            };

            // Group by stop ID to avoid duplicate API calls
            var uniqueStops = stopConfigs.GroupBy(c => c.StopId).ToList();
            
            // Make all API calls in parallel
            var apiTasks = uniqueStops.Select(g => new 
            { 
                StopId = g.Key, 
                Configs = g.ToList(),
                Task = apiService.GetStopInfoAsync(g.Key.ToString()) 
            }).ToList();

            var apiResults = await Task.WhenAll(apiTasks.Select(t => t.Task));

            // Process all results and prepare display messages
            var messages = new List<string>();
            var now = DateTime.Now;
            var today = now.Date;

            for (int i = 0; i < apiTasks.Count; i++)
            {
                var result = apiResults[i];
                var taskInfo = apiTasks[i];

                if (result != null)
                {
                    var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(result.Vehicles.GetRawText(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    foreach (var config in taskInfo.Configs)
                    {
                        var vehicle = vehicles?.FirstOrDefault(v => v.Route == config.RouteNumber);
                        if (vehicle != null && !string.IsNullOrEmpty(vehicle.Actual))
                        {
                            if (DateTime.TryParse(vehicle.Actual, out DateTime actualTime))
                            {
                                actualTime = today.AddHours(actualTime.Hour).AddMinutes(actualTime.Minute).AddSeconds(actualTime.Second);
                                
                                if (actualTime < now)
                                {
                                    actualTime = now;
                                }
                                
                                var timeDiff = actualTime - now;
                                var minutes = (int)Math.Round(timeDiff.TotalMinutes);
                                var message = string.Format(config.Message, minutes);
                                messages.Add(message);
                            }
                        }
                    }
                }
            }

            // Display all messages as plain text
            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }

            apiService.Dispose();
        }
    }
}
