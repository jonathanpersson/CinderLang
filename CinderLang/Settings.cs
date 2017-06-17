using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang
{
    class Settings
    {
        // CURRENT PROGRAM.
        public static string main_class_identifier = "";
        public static List<string> references_to_add = new List<string>();

        // ENVIRONMENT SETTINGS.
        public static bool global_debug_out = false;

        // STRINGS.
        public static string[] special_characters_find = { " ", "(", ")", "[", "]", "{", "}", ".", ",", "^", "*", "/", "+", "-", "&" };
        public static string[] special_characters_replace = { "%space%", "%lpar%", "%rpar%", "%lsqb%", "%rsqb%", "%lcbr%", "%rcbr%", "%dot%",
        "%com%", "%flex%", "%ast%", "%slash%", "%plus%", "%dash%", "%amp%"};
        public static string split_characters = " .,^*/+-()[]{}&";
        public static string split_characters_exclude = " ";
    }
}
