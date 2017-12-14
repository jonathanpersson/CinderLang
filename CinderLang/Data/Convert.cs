using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang.Data
{
    class Convert
    {
        /// <summary>
        /// Conversions (of non-array/list type variables) to and from string.
        /// </summary>

        // Convert from string.
        public static dynamic From_String(string value, string data_type)
        {
            switch (data_type)
            {
                case "char":
                    return char.Parse(value);
                case "int":
                    return int.Parse(value);
                case "float":
                    return float.Parse(value);
                case "double":
                    return double.Parse(value);
                case "decimal":
                    return decimal.Parse(value);
                default:
                    return value;
            }
        }

        // Convert to string.
        public static string To_String(dynamic value) { return value.ToString(); }

        /// <summary>
        /// Conversions of lists and arrays.
        /// </summary>
    }
}
