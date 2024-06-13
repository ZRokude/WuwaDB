using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using WuwaDB.DBAccess.Repository;
using WuwaDB.DBAccess.Entities.Account;
using BC = BCrypt.Net.BCrypt;
using MudBlazor;
using WuwaDB.Authentication;
using System.Data;

using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;
using WuwaDB.Services;

namespace WuwaDB.Components.MudDialog
{
    public partial class LoginDialog
    {
        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private UserRepository UserRepository { get; set; }
        private Account Account { get; set; } = new Account();
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public string ContentText { get; set; }


        private string error = string.Empty;

        private Model model = new Model();

        /// <summary>
        ///  its a function to set authentication with CustomAuthentication 
        ///  based on the role and username of the current logged in user
        ///  the way how to do it is through variable account that checked 
        ///  the if user exist with current data input
        ///  since password is ahshed by BCrypt.Net, we used EnhancedVerify() to check the password if its right
        /// </summary>
        private async void Submit()
        {
            Account = await UserRepository.GetDataAsync<Account>(new{ Username = Account.Username});
            if (Account == null || !BC.EnhancedVerify(model.Password, Account.Password))
            {
                error = "Email is not found";
                return;
            }
            CustomAuthentication customAuthentication = (CustomAuthentication)StateProvider;
            await customAuthentication.UpdateAuthenticationState(new LoginSession()
            {
                Username = Account.Username,
                Role = Account.Role.Name.ToString()
            });
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
        private async Task Authenticate(Account UserAccount)
        {
            CustomAuthentication customAuthentication = (CustomAuthentication)StateProvider;
            await customAuthentication.UpdateAuthenticationState(new LoginSession()
            {
                Username = UserAccount.Username,
                Role = UserAccount.Role.ToString()
            });
        }
    }
}
