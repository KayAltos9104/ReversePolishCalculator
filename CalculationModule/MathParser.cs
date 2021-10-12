using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationModule
{
    static class MathParser
    {
        public static readonly Dictionary<string, byte> signsDict = new ()
        {
            { "+", 1 },
            { "-", 1 },
            { "*", 2 },
            { "/", 2 },
            { "^", 3 }
        };

        public static bool IsMathOperator(string symbol)
        {
            return signsDict.ContainsKey(symbol);            
        }
        public static bool MathOperate(string mathOperator, double number1, double number2, out double result)
        {
            switch (mathOperator)
            {
                case "+":
                    {
                        result = number1 + number2;
                        return true;
                    }
                case "-":
                    {
                        result = number2 - number1;
                        return true;
                    }
                case "*":
                    {
                        result = number1 * number2;
                        return true;
                    }
                case "/":
                    {
                        result = number2 / number1;
                        return true;
                    }
                case "^":
                    {
                        result = Math.Pow(number2, number1);
                        return true;
                    }
                default:
                    {
                        result = 0;
                        return false;
                    }
            }
        }
    }
}
