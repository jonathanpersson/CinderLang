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
            while (true)
            {
                string[] menu_items = { "Directory Browser", "Option 1", "Option 2", "Option 3" };
                int selected_item = Draw_Menu(menu_items, "Ci Executor");

                switch (selected_item)
                {
                    case 0:
                        Directory_Browser();
                        break;
                }
            }
        }

        // Get current directory and draw a menu.
        static void Directory_Browser()
        {
            while (true)
            {
                string current_directory = System.IO.Directory.GetCurrentDirectory();
                string[] available_directories = System.IO.Directory.GetDirectories(current_directory);
                string[] available_files = System.IO.Directory.GetFiles(current_directory);
                List<string> menu_items_list = new List<string>(); menu_items_list.Add("Up");

                // Join strings.
                for (int i = 0; i <= available_directories.Length + available_files.Length - 2; i++)
                {
                    if (i <= available_directories.Length - 1) menu_items_list.Add($"<DIR> {available_directories[i].Replace(current_directory, "")}");
                    else if (i > available_directories.Length - 1) menu_items_list.Add($"<FIL> {available_files[i - available_directories.Length].Replace(current_directory, "")}");
                }

                menu_items_list.Add("Set custom directory");
                menu_items_list.Add("Exit");

                int selected_item = Draw_Menu(menu_items_list.ToArray(), "Directory Browser");

                if (selected_item == 0) System.IO.Directory.SetCurrentDirectory(System.IO.Directory.GetParent(current_directory).ToString()); // Up.
                else if (selected_item == menu_items_list.Count - 1) break; // Exit.
                else if (selected_item == menu_items_list.Count - 2) // Set custom directory.
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nEnter directory: ");
                    string new_dir = Console.ReadLine();
                    System.IO.Directory.SetCurrentDirectory(new_dir);
                    Console.ResetColor();
                }
                else if (selected_item <= available_directories.Length) System.IO.Directory.SetCurrentDirectory(available_directories[selected_item - 1]); // Move to dir.
                else Exec.Init_Program(available_files[selected_item - available_directories.Length - 1]); // Open file.
            }
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
