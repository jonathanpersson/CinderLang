using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinderLang;

namespace CinderExec
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] menu_items = { "Directory Browser" , "Option 1", "Option 2", "Option 3"};
            Draw_Menu(menu_items, "Ci Executor");
        }

        // Get current directory and draw a menu.
        static void Directory_Browser()
        {
            string current_directory = System.IO.Directory.GetCurrentDirectory();
            string[] available_directories = System.IO.Directory.GetDirectories(current_directory);
            string[] available_files = System.IO.Directory.GetFiles(current_directory);
            
            // Join strings.
            for ()
        }

        // Draw a menu.
        static int Draw_Menu(string[] menu_items, string title)
        {
            int current_pos = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{title}\n");

                for (int i = 0; i < menu_items.Length; i++)
                {
                    if (i == current_pos)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(menu_items[i]);
                    Console.ResetColor();
                }

                ConsoleKey key_press = Console.ReadKey().Key;

                switch (key_press)
                {
                    case ConsoleKey.UpArrow:
                        current_pos--;
                        break;
                    case ConsoleKey.DownArrow:
                        current_pos++;
                        break;
                    case ConsoleKey.Enter:
                        return current_pos;
                }

                if (current_pos < 0) current_pos = menu_items.Length - 1;
                if (current_pos >= menu_items.Length) current_pos = 0;
            }
        }
    }
}
