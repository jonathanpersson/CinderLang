using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang.Command
{
    class Func
    {
        // Get command.
        public static dynamic Get_Command(string command, List<string> args)
        {
            dynamic return_value = null; // Return value. Default = null.

            // Check whether command is a language command or an imported function.
            if (Console.Commandlist.Contains(command.ToLower()))
            {
                //TODO: Change method (not the programming kind) later.
                switch (command.ToLower())
                {
                    case "print": //TEMP.
                        Console.Print(args[0]);
                        break;
                    case "printl":
                        Console.Printl(args[0]);
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                }
            }
            else
            {
                // Get function from memory.
                Exec.Run_Object(Memory.Get_Function(command));
            }

            return return_value;
        }

        // Run from memory.
        public static dynamic Run_From_Memory(dynamic function)
        {
            return null;
        }
    }
}
