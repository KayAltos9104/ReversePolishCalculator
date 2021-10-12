using System;

namespace CalculationModule
{
    class Program
    {
        static void Main()
        {
            ReversePolishNotation rpn = new ReversePolishNotation();
            Console.WriteLine(rpn.Calculate("1 2 3 2 + ^ +"));
        }
    }
}
