using System;

namespace Bookish
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int choice = 0;
            while (choice != 3)
            {
                DisplayMenu();
                int.TryParse(Console.ReadLine(), out choice);
                GetChoice(choice);
            }
        }

        private static void GetChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    GetLoginInfo getLoginInfo = new GetLoginInfo();
                    break;

                case 2:
                    GetRegisterInfo getRegisterInfo = new GetRegisterInfo();
                    break;
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("Welcome to the book(ish) system!");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("   1. Login");
            Console.WriteLine("   2. Register");
            Console.WriteLine();
            Console.WriteLine("   3. Exit");
            Console.Write("   > ");
        }
    }
}