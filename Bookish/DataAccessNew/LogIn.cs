using DataAccessNew.Tables;
using DevOne.Security.Cryptography.BCrypt;

namespace DataAccessNew
{
    public class LogIn
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly Users _user;

        public LogIn(string userName)
        {
            _user = GetUserData(userName);
        }

        public bool ValidatePassword(string password)
        {
            return _user != null && BCryptHelper.CheckPassword(password, _user.passwordHash);
        }

        private Users GetUserData(string userName)
        {
            return _userRepository.GetSingleUser(userName);
        }
    }
}