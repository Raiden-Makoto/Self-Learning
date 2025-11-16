using System;
using System.Collections.Generic;

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
        static void Main(string[] args)
        {
            var display = new SegmentDisplay16();
            string text = "0123456789";
            display.DisplayText(text);
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
