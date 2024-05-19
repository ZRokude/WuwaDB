using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using System.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using WuwaDB.DBAccess.Repository;


namespace WuwaDB.Components.Pages
{
    public partial class CreateAccDialog
    {

        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private UserRepository UserRepository { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        public AuthModel authModel { get; set; } = new();
        string[] additionalProp { get; set; }
        public int additionalPropCount = 0;

        protected override void OnInitialized()
        {
            // Initialize additionalProp with a size
            additionalProp = new string[20];
        }
        private async Task Submit()
        {
            var authModelProperties = authModel.GetType().GetProperties();
            
            // Iterate through the properties and add them to the dictionary
            foreach (var property in authModelProperties)
            {
                // Skip properties that are role, username, or password
                if (property.Name == nameof(authModel.Role) ||
                    property.Name == nameof(authModel.Username) ||
                    property.Name == nameof(authModel.Password))
                    continue;

                additionalProp[additionalPropCount] = property.GetValue(authModel).ToString();
                additionalPropCount++;
            }

            await UserRepository.CreateUserDataAsync(authModel.Username, authModel.Password, authModel.Role, additionalProp);
            StateHasChanged();
            MudDialog.Close(DialogResult.Ok(true));
            
        }
        private async void Cancel()
        {
            MudDialog.Close();
        }

        public class AuthModel
        {
            [Required(ErrorMessage = "Username is invalid, please fill the username ")]
            public string? Username { get; set; }
            [PasswordValidation(ErrorMessage = "You need to have atleast 1 uppercase, 1 lowercase, 1 special character, 1 number and 15 characters long")]
            public string Password { get; set; }
            public string Role { get; set; }
            [Required(ErrorMessage = "Email is invalid, please fill the email")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Role is invalid, please select the role")]
            public bool EmailConfirmed { get; set; } = false;
        }
    }
}
