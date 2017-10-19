using DataAccessNew;

namespace Bookish.Web.Models
{
    public class LoginRegister
    {
        public bool Validate(LoginRegisterData data)
        {
            return data.NewUser ? Register(data) : LogIn(data);
        }

        private bool Register(LoginRegisterData data)
        {
            Register register = new Register(data.UserName, data.Password);
            if (data.Password != data.ConfirmPassword || !register.CheckUserNameIsFree()) return false;
            register.InsertNewUser();
            return true;
        }

        private bool LogIn(LoginRegisterData data)
        {
            LogIn login = new LogIn(data.UserName);
            return login.ValidatePassword(data.Password);
        }
    }
}