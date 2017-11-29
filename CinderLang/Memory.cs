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

        // Get startup_object
        public static Objects.Object Get_Startup_Object()
        {
            return program_object.Get_Child_From_Identifier(program_object.Get_Child_From_Identifier
                (Settings.main_class_identifier).Get_Memory_Identifier("Main"));
        }

        // Add Variables
        public static void Add_Variables(Dictionary<int, List<string>> variables)
        {
            foreach (int i in variables.Keys)
            {
                string var_id = "";
                string var_mod = "null";
                string var_type = "generic object";
                dynamic var_value;
                int last_index = 0;

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
                    var_value = 
                }
            }
        }
    }
}
