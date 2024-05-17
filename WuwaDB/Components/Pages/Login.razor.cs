using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using WuwaDB.Server.Repository;
using WuwaDB.Server.Entities.Account;
using BC = BCrypt.Net.BCrypt;
namespace WuwaDB.Components.Pages
{
    public partial class Login
    {
        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private UserRepository UserRepository { get; set; }
        private Account Account { get; set; }

        public class Model
        {
            public string Username = string.Empty, Password = string.Empty;
        }

        private string error = string.Empty;

        private Model model = new Model();

        private async Task Authenticate()
        {
            Account? UserAccount = await UserRepository.GetUserDataAsync(Account.Username);

            if (UserAccount == null || !BC.EnhancedVerify(model.Password, UserAccount.Password))
            {
                error = "Email is not found";
                return;
            }


            //CustomAuthentication customAuthentication = (CustomAuthentication)StateProvider;
            //await customAuthentication.UpdateAuthenticationState(new UserSession()
            //{
            //    Email = UserAccount.Email,
            //    Role = UserAccount.Role.ToString(),
            //});
            //if (UserAccount.Role == Roles.Admin)
            //{
            //    navigationManager.NavigateTo("/", true);

            //}
            //else
            //{
            //    navigationManager.NavigateTo("/", true);
            //}


        }



    }
}
