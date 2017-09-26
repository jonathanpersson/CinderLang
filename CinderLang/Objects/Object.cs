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
        private Dictionary<string, string> _accessible_ids = new Dictionary<string, string>();

        // Properties.
        public string Identifier { get { return _identifier; } set { _identifier = value; } } // Identifier property.
        public string Type { get { return _type; } set { _type = value; } } // Type property.
        public Dictionary<int, List<string>> Args { get { return _args; } set { _args = value; } }
        public Dictionary<int, List<string>> Lines { get { return _lines; } set { _lines = value; } }

        // Add child ruitines to _children.
        public void Add_Children(Dictionary<string, dynamic> children_to_add, bool debug_out = false)
        {
            foreach (string child_id in children_to_add.Keys)
            {
                _children.Add(child_id, children_to_add[child_id]);
                if (debug_out == true) Console.WriteLine($"Child \"{child_id}\" added to \"{_identifier}\".");
            }
        }

        // Remove child ruitines from _children.
        public void Remove_Children(string[] children_to_remove, bool debug_out = false)
        {
            foreach (string child_id in children_to_remove)
            {
                _children.Remove(child_id);
                if (debug_out == true) Console.WriteLine($"Child \"{child_id}\" removed from \"{_identifier}\".");
            }
        }

        // Find and create child ruitines.
        public void Find_Children()
        {
            bool search_finished = false;
            do
            {
                //TODO:
                // -LOOK FOR CHILD RUITINES
                // -WHEN ONE IS FOUND MOVE ITS CODE INTO OBJECT, INSERT INTO MEMORY.
                // -BREAK OUT OF FOREACH LOOP.
                // -LOOP THIS UNTIL FINISHED.
                foreach (int line_number in Lines.Keys)
                {
                    int skipped_objects = 0;
                    string object_type = "";
                    bool found_current_object = false;
                    var object_range = (start: 0, end: 0);

                    foreach (string item in Lines[line_number])
                    {
                        if (Settings.object_keywords.Contains(item) && found_current_object == false)
                        {
                            found_current_object = true;
                            object_type = item;
                            object_range.start = line_number;
                        }
                        else if (Settings.object_keywords.Contains(item) && found_current_object == true) skipped_objects++;
                        else if (item == "end" && skipped_objects > 0) skipped_objects--;
                        else if (item == "end" && skipped_objects == 0)
                        {
                            object_range.end = line_number;
                            break;
                        }
                    }
                    //TODO:
                    // Create method for extracting arguments within parentheses.
                    // Read arguments and convert them into sublines.
                    // Get lines within object_range and add them to new_object_lines.
                    // Move backwards through object_range and remove lines from Lines.
                    // Replace first line within object_range with a function call for new_object_id.
                    // Add new_object_lines and arguments to new object.
                    // Name object and add to _accessible_ids and memory.
                    // Other stuff.
                    string new_object_id = Math.Random.Generate_GUID();
                    Dictionary<int, List<string>> new_object_lines = new Dictionary<int, List<string>>();
                }
            }
            while (search_finished == false);
        }
    }
}
