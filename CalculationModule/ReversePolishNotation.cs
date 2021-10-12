using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationModule
{

    public static class ReversePolishNotation
    {
        public static bool RevPolNotTryParse(string input, out string postFixNotation)
        {
            List<string> parsedInput = new List<string>(input.Split(" "));
            Stack<string> stack = new Stack<string>();
            postFixNotation = "";
            foreach (var element in parsedInput)
            {                
                if (double.TryParse(element, out double newNumber))
                {
                    postFixNotation += newNumber + " ";
                }
                else if (element == "(")
                {
                    stack.Push(element);
                }
                else if (MathParser.IsMathOperator(element))
                {
                    while (stack.Count != 0 && MathParser.IsMathOperator(stack.Peek()) && MathParser.signsDict[element] <= MathParser.signsDict[stack.Peek()])
                    {
                        postFixNotation += stack.Pop() + " ";
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
                            postFixNotation += stack.Pop() + " ";
                    }
                    stack.Pop();//Выталкиваем открывающую скобку
                }
                else
                    return false;
            }
            while (stack.Count != 0)
                postFixNotation += stack.Pop() + " ";

            postFixNotation = postFixNotation.Trim();
            return true;
        }



        public static bool Calculate(string input, out double answer, out string log)
        {
            string[] parsedInput = input.Split(" ");
            log = "Вычисление прошло успешно";
            List<string> expression = new List<string>(parsedInput);
            Stack<double> stack = new Stack<double>();

            foreach (var element in expression)
            {
                if (MathParser.IsMathOperator(element))
                {
                    if (stack.Count >= 2)//TODO: Сделать обработку для действий с одним операндом
                    {
                        double number1 = stack.Pop();
                        double number2 = stack.Pop();                       
                        if (MathParser.MathOperate(element, number1, number2, out double result))
                        {
                            stack.Push(result);
                        }
                        else
                        {
                            log = $"Неверная запись в выражении - {element}";
                            answer = 0;
                            return false;
                        }
                    }
                    else
                    {                        
                        log = $"Не хватает операндов для действия {element}";
                        answer = 0;
                        return false;
                    }
                }
                else
                {                   
                    if (double.TryParse(element, out double newNumber))
                    {
                        stack.Push(newNumber);
                    }
                    else
                    {                        
                        log = $"В выражении присутствует не число и не оператор - {element}";
                        answer = 0;
                        return false;
                    }
                }
            }

            answer = stack.Pop();
            return true;
        }
       
    }
}
