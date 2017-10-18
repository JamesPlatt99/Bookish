using DataAccessNew.Tables;
using DevOne.Security.Cryptography.BCrypt;

namespace DataAccessNew
{
    public class LogIn
    {
        private readonly CustomerRepository _customerRepository = new CustomerRepository();
        private readonly Users _user;
        public LogIn(string userName)
        {
            _user = GetUserData(userName);
        }

        public bool validatePassword(string password)
        {
            return BCryptHelper.CheckPassword(password, _user.passwordHash);
        }

        private Users GetUserData(string userName)
        {
            return _customerRepository.GetSingleUser(userName);
        }
    }
}