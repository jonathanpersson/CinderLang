using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinderLang.Math
{
    class Line
    {
        //Perform mathematical operations on line.
        public static dynamic Calculate(List<string> items, string return_as = "int")
        {
            List<string> converted_items = new List<string>(items);
            
            foreach (char op in Settings.mathematical_operators)
            {
                if (converted_items.Contains(op.ToString()))
                {
                    for (int i = 0; i < converted_items.Count(); i++)
                    {
                        if (converted_items[i] == op.ToString())
                        {
                            double previous = double.Parse(converted_items[i - 1]);
                            double next = double.Parse(converted_items[i + 1]);
                            double new_value = 0;

                            switch (op)
                            {
                                case '^':
                                    new_value = System.Math.Pow(previous, next);
                                    break;
                                case '*':
                                    new_value = previous * next;
                                    break;
                                case '/':
                                    new_value = previous / next;
                                    break;
                                case '+':
                                    new_value = previous + next;
                                    break;
                                case '-':
                                    new_value = previous - next;
                                    break;
                            }

                            // Add values to converted_items.
                            converted_items[i] = new_value.ToString();
                            converted_items.RemoveAt(i + 1);
                            converted_items.RemoveAt(i - 1);
                            break;
                        }
                    }
                }
            }

            return new List<string>(converted_items);
        }
    }
}