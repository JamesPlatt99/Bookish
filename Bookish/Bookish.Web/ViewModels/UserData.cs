using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookish.Web.Models;
using DataAccessNew;
using DataAccessNew.Tables;

namespace Bookish.Web.ViewModels
{
    public class UserData
    {
        public Users User { get; set; }

        public void GetUserData(LoginRegisterData loginRegisterData)
        {
            Users user = new Users();
            user.userName = loginRegisterData.UserName;
            user.passwordHash = loginRegisterData.Password;
            User = user;
        }
    }
}