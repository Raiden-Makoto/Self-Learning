using System.Collections.Generic;

namespace HelloWorld
{
    static class SegmentDisplayLetters
    {
        public static Dictionary<char, string[]> GetLetters()
        {
            var letters = new Dictionary<char, string[]>();

            // Space
            letters[' '] = new string[] {
                "     ",
                "     ",
                "     ",
                "     ",
                "     "
            };

            // Numbers 0-9
            letters['0'] = new string[] {
                "|\u203E\u203E\u203E| ",
                "|   | ",
                "|   | ",
                "|   | ",
                "|___| "
            };

            letters['1'] = new string[] {
                " /|   ",
                "/ |   ",
                "  |   ",
                "  |   ",
                "__|__ "
            };

            letters['2'] = new string[] {
                "\u203E\u203E\u203E\u203E| ",
                "    | ",
                "----| ",
                "|     ",
                "|____ " 
            };

            letters['3'] = new string[] {
                "\u203E\u203E\u203E\u203E| ",
                "    | ",
                "----| ",
                "    | ",
                "____| "
            };

            letters['4'] = new string[] {
                "|   | ",
                "|   | ",
                " \u203E\u203E\u203E| ",
                "    | ",
                "    | "
            };

            letters['5'] = new string[] {
                "|\u203E\u203E\u203E\u203E ",
                "|     ",
                "|---- ",
                "    | ",
                "____| "
            };

            letters['6'] = new string[] {
                "|\u203E\u203E\u203E ",
                "|     ",
                "|---| ",
                "|   | ",
                "|___| "
            };

            letters['7'] = new string[] {
                "\u203E\u203E\u203E\u203E| ",
                "   | ",
                "   | ",
                "   | ",
                "   | "
            };

            letters['8'] = new string[] {
                "|\u203E\u203E\u203E| ",
                "|   | ",
                "|---| ",
                "|   | ",
                "|___| "
            };

            letters['9'] = new string[] {
                "|\u203E\u203E\u203E| ",
                "|   | ",
                "|---| ",
                "    | ",
                "____| "
            };

            // Letters A-Z
            letters['a'] = new string[] {
                "|\u203E\u203E\u203E| ",
                "|   | ",
                "|---| ",
                "|   | ",
                "|   | ",
            };

            letters['b'] = new string[] {
                "|\u203E\u203E\u203E\\ ",
                "|   / ",
                "|\u203E\u203E\u203E\\ ",
                "|   | ",
                "|___/ "
            };

            letters['c'] = new string[] {
                "|\u203E\u203E\u203E\u203E ",
                "|     ",
                "|     ",
                "|     ",
                "|____ "
            };

            letters['d'] = new string[] {
                "|\u203E\u203E\\  ",
                "|   \\ ",
                "|   | ",
                "|   / ",
                "|__/  "
            };

            letters['e'] = new string[] {
                "|\u203E\u203E\u203E\u203E ",
                "|     ",
                "|---- ",
                "|     ",
                "|____ "
            };

            letters['f'] = new string[] {
                "|\u203E\u203E\u203E\u203E ",
                "|     ",
                "|---- ",
                "|     ",
                "|     "
            };

            letters['g'] = new string[] {
                "|\u203E\u203E\u203E\u203E ",
                "|     ",
                "| --| ",
                "|   | ",
                "|___| "
            };

            letters['h'] = new string[] {
                "|   | ",
                "|   | ",
                "|---| ",
                "|   | ",
                "|   | "
            };

            letters['i'] = new string[] {
                "\u203E\u203E|\u203E\u203E ",
                "  |   ",
                "  |   ",
                "  |   ",
                "__|__ "
            };

            letters['j'] = new string[] {
                "\u203E\u203E|\u203E\u203E ",
                "  |   ",
                "  |   ",
                "| |   ",
                "\\_/   "
            };

            letters['k'] = new string[] {
                "|   / ",
                "|  /  ",
                "|--   ",
                @"|  \  ",
                @"|   \ "
            };

            letters['l'] = new string[] {
                "|     ",
                "|     ",
                "|     ",
                "|     ",
                "|____ "
            };

            letters['m'] = new string[] {
                @"|\ /| ",
                @"| | |",
                "|   | ",
                "|   | ",
                "|   | "
            };

            letters['n'] = new string[] {
                "     ",
                "|\\| |",
                "| |\\|",
                "| | |",
                "     "
            };

            letters['o'] = new string[] {
                " _ _ ",
                "|   |",
                "     ",
                "|   |",
                " - - "
            };

            letters['p'] = new string[] {
                " _ _ ",
                "|\\|/|",
                " -|- ",
                "|     ",
                "     "
            };

            letters['q'] = new string[] {
                " _ _ ",
                "|\\|/|",
                "| | |",
                "|/|\\|",
                " - - "
            };

            letters['r'] = new string[] {
                " _ _ ",
                "|\\|/|",
                " -|- ",
                "| |  ",
                "     "
            };

            letters['s'] = new string[] {
                " _ _ ",
                "|/   ",
                " -|- ",
                "  |\\|",
                " - - "
            };

            letters['t'] = new string[] {
                " _ _ ",
                "  |  ",
                "  |  ",
                "  |  ",
                "     "
            };

            letters['u'] = new string[] {
                "     ",
                "|   |",
                "|   |",
                "|   |",
                " - - "
            };

            letters['v'] = new string[] {
                "     ",
                "|   |",
                "|   |",
                " \\/  ",
                "     "
            };

            letters['w'] = new string[] {
                "     ",
                "|   |",
                "|   |",
                @"|/|\|",
                " - - "
            };

            letters['x'] = new string[] {
                "     ",
                @" \/  ",
                "  |  ",
                @" /\  ",
                "     "
            };

            letters['y'] = new string[] {
                "     ",
                "|   |",
                " -|- ",
                "  |  ",
                "     "
            };

            letters['z'] = new string[] {
                " _ _ ",
                "  /| ",
                " - - ",
                "|/   ",
                " - - "
            };

            return letters;
        }
    }
}

