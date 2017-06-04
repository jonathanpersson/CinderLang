using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang.Objects
{
    class Object
    {
        // Fields.
        private string _identifier = ""; // Identifier field.
        private Dictionary<string, dynamic> _subs = new Dictionary<string, dynamic>(); // Subs field. Used to store subruitines in object.
        private Dictionary<int, List<string>> _args = new Dictionary<int, List<string>>(); // Args field.
        private Dictionary<int, List<string>> _lines = new Dictionary<int, List<string>>(); // Lines field.

        // Properties.
        public string Identifier { get { return _identifier; } set { _identifier = value; } } // Identifier property.

        // Add subs to _subs.
        public void add_subs (Dictionary<string, dynamic> subs_to_add, bool debug_out = false)
        {
            foreach (string sub_id in subs_to_add.Keys)
            {
                _subs.Add(sub_id, subs_to_add[sub_id]);
                if (debug_out == true) { Console.WriteLine($"Sub \"{sub_id}\" added to \"{_identifier}\"."); }
            }
        }

        // Remove subs from _subs.
        public void remove_subs (Dictionary<string, dynamic> subs_to_remove, bool debug_out = false)
        {
            foreach (string sub_id in subs_to_remove.Keys)
            {
                _subs.Remove(sub_id);
                if (debug_out == true) { Console.WriteLine($"Sub \"{sub_id}\" removed from \"{_identifier}\"."); }
            }
        }
    }
}
