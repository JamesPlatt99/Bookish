using Bookish.Web.Models;
using DataAccessNew.Tables;

namespace Bookish.Web.ViewModels
{
    public class UserData
    {
        public Users User { get; set; }

        public void GetUserData(LoginRegisterData loginRegisterData)
        {
            Users user = new Users
            {
                userName = loginRegisterData.UserName,
                passwordHash = loginRegisterData.Password
            };
            User = user;
        }
    }
}