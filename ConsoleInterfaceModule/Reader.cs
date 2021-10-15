using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterfaceModule
{
    static class Reader
    {
        public static bool Read (out string expression)
        {
            Console.Write("Введите выражение для расчета: ");
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

        public static bool Read(string path, out string expression)
        {
            StreamReader sr = new StreamReader(path);
            try
            {
                expression = sr.ReadLine();
                if (expression == null)
                {                    
                    return false;
                }    
                else
                {
                    return true;
                }
            }
            catch
            {
                expression = "";
                return false;
            }
        }

        public static string GetModeName (ReadMode mode)
        {
            switch (mode)
            {
                case ReadMode.console:
                    {
                        return "Консоль";
                    }
                case ReadMode.textFile:
                    {
                        return "Текстовый файл";
                    }
                default:
                    {
                        return "Неизвестный режим";
                    }
            }
        }

        public enum ReadMode: byte
        {
            console = 0,
            textFile = 1
        }
    }
}
