using WuwaDB.Server.Entities.Account;

namespace WuwaDB.Authentication
{
    public class LoginSession
{
    public string Username { get; set; } = string.Empty;
    public List<Role> Role { get; set; } = new List<Role>();

}
}

