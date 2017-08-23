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
                
                if (temp_item_list.Count > 0) file_items.Add(i, temp_item_list);
            }

            // Create class object for program.
            bool finished_class = false;

            do
            {
                var remove_range = (min: 0, max: 0);
                foreach (int line_number in file_items.Keys)
                {
                    if (file_items[line_number][0].ToLower() == "class" && remove_range.min == 0) remove_range.min = line_number; // Check for start of class.
                    else if (file_items[line_number][0].ToLower() == "end" && file_items[line_number][1].ToLower() == "class") remove_range.max = line_number; // Check for end of class.
                }

                Objects.Object new_class_object = new Objects.Object(); // Class object to create.
                new_class_object.Identifier = file_items[remove_range.min][1]; // Set class identifier.

                // Create function objects for class.
                var function_range = (from: 0, to: 0);
                for (int i = remove_range.min; i <= remove_range.max; i++)
                {
                    if (file_items[i][0].ToLower() == "function") function_range.from = i;
                    else if (file_items[i][0].ToLower() == "end" && file_items[i][1].ToLower() == "function") function_range.to = i;

                    if (function_range.from != 0 && function_range.to != 0)
                    {
                        Dictionary<int, List<string>> function_lines = new Dictionary<int, List<string>>();
                        Objects.Object function_object = new Objects.Object(); // Temporary function object.
                        function_object.Identifier = file_items[function_range.from][1]; // Set function identifier.

                        // Add function lines to function_lines.
                        for (int j = function_range.from + 1; j < function_range.to; i++) function_lines.Add(j, file_items[j]);

                        // MOVE THIS, CREATE CHILD OBJECTS AFTER FUNCTION OBJECT //
                        //// Go through function_lines and create objects for all child ruitines, add these to function_object.
                        //for (int j = function_range.to - 1; i > function_range.from; i--)
                        //{
                        //    if (Settings.statement_keywords.Contains(function_lines[j][0].ToLower())) // If-statement found.
                        //    {
                        //        string child_name = $"{function_lines[j][0].ToLower()}-{Math.Floor((Math.Sqrt(Environment.TickCount/2))).ToString("X")}";
                        //        string child_type = function_lines[j][0].ToLower();

                        //    }
                        //}

                        function_object.Lines = new Dictionary<int, List<string>>(function_lines);
                    }
                }
            }
            while (finished_class == false);
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
