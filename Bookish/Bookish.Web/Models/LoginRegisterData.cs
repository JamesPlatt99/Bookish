namespace Bookish.Web.Models
{
    public class LoginRegisterData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool NewUser { get; set; }
        public string Error { get; set; }
    }
}