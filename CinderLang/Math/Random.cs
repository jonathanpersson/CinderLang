using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang.Math
{
    class Random
    {

        private static readonly System.Random _rnd = new System.Random();

        // Generate GUID and return it as a Base64 string.
        public static string Generate_GUID()
        {
            Guid g = Guid.NewGuid();
            return Convert.ToBase64String(g.ToByteArray()).Replace("=", "");
        }

        // Generate a random integer.
        public static int Generate_Int(int min, int max)
        {
            int random_number = _rnd.Next(min, max);
            return random_number;
        }
    }
}
