using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang.Data
{
    class String
    {
        // Convert items to string.
        public static string Convert_Items_To_String (List<string> items)
        {
            List<string> converted_items = new List<string>(items);
            
            while (converted_items.Count() > 1)
            {
                for (int i = 1; i < items.Count() - 1; i++)
                {
                    string item_prev = converted_items[i - 1];
                    string item_current = converted_items[i];
                    string item_next = converted_items[i + 1];
                    string new_value = "";

                    if (Settings.string_operators.Contains(item_current))
                    {
                        switch (item_current)
                        {
                            case "&":
                                new_value = item_prev + item_next;
                                break;
                        }

                        converted_items[i - 1] = new_value;
                        converted_items.RemoveRange(i, 2);
                        break;
                    }
                }
            }

            return converted_items[0];
        }
    }
}
