using System;
using System.Threading;

namespace IP_Parser
{
    class Menu : Parser
    {
        public void MainMenu()
        {
            CreateDefaultFiles();

            string choice;
            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\t ================================= ");
                Console.WriteLine("\t\t\t\t\t ==                             == ");
                Console.WriteLine("\t\t\t\t\t ==          IP Parser          == ");
                Console.WriteLine("\t\t\t\t\t ==                             == ");
                Console.WriteLine("\t\t\t\t\t ================================= ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(" Menu:");
                Console.WriteLine();
                Console.WriteLine(" 1. Start parse");
                Console.WriteLine(" 2. How to use");
                Console.WriteLine(" 3. Exit");
                Console.WriteLine();
                Console.Write(" Enter: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Start();
                        break;
                    case "2":
                        HowToUse();
                        break;
                    case "3":
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" Incorrect choice!");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        Console.Beep();
                        MainMenu();
                        break;
                }
            } while (choice != "3");
        }

        private void HowToUse()
        {

        }
    }
}
