using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang.Objects
{
    class Object
    {
        private string _identifier = "";
        public string Identifier { get { return _identifier; } set { _identifier = value; } }
        public Dictionary<string, dynamic> Subs = new Dictionary<string, dynamic>();
        public Dictionary<int, List<string>> Args = new Dictionary<int, List<string>>();
    }
}
