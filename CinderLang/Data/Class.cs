using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang.Data
{
    class Class
    {
        public static List<string> Get_Library_References(Dictionary<int, List<string>> class_lines)
        {
            foreach (int i in class_lines.Keys)
            {
                if (class_lines[i][0].ToLower() == "import")
                {

                }
            }
            return null;
        }
    }
}
