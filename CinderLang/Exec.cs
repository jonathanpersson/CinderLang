using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace CinderLang
{
    public class Exec
    {
        // Start running program from file.
        public static void Init_Program(string file)
        {
            Memory.program_object.Identifier = "PROGRAM";
            Import_Program(file);

            // Print Memory Contents.
            Console.WriteLine("LISTING MEMORY CONTENTS...");
            Console.WriteLine("FORMAT: BASE OBJ ID->TYPE->MEMORY ID->ACTUAL ID\n");
            Memory.program_object.List_Children();
            Console.WriteLine("\nEND OF MEMORY CONTENTS");

            int exit_code = Run_Object(Memory.Get_Startup_Object());

            Console.WriteLine("Execution finished. Press any key to continue.");
            Console.ReadKey();

            // Clear environment and memory.
            Console.Clear();
            Console.WriteLine("Clearing program object...");
            Memory.program_object = new Objects.Object();
            Console.WriteLine("Clearing Settings...");
            Settings.main_class_identifier = "";
        }

        // Run Object
        public static dynamic Run_Object(Object obj)
        {
            return 0;
        }

        // Import program/class into memory.
        public static void Import_Program(string file, bool override_class_name = false, string new_class_name = "")
        {
            string[] file_lines = FileSys.File.read_lines(file); // Read lines from file.
            Dictionary<int, List<string>> file_items = new Dictionary<int, List<string>>();
            Dictionary<string, dynamic> found_classes = new Dictionary<string, dynamic>();

            // Split lines into items and add them to file_items.
            for (int i = 1; i <= file_lines.Length; i++)
            {
                List<string> temp_item_list = new List<string>(Split_String_Into_Items(file_lines[i - 1]));
                
                if (temp_item_list.Count > 0) file_items.Add(i, temp_item_list);
            }

            // Create class object for program.
            bool finished_class = false;
            do
            {
                List<int> object_range = new List<int>();
                int object_definition = 0;
                int object_end = 0;
                bool inside_object = false;

                foreach (int line_number in file_items.Keys)
                {
                    if (file_items[line_number][0].ToLower() == "class" && inside_object == false) // Check for start of class.
                    {
                        inside_object = true; 
                        object_definition = line_number;
                    }
                    else if (file_items[line_number][0].ToLower() == "end" && file_items[line_number][1].ToLower() == "class")// Check for end of class.
                    {
                        inside_object = false;
                        object_end = line_number;
                    }
                    else if (inside_object == true) object_range.Add(line_number);
                }

                if (object_range.Count != 0)
                {
                    Dictionary<int, List<string>> class_lines = new Dictionary<int, List<string>>(); // Lines to add to class object.
                    Objects.Object new_class_object = new Objects.Object(); // Class object to create.
                    new_class_object.Identifier = file_items[object_definition][1]; // Set class identifier.
                    new_class_object.Type = "class";

                    // Add lines to class and remove lines from file_items. Then add class object to found_classes.
                    foreach (int i in object_range)
                    {
                        class_lines.Add(i, file_items[i]);
                        file_items.Remove(i);
                    }

                    file_items.Remove(object_definition); file_items.Remove(object_end); // Remove start and end of class.
                    new_class_object.Lines = new Dictionary<int, List<string>>(class_lines);

                    // Import child objects of class into memory.
                    new_class_object.Find_Children();

                    found_classes.Add(new_class_object.Identifier, new_class_object);

                    // Set main class identifier.
                    if (Settings.main_class_identifier == "") Settings.main_class_identifier = new_class_object.Identifier;
                }
                else finished_class = true;
            }
            while (finished_class == false);

            // Import library references.
            //List<string> library_references = new List<string>(find_references());

            // Add classes to program_object.
            Memory.program_object.Add_Children(found_classes);
        }

        // Temporary - Move later.
        public static List<string> Split_String_Into_Items(string string_to_split, bool ignore_inline_strings = false)
        {
            string altered_string = string_to_split.Trim('\t');
            List<string> string_items = new List<string>();

            // Go through string and make in-line strings compatible.
            if (ignore_inline_strings == false && altered_string.Contains("\""))
            {
                foreach (Match match in Regex.Matches(altered_string, "\"([^\"]*)\""))
                {
                    string match_string = match.ToString();
                    
                    for (int i = 0; i < Settings.special_characters_find.Length; i++)
                    {
                        if (match_string.Contains(Settings.special_characters_find[i]))
                        {
                            match_string = match_string.Replace(Settings.special_characters_find[i], Settings.special_characters_replace[i]);
                        }
                    }

                    altered_string = altered_string.Replace(match.ToString(), match_string);
                }
            }

            // Check if line is a comment, or empty.
            if (altered_string.StartsWith("//") == false && altered_string != "")
            {
                string current_item = "";

                foreach (char c in altered_string)
                {
                    if (Settings.split_characters.Contains(c))
                    {
                        if (current_item != "")
                        {
                            string_items.Add(current_item);
                            current_item = "";
                        }
                        
                        if (Settings.split_characters_exclude.Contains(c) == false)
                        {
                            string_items.Add(c.ToString());
                        }
                    }
                    else { current_item += c; }
                }

                if (current_item != "") { string_items.Add(current_item); }
            }

            return string_items;
        }
    }
}
