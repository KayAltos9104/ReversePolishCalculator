using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationModule
{

    class ReversePolishNotation
    {
        Dictionary<string, byte> signsDict = new Dictionary<string, byte>();

        public ReversePolishNotation()//Знаки в двух местах дублируются. Плохо. Надо как-то объединить с MathOperate
        {
            signsDict.Add("+", 1);
            signsDict.Add("-", 1);
            signsDict.Add("*", 2);
            signsDict.Add("/", 2);
            signsDict.Add("^", 3);
        }

        public bool RevPolNotTryParse(string input, out string revPolNotExpr)
        {
            List<string> parsedInput = new List<string>(input.Split(" "));
            Stack<string> stack = new Stack<string>();
            revPolNotExpr = "";
            foreach (var element in parsedInput)
            {
                double newNumber;
                if (double.TryParse(element, out newNumber))
                {
                    revPolNotExpr += newNumber + " ";
                }
                else if (element == "(")
                {
                    stack.Push(element);
                }
                else if (IsMathOperator(element))
                {
                    while (stack.Count != 0 &&IsMathOperator(stack.Peek()) && signsDict[element] <= signsDict[stack.Peek()])
                    {
                        revPolNotExpr += stack.Pop() + " ";
                    }

                    stack.Push(element);
                }
                else if (element == ")")
                {
                    while (stack.Peek() != "(")
                    {
                        if (stack.Count == 0)
                            return false;
                        else
                            revPolNotExpr += stack.Pop() + " ";
                    }
                    stack.Pop();//Выталкиваем открывающую скобку
                }
                else
                    return false;
            }
            while (stack.Count != 0)
                revPolNotExpr += stack.Pop() + " ";
            revPolNotExpr = revPolNotExpr.Trim();
            return true;
        }


        public double Calculate(string input)
        {
            string[] parsedInput = input.Split(" ");
            List<string> expression = new List<string>(parsedInput);
            Stack<double> stack = new Stack<double>();
            try
            {
                foreach (var element in expression)
                {
                    if (IsMathOperator(element))
                    {
                        if (stack.Count >= 2)//TODO: Сделать обработку для действий с одним операндом
                        {
                            double number1 = stack.Pop();
                            double number2 = stack.Pop();
                            double result;
                            if (MathOperate(element, number1, number2, out result))
                                stack.Push(result);
                            else
                                throw new Exception($"Неверная запись в выражении - {element}");
                        }
                        else
                        {
                            throw new Exception($"Не хватает операндов для действия {element}");
                        }
                    }
                    else
                    {
                        double newNumber;
                        if (double.TryParse(element, out newNumber))
                        {
                            stack.Push(newNumber);
                        }
                        else
                        {
                            throw new Exception($"В выражении присутствует не число и не оператор - {element}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка исходного выражения: {e.Message}");
            }

            return stack.Pop();
        }
        private bool IsMathOperator(string symbol)
        {
            foreach (var sign in signsDict)
                if (symbol == sign.Key) return true;
            return false;
        }
        private bool MathOperate(string mathOperator, double number1, double number2, out double result)
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
                        result = number1 - number2;
                        return true;
                    }
                case "*":
                    {
                        result = number1 * number2;
                        return true;
                    }
                case "/":
                    {
                        result = number1 / number2;
                        return true;
                    }
                case "^":
                    {
                        result = Math.Pow(number1, number2);
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
