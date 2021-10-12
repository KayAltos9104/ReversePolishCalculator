using System;
using CalculationModule;

namespace ConsoleInterfaceModule
{
    class Program
    {
        static void Main()
        {
            //ReversePolishNotation.TryParse("2 * 2 + ( 8 / 4 ) ^ 3", out string expression);
            ReversePolishNotation.TryParse("2*2+(8/4 ) ^ 3", out string expression);
            //Console.WriteLine(rpn.Calculate("1 2 3 2 + ^ +"));
            ReversePolishNotation.Calculate(expression, out double answer, out string log);
            Console.WriteLine($"Ответ: {answer}; Лог расчетов: {log}");
        }
    }
}
