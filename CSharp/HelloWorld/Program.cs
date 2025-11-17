using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

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
            // Segment Display (commented out)
            var display = new SegmentDisplay16();
            string text = "LM";
            display.DisplayText(text);

            /*if (args.Length == 0)
            {
                Console.WriteLine("Usage: dotnet run <stop_id>");
                Console.WriteLine("Example: dotnet run 12345");
                return;
            }

            string stopId = args[0];
            var apiService = new TransseeApiService();
            
            var result = await apiService.GetStopInfoAsync(stopId);

            if (result != null)
            {
                // Dump the entire response as JSON
                var json = JsonSerializer.Serialize(result, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
                Console.WriteLine(json);
            }
            else
            {
                Console.WriteLine("Failed to get data from API.");
            }

            apiService.Dispose();*/
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
