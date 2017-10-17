using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Bookish.DataConnection.Tables;
using Dapper;

namespace DataAccess
{
    public class Register
    {
        private readonly string _userName;
        private readonly string _password;
        private readonly IDbConnection _db = new SqlConnection("Server=DESKTOP-HBK73L7;Database=Bookish;User Id=dbo; Password=;");

        public Register(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public void InsertNewUser()
        {
            string sqlString = $"INSERT INTO Users (userName,PasswordHash) VALUES ({_userName},{_password});";
            _db.Query<Users>(sqlString);
        }

        public Boolean CheckUserNameIsFree()
        {
            string sqlString = $"SELECT * FROM Users WHERE Username = {_userName};";
            List<Users> output = (List<Users>) _db.Query<Users>(sqlString);
            return (output.Count == 0);
        }
    }
}
