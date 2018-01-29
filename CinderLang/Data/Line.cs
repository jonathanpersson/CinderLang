﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang.Data
{
    class Line
    {
        // Split line into sublines.
        public static Dictionary<int, List<string>> Get_Sublines(List<string> line, string split_character_override = "")
        {
            Dictionary<int, List<string>> subline_dictionary = new Dictionary<int, List<string>>();
            List<string> current_subline = new List<string>();
            int current_subline_index = 0;
            string split_character = Settings.subline_split_character;

            if (split_character_override != "") split_character = split_character_override;

            for (int i = 0; i < line.Count(); i++)
            {
                if (line[i] == split_character)
                {
                    subline_dictionary.Add(current_subline_index++, new List<string>(current_subline));
                    current_subline = new List<string>();
                }
                else current_subline.Add(line[i]);
            }

            if (current_subline.Count != 0) subline_dictionary.Add(current_subline_index++, new List<string>(current_subline));

            return new Dictionary<int, List<string>>(subline_dictionary);
        }

        //Get arguments from line.
        public static List<string> Get_Arguments(List<string> line, int start_index = 0)
        {
            int found_openings = 0;
            var arg_range = (start: 0, end: 0);
            List<string> arg_list = new List<string>();

            for (int i = start_index; i < line.Count(); i++)
            {
                if (line[i] == "(" && found_openings == 0 && arg_range.start == 0) arg_range.start = i;
                else if (line[i] == "(") found_openings++;
                else if (line[i] == ")" && found_openings > 0) found_openings--;
                else if (line[i] == ")" && found_openings == 0)
                {
                    arg_range.end = i;
                    break;
                }
            }

            for (int i = arg_range.start + 1; i < arg_range.end; i++) arg_list.Add(line[i]);

            return new List<string>(arg_list);
        }

        // Get type from line.
        public static string Get_Type(List<string> line)
        {
            foreach (string item in line)
            {
                if (Settings.object_keywords.Contains(item.ToLower())) return item.ToLower();
            }

            return "generic";
        }

        // Get in-file object name.
        public static string Get_Object_Name(List<string> line, string type, string new_id = "")
        {
            if (Settings.named_objects.Contains(type))
            {
                for (int i = 0; i < line.Count; i++) if (line[i] == type) return line[i + 1];
            }
            return new_id;
        }

        ///<summary>
        /// Replace variable calls in a line with their actual values.
        ///</summary>
        public static List<string> Replace_Variables(List<string> line, Dictionary<string, string> accessible_ids)
        {
            List<string> new_line = new List<string>(line);
            for (int i = 0; i < new_line.Count; i++)
            {
                string item = new_line[i];
                if (accessible_ids.Keys.Contains(item))
                {
                    new_line[i] = Memory.program_object.Get_Child_From_Identifier(accessible_ids[item]).V_Value.ToString();
                }
            }

            return new List<string>(new_line);
        }
    }
}
