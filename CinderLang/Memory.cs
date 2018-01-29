using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang
{
    class Memory
    {
        public static Objects.Object program_object = new Objects.Object(); // Program object.
        //TODO: Add something to store global variables.

        // Get function
        public static Objects.Object Get_Function(string identifier)
        {
            string function_memory_id = program_object.Get_Memory_Identifier(identifier);
            return program_object.Get_Child_From_Identifier(function_memory_id);
        }

        // Get startup_object
        public static Objects.Object Get_Startup_Object()
        {
            return program_object.Get_Child_From_Identifier(program_object.Get_Child_From_Identifier
                (Settings.main_class_identifier).Get_Memory_Identifier($"{Settings.main_class_identifier}.Main"));
        }

        // Add Variables
        public static void Add_Variables(Dictionary<int, List<string>> variables, string parent = "")
        {
            Dictionary<string, dynamic> variables_to_add = new Dictionary<string, dynamic>();

            foreach (int i in variables.Keys)
            {
                Objects.Object var_obj = new Objects.Object();
                string var_id = "";
                string var_mod = "null";
                string var_type = "generic object";
                dynamic var_value = null;
                int last_index = 0;

                // Get variable name and type.
                for (int j = 0; j < variables[i].Count(); j++)
                {
                    string item = variables[i][j];
                    last_index = j;

                    if (Settings.variable_mods.Contains(item)) var_mod = item;
                    else if (Settings.variable_types.Contains(item)) var_type = item;
                    else
                    {
                        var_id = item;
                        break;
                    }
                }

                // Get variable value (if any).
                if (last_index < variables[i].Count() - 1)
                {
                    List<string> value_items = new List<string>();
                    bool start_adding = false;

                    for (int j = last_index + 1; j < variables[i].Count(); j++)
                    {
                        //string current_item = variables[i][j];
                        if (variables[i][j] == "=" && start_adding == false) start_adding = true;
                        else if (start_adding == true) value_items.Add(variables[i][j]);
                    }
                    var_value = Data.String.Convert_Items_To_String(Math.Line.Calculate(value_items));
                    var_value = Data.Convert.From_String(var_value, var_type);
                }

                // Add data to object.
                string memory_id = Math.Random.Generate_GUID();
                var_obj.Identifier = var_id;
                var_obj.Type = var_type;
                var_obj.Parent = parent;
                var_obj.V_Value = var_value;

                // Add memory id to variable parent.
                string parent_mem_id = program_object.Get_Memory_Identifier(parent); // Get parent's memory ID.
                program_object.Get_Child_From_Identifier(parent_mem_id).Add_Child_ID(var_obj.Identifier, memory_id); // Add variable to parent access. IDs.

                // Add object to variables_to_add.
                variables_to_add.Add(memory_id, var_obj);
            }

            // Add to program object.
            program_object.Add_Children(variables_to_add);
        }
    }
}
