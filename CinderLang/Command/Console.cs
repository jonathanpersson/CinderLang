using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang.Command
{
    class Console
    {
        public static string[] Commandlist = { "print", "printl", "clear" };

        // Print.
        public static void Print(string text, ConsoleColor bg = ConsoleColor.Black, ConsoleColor fg = ConsoleColor.White)
        {
            System.Console.BackgroundColor = bg;
            System.Console.ForegroundColor = fg;
            System.Console.Write(text);
            System.Console.ResetColor();
        }

        // Print line.
        public static void Printl(string text, ConsoleColor bg = ConsoleColor.Black, ConsoleColor fg = ConsoleColor.White)
        {
            System.Console.BackgroundColor = bg;
            System.Console.ForegroundColor = fg;
            System.Console.WriteLine(text);
            System.Console.ResetColor();
        }

        // Clear.
        public static void Clear()
        {
            System.Console.Clear();
        }
    }
}
