using WuwaDB.DBAccess.Entities.Account;

namespace WuwaDB.Authentication
{
    public class LoginSession
    {
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

    }
}

