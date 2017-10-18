using System;
using DataAccessNew;

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
                if (login.validatePassword(password))
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine(" Invalid password.");
                Console.WriteLine();
            }
    }
    }
}