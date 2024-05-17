using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using WuwaDB.DBAccess.Repository;
using WuwaDB.DBAccess.Entities.Account;
using BC = BCrypt.Net.BCrypt;
using MudBlazor;
using WuwaDB.Authentication;
using System.Data;

using Microsoft.AspNetCore.Authorization.Infrastructure;
namespace WuwaDB.Components.Pages
{
    public partial class Login
    {
        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private UserRepository UserRepository { get; set; }
        private Account Account { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public string ContentText { get; set; }



        private async void Submit()
        {
            Account? UserAccount = await UserRepository.GetUserDataAsync(Account.Username);

            if (UserAccount == null || !BC.EnhancedVerify(model.Password, UserAccount.Password))
            {
                error = "Email is not found";
                return;
            }
            await Authenticate(UserAccount);
            MudDialog.Close(DialogResult.Ok(true));
        }
        void Cancel()
        {
            MudDialog.Cancel();
        }
        public class Model
        {
            public string Username = string.Empty, Password = string.Empty;
        }

        private string error = string.Empty;

        private Model model = new Model();

        private async Task Authenticate(Account UserAccount)
        {
            CustomAuthentication customAuthentication = (CustomAuthentication)StateProvider;
            await customAuthentication.UpdateAuthenticationState(new LoginSession()
            {
                Username = UserAccount.Username,
                Role = UserAccount.Roles.ToList()
            });
            
            if (UserAccount.Roles.Any(role => role.Name != null))
            {
                navigationManager.NavigateTo("/", true);

            }
            else
            {
                navigationManager.NavigateTo("/", true);
            }





        }



    }
}
