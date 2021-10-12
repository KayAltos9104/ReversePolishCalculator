using System;

namespace CalculationModule
{
    class Program
    {
        static void Main()
        {
            string expression;
            ReversePolishNotation.RevPolNotTryParse("2 * 2 + ( 8 / 4 ) ^ 3", out expression);
            //Console.WriteLine(rpn.Calculate("1 2 3 2 + ^ +"));
            ReversePolishNotation.Calculate(expression, out double answer, out string log);
            Console.WriteLine($"Ответ: {answer}; Лог расчетов: {log}");
        }
    }
}
