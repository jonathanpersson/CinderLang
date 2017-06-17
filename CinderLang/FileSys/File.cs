using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang.FileSys
{
    class File
    {
        // Read lines from file.
        public static string[] read_lines (string file)
        {
            return System.IO.File.ReadAllLines(file);
        }
    }
}
