using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataAccessNew.Tables;
using DevOne.Security.Cryptography.BCrypt;

namespace DataAccessNew
{
    public class Register
    {
        private readonly string _userName;
        private readonly string _passwordHash;
        private readonly CustomerRepository _customerRepository = new CustomerRepository();

        public Register(string userName, string password)
        {

            string salt = BCryptHelper.GenerateSalt();
            _passwordHash =  BCryptHelper.HashPassword(password, salt);
            _userName = userName;
        }

        public void InsertNewUser()
        {
            Users newUser = new Users
            {
                userName = _userName,
                passwordHash = _passwordHash
            };
            _customerRepository.InsertUser(newUser);
        }

        public Boolean CheckUserNameIsFree()
        {
            List<Users> users = _customerRepository.GetUsers();
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
