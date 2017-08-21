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
        private string _type = "generic object";
        private Dictionary<string, dynamic> _children = new Dictionary<string, dynamic>(); // Children field. Used to store child ruitines in object.
        private Dictionary<int, List<string>> _args = new Dictionary<int, List<string>>(); // Args field.
        private Dictionary<int, List<string>> _lines = new Dictionary<int, List<string>>(); // Lines field.

        // Properties.
        public string Identifier { get { return _identifier; } set { _identifier = value; } } // Identifier property.
        public string Type { get { return _type; } set { _type = value; } } // Type property.
        public Dictionary<int, List<string>> Args { get { return _args; } set { _args = value; } }
        public Dictionary<int, List<string>> Lines { get { return _lines; } set { _lines = value; } }

        // Add child ruitines to _children.
        public void add_children (Dictionary<string, dynamic> children_to_add, bool debug_out = false)
        {
            foreach (string child_id in children_to_add.Keys)
            {
                _children.Add(child_id, children_to_add[child_id]);
                if (debug_out == true) Console.WriteLine($"Child \"{child_id}\" added to \"{_identifier}\".");
            }
        }

        // Remove child ruitines from _children.
        public void remove_children (Dictionary<string, dynamic> children_to_remove, bool debug_out = false)
        {
            foreach (string child_id in children_to_remove.Keys)
            {
                _children.Remove(child_id);
                if (debug_out == true) Console.WriteLine($"Child \"{child_id}\" removed from \"{_identifier}\".");
            }
        }
    }
}
