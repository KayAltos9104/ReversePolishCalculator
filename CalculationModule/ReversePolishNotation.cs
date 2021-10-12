using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationModule
{

    public static class ReversePolishNotation
    {
        public static bool TryParse(string input, out string postFixNotation)
        {
            input = input.Replace(" ", "");
            List<string> splitInput = new();
            Queue<string> buffer = new ();
            for (int i = 0; i< input.Length;i++)
            {
                if (double.TryParse(input[i].ToString(), out _)&&i!= input.Length-1)
                {
                    buffer.Enqueue(input[i].ToString());
                }
                else
                {
                    string number = "";
                    while (buffer.Count !=0)
                    {
                        number += buffer.Dequeue();
                    }
                    if (number != "")
                        splitInput.Add(number);

                    splitInput.Add(input[i].ToString());
                }
            }
            
            Stack<string> stack = new ();
            postFixNotation = "";
            foreach (var element in splitInput)
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
            string[] splitInput = input.Split(" ");
            log = "Вычисление прошло успешно";
            List<string> expression = new (splitInput);
            Stack<double> stack = new ();

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
                        log = $"В выражении присутствует не число и не оператор";
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
