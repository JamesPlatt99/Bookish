using System;

namespace Bookish
{
    public class UserInput
    {
        public static string GetInput(string prompt)
        {
            string userName = "";
            while (userName.Length == 0)
            {
                Console.Write($" {prompt}:");
                userName = Console.ReadLine();
            }
            return userName;
        }
    }
}