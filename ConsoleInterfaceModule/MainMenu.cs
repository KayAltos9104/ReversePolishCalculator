using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculationModule;

namespace ConsoleInterfaceModule
{
    class MainMenu
    {
        int cursor;

        Reader.ReadMode currentReadMode;

        public delegate void ConsoleHandler (object sender, ConsoleMenuEventHandler e);
        public event ConsoleHandler Push;

        readonly Dictionary<int, string> menuItemsNames; //TODO: Фигово, конечно, но я пока не придумал, как соединить названия, айдишники
        //и чтобы делегат еще можно было пришпандорить
        readonly Dictionary<int, ConsoleHandler> menuItemsDict; 

        public MainMenu ()
        {
            menuItemsNames = new Dictionary<int, string>();
            menuItemsDict = new Dictionary<int, ConsoleHandler>();

            menuItemsNames.Add(0, "Посчитать выражение");
            menuItemsDict.Add(0, Calculate);

            menuItemsNames.Add(1, "Переключить режим считывания");
            menuItemsDict.Add(1, SwitchReadMode);

            menuItemsNames.Add(2, "Выйти из программы");
            menuItemsDict.Add(2, Exit);

            Push += menuItemsDict[cursor];
            currentReadMode = Reader.ReadMode.console;
        }
        //TODO: Разбить на модули
        public void LaunchMainCycle ()
        {
            ConsoleKeyInfo key;
           
            do
            {
                Console.WriteLine("Используйте стрелки, чтобы переключаться между пунктами, пробел - чтобы выбрать");
                foreach (var item in menuItemsNames)
                {
                    Console.WriteLine("> " + item.Value);
                }
                Console.WriteLine();
                Console.WriteLine($"Текущий режим ввода выражения: {Reader.GetModeName(currentReadMode)}");
                Console.WriteLine("Выбрано: " + menuItemsNames[cursor]);

                key = Console.ReadKey();
                Console.Clear();
                switch (key.Key)
                {                    
                    case ConsoleKey.UpArrow:
                        {
                            SwitchCursor(isUp: true);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            SwitchCursor(isUp: false);
                            break;
                        }
                    case ConsoleKey.Spacebar:
                        {
                            Push.Invoke(this, new ConsoleMenuEventHandler());
                            break;
                        }                    
                }
            } while (key.Key!=ConsoleKey.Escape);        
        }

        private void SwitchCursor(bool isUp)
        {
            Push -= menuItemsDict[cursor];
            if (isUp)
                SetCursor(cursor + 1);
            else
                SetCursor(cursor - 1);
            Push += menuItemsDict[cursor];
            
        }
        private void SetCursor(int value)
        {
            if (value >= menuItemsDict.Count)
                cursor = 0;
            else if (value < 0)
                cursor = menuItemsDict.Count - 1;
            else
                cursor = value;
        }

        private void Calculate (object sender, ConsoleMenuEventHandler e)
        {
            switch (currentReadMode)
            {
                case Reader.ReadMode.console:
                    {
                        if (Reader.Read(out string input))
                        {
                            if (ReversePolishNotation.TryParse(input, out string postFixExpression))
                            {
                                if (ReversePolishNotation.Calculate(postFixExpression, out double result, out string log))
                                {
                                    //Console.WriteLine($"Исходное выражение: {input}");
                                    Console.WriteLine($"Постфиксная запись: {postFixExpression}");
                                    Console.WriteLine($"Ответ: {result}");
                                    Console.WriteLine(log);
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine(log);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Не удалось разбить строку на составляющие");//Убрать потом отсюда врайтлайны
                            }
                        }
                        else
                        {
                            Console.WriteLine("Введена пустая строка");//Убрать потом отсюда врайтлайны
                        }
                        break;
                    }
                case Reader.ReadMode.textFile:
                    {
                        Console.WriteLine("Пока не работает");
                        break;
                    }
            }
        }

        private void SwitchReadMode(object sender, ConsoleMenuEventHandler e)
        {
            if (currentReadMode == Reader.ReadMode.console)
                currentReadMode = Reader.ReadMode.textFile;
            else
                currentReadMode = Reader.ReadMode.console;
        }

        private void Exit(object sender, ConsoleMenuEventHandler e)
        {
            Console.WriteLine("Завершена работа программы. Нажмите любую клавишу...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }

    class ConsoleMenuEventHandler
    {

    }
   
}
