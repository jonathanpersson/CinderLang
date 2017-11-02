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
        private string _function_return_type = "null";
        private Dictionary<string, dynamic> _children = new Dictionary<string, dynamic>(); // Children field. Used to store child ruitines in object.
        private Dictionary<int, List<string>> _args = new Dictionary<int, List<string>>(); // Args field.
        private Dictionary<int, List<string>> _lines = new Dictionary<int, List<string>>(); // Lines field.
        private Dictionary<string, string> _accessible_ids = new Dictionary<string, string>();

        // Properties.
        public string Identifier { get { return _identifier; } set { _identifier = value; } } // Identifier property.
        public string Type { get { return _type; } set { _type = value; } } // Type property.
        public string Function_Return_Type { get { return _function_return_type; } set { _function_return_type = value; } }
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

        // List child ruitines.
        public void List_Children()
        {
            foreach (string id in _children.Keys)
            {
                Console.WriteLine($"{_identifier}->{id}({_children[id].Identifier})");
                _children[id].List_Children();
            }
        }

        // Find and create child ruitines.
        public void Find_Children()
        {
            Dictionary<string, dynamic> child_ruitines = new Dictionary<string, dynamic>();
            bool search_finished = false;
            do
            {
                //TODO:
                // -LOOK FOR CHILD RUITINESx
                // -WHEN ONE IS FOUND MOVE ITS CODE INTO OBJECT, INSERT INTO MEMORY.x
                // -BREAK OUT OF FOREACH LOOP.x
                // -LOOP THIS UNTIL FINISHED.x
                // REMOVE THIS LATER, KEEPING JUST IN CASE.
                var object_range = (start: 0, end: 0);

                foreach (int line_number in _lines.Keys)
                {
                    int skipped_objects = 0;
                    string object_type = "";
                    bool found_current_object = false;
                    bool object_range_found = false;

                    foreach (string item in _lines[line_number])
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
                            object_range_found = true;
                            break;
                        }
                    }
                    if (object_range_found == true) break;
                }

                if (object_range.start != 0 && object_range.end != 0)
                {
                    string new_object_id = Math.Random.Generate_GUID(); // ID used to identify the object in memory.
                    string new_object_type = Data.Line.Get_Type(_lines[object_range.start]); // Object type.
                    string object_id = Data.Line.Get_Object_Name(_lines[object_range.start], new_object_type, new_object_id); // Get in-file object name.
                    string[] object_call = { new_object_id, "(", ")" }; // Object call. Inserted into code.
                    string return_type = "null";
                    Dictionary<int, List<string>> new_object_lines = new Dictionary<int, List<string>>();
                    Dictionary<int, List<string>> new_object_args = new Dictionary<int, List<string>>(Data.Line.Get_Sublines(Data.Line.Get_Arguments(
                        _lines[object_range.start])));
                    Object new_object = new Object();

                    // Get return type.
                    if (new_object_type == "function") return_type = _lines[object_range.start][_lines[object_range.start].Count - 1];

                    // Insert object_call.
                    _lines[object_range.start] = new List<string>(object_call);

                    // Add lines to new_object_lines.
                    for (int i = object_range.start + 1; i < object_range.end; i++) new_object_lines.Add(i, _lines[i]);

                    // Remove object_lines from _lines.
                    for (int i = object_range.end; i > object_range.start; i--) _lines.Remove(i);

                    // Add everything to new_object.
                    new_object.Identifier = object_id;
                    new_object.Lines = new_object_lines;
                    new_object.Args = new_object_args;
                    new_object.Type = new_object_type;
                    new_object.Function_Return_Type = return_type;

                    // Start adding into memory.
                    _accessible_ids.Add(object_id, new_object_id); // Add object to accessible IDs.
                    child_ruitines.Add(new_object_id, new_object); // These are added into memory upon finishing child ruitine search.
                }
                else search_finished = true;
            }
            while (search_finished == false);

            // Finish adding into memory.
            foreach (Object child in child_ruitines.Values)
            {
                child.Find_Children();
            }
            Memory.program_object.Add_Children(child_ruitines);
        }
    }
}
