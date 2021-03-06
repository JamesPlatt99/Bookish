﻿using Dapper;
using DataAccessNew.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessNew
{
    public class UserRepository
    {
        private readonly IDbConnection _db = new SqlConnection("Server = localhost; Database=Booksih;Trusted_Connection=True;");

        public List<Users> GetUsers(int amount, string sort)
        {
            string sqlString = $"SELECT TOP {amount} * FROM Users ORDERBY {sort};";
            List<Users> users = (List<Users>)_db.Query<Users>(sqlString);
            return users;
        }

        public List<Users> GetUsers()
        {
            string sqlString = "SELECT * FROM Users;";
            List<Users> users = (List<Users>)_db.Query<Users>(sqlString);
            return users;
        }

        public Users GetSingleUser(int userId)
        {
            string sqlString = $"SELECT * FROM Users Where id = '{userId}';";
            Users user = _db.Query<Users>(sqlString).SingleOrDefault();
            return user;
        }

        public Users GetSingleUser(string userName)
        {
            string sqlString = $"SELECT * FROM Users Where userName = '{userName}';";
            Users user = _db.Query<Users>(sqlString).SingleOrDefault();
            return user;
        }

        public void InsertUser(Users user)
        {
            _db.Execute($"INSERT Users(userName,passwordHash) VALUES ('{user.userName}', '{user.passwordHash}')");
        }

        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(Users ourUser)
        {
            throw new NotImplementedException();
        }
    }
}