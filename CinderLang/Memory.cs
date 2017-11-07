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
    }
}
