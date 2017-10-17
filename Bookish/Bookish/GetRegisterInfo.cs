using DataAccess;

namespace Bookish
{
    public class GetRegisterInfo
    {
        public GetRegisterInfo()
        {
            while (true) {
                GetUserNameAndPassword(out string userName, out string password);
                Register register = new Register(userName,password);
                if (!register.CheckUserNameIsFree()) continue;
                register.InsertNewUser();
                break;
            }
        }

        private void GetUserNameAndPassword(out string userName, out string password)
        {
            while (true)
            {
                userName = UserInput.GetInput("Username");
                password = UserInput.GetInput("Password");
                string confirmPassword = UserInput.GetInput("Confirm Password");
                if (password == confirmPassword)
                {
                    break;
                }
                System.Console.WriteLine("Passwords do not match.");
            }
        }
    }
}