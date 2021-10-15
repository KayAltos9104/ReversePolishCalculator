using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterfaceModule
{
    class ConsoleReader: IReader
    {
        public bool ReadExpression(out string expression)
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
    }
}
