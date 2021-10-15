using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterfaceModule
{
    interface IReader
    {
        bool ReadExpression(out string expression);
    }
}
