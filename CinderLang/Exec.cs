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
        public static void init_program(string file)
        {
            
        }

        // Import program/class into memory.
        public static void import_program(string file, bool override_class_name = false, string new_class_name = "")
        {
            string[] file_lines = FileSys.File.read_lines(file); // Read lines from file.
            Dictionary<int, List<string>> file_items = new Dictionary<int, List<string>>();
            Dictionary<string, dynamic> found_classes = new Dictionary<string, dynamic>();

            // Split lines into items and add them to file_items.
            for (int i = 1; i <= file_lines.Length; i++)
            {
                List<string> temp_item_list = new List<string>(split_string_into_items(file_lines[i - 1]));
                
                if (temp_item_list.Count > 0) file_items.Add(i, temp_item_list);
            }

            // Create class object for program.
            bool finished_class = false;
            do
            {
                var class_range = (min: 0, max: 0);
                foreach (int line_number in file_items.Keys)
                {
                    if (file_items[line_number][0].ToLower() == "class" && class_range.min == 0) class_range.min = line_number; // Check for start of class.
                    else if (file_items[line_number][0].ToLower() == "end" && file_items[line_number][1].ToLower() == "class") class_range.max = line_number; // Check for end of class.
                }

                if (class_range.max != 0)
                {
                    Dictionary<int, List<string>> class_lines = new Dictionary<int, List<string>>(); // Lines to add to class object.
                    Objects.Object new_class_object = new Objects.Object(); // Class object to create.
                    new_class_object.Identifier = file_items[class_range.min][1]; // Set class identifier.
                    new_class_object.Type = "class";

                    // Add lines to class and remove lines from file_items. Then add class object to found_classes.
                    for (int i = class_range.min + 1; i < class_range.max; i++)
                    {
                        class_lines.Add(i, file_items[i]);
                        file_items.Remove(i);
                    }
                    file_items.Remove(class_range.min); file_items.Remove(class_range.max); // Remove start and end of class.
                    new_class_object.Lines = new Dictionary<int, List<string>>(class_lines);
                    found_classes.Add(new_class_object.Identifier, new_class_object);
                }
                else finished_class = true;
            }
            while (finished_class == false);

            // Add classes to program_object.
            Memory.program_object.add_children(found_classes);
        }

        // Temporary - Move later.
        public static List<string> split_string_into_items(string string_to_split, bool ignore_inline_strings = false)
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
