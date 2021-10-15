using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterfaceModule
{
    static class Reader
    {
        public static bool Read (out string expression)
        {
            string input = Console.ReadLine();
            expression = input;
            if (input.Length == 0)
            {
                return false;
            }
            else
            {

                return true;
            }
        }

        public static bool Read(string path, out string expression)
        {
            StreamReader sr = new StreamReader(path);
            try
            {
                expression = sr.ReadLine();
                if (expression == null)
                {                    
                    return false;
                }    
                else
                {
                    return true;
                }
            }
            catch
            {
                expression = "";
                return false;
            }
        }
    }
}
