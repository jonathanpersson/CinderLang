﻿using System;
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
        public static string[] object_keywords = { "if", "switch", "for", "while", "do", "loop", "function", "mathf"};
        public static string[] named_objects = { "function", "mathf" };
        public static string[] variable_types = { "string", "char", "int", "float", "double", "decimal", "long" };
        public static string[] variable_mods = { "const", "dynamic" };

        // STRINGS.
        public static string[] special_characters_find = { " ", "(", ")", "[", "]", "{", "}", ".", ",", "^", "*", "/", "+", "-", "&" };
        public static string[] special_characters_replace = { "%space%", "%lpar%", "%rpar%", "%lsqb%", "%rsqb%", "%lcbr%", "%rcbr%", "%dot%",
        "%com%", "%flex%", "%ast%", "%slash%", "%plus%", "%dash%", "%amp%"};
        public static string split_characters = " .,^*/+-()[]{}&";
        public static string split_characters_exclude = " ";
        public static string subline_split_character = ",";
        public static string string_operators = "&";
        public static string mathematical_operators = "^*/+-";
        public static string[] logical_operators = { "<", ">", "=", "<=", ">=" };
        public static string[] special_logical_operators = { "A=", "O=", "NA=", "NO=", "X=", "XN=", "N=" };

        // DOUBLES, DECIMALS AND FLOATS.
        public static string double_prefix = "D";
        public static string decimal_prefix = "d";
        public static string float_prefix = "f";
    }
}
