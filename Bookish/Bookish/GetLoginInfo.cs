using DataAccessNew;
using System;

namespace Bookish
{
    public class GetLoginInfo
    {
        public GetLoginInfo()
        {
            while (true)
            {
                string userName = UserInput.GetInput("Username");
                string password = UserInput.GetInput("Password");
                LogIn login = new LogIn(userName);
                if (login.ValidatePassword(password))
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine(" Invalid passwordHash.");
                Console.WriteLine();
            }
        }
    }
}