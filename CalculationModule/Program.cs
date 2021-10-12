using System;

namespace CalculationModule
{
    class Program
    {
        static void Main()
        {
            ReversePolishNotation rpn = new ReversePolishNotation();
            string expression;
            rpn.RevPolNotTryParse("2 * 2 + ( 4 * 4 )", out expression);
            //Console.WriteLine(rpn.Calculate("1 2 3 2 + ^ +"));
            Console.WriteLine(rpn.Calculate(expression));
        }
    }
}
