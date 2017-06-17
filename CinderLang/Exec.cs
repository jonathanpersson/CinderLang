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
        public static void init_program (string file)
        {
            
        }

        // Import program/class into memory.
        public static void import_program (string file, bool override_class_name = false, string new_class_name = "")
        {
            string[] file_lines = FileSys.File.read_lines(file); // Read lines from file.
            Dictionary<int, List<string>> file_items = new Dictionary<int, List<string>>();

            // Split lines into items and add them to file_items.
            for (int i = 1; i <= file_lines.Length; i++)
            {
                List<string> temp_item_list = new List<string>(split_string_into_items(file_lines[i - 1]));
                
                if (temp_item_list.Count > 0) { file_items.Add(i, temp_item_list); }
            }

            // Create class object for program.

        }

        // Temporary - Move later.
        public static List<string> split_string_into_items (string string_to_split, bool ignore_inline_strings = false)
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
