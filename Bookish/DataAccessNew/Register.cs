using DataAccessNew.Tables;
using DevOne.Security.Cryptography.BCrypt;
using System;
using System.Collections.Generic;

namespace DataAccessNew
{
    public class Register
    {
        private readonly string _userName;
        private readonly string _passwordHash;
        private readonly UserRepository _userRepository = new UserRepository();

        public Register(string userName, string password)
        {
            string salt = BCryptHelper.GenerateSalt();
            _passwordHash = BCryptHelper.HashPassword(password, salt);
            _userName = userName;
        }

        public void InsertNewUser()
        {
            Users newUser = new Users
            {
                userName = _userName,
                passwordHash = _passwordHash
            };
            _userRepository.InsertUser(newUser);
        }

        public Boolean CheckUserNameIsFree()
        {
            List<Users> users = _userRepository.GetUsers();
            foreach (Users user in users)
            {
                if (user.userName == _userName)
                {
                    return false;
                }
            }
            return true;
        }
    }
}