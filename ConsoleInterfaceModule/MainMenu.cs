using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterfaceModule
{
    class MainMenu
    {
        int cursor;

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

            menuItemsNames.Add(1, "Выбрать режим считывания");
            menuItemsDict.Add(1, SetReadMode);

            menuItemsNames.Add(2, "Выйти из программы");
            menuItemsDict.Add(2, Exit);
        }
        public void LaunchMainCycle ()
        {
            ConsoleKeyInfo key;
           
            do
            {
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
            Console.WriteLine("Заглушка!");            
        }

        private void SetReadMode(object sender, ConsoleMenuEventHandler e)
        {
            Console.WriteLine("Заглушка!");
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
