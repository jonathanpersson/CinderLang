using System;
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
        public static List<string> Get_Arguments(List<string> line, int start_index)
        {

        }
    }
}
