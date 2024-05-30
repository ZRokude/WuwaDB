using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using System.Security.Claims;
using WuwaDB.Authentication;
using WuwaDB.Components.MudDialog;

namespace WuwaDB.Components.Layout
{
    public partial class NavMenu
    {
        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
        [Inject] private NavigationManager navigationManager { get; set; } = default!;

        [Inject] public IDialogService DialogService { get; set; }
        private async Task OpenLoginDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<Login>("Login", options);
            var result = await dialog.Result;

        }
        private async Task OpenRegisterDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<CreateAccDialog>("Register", options);
            var result = await dialog.Result;

        }
        private async Task LogOutAsync()
        {
            await ((CustomAuthentication)AuthStateProvider).UpdateAuthenticationState(null);
            navigationManager.NavigateTo("/", true);

        }
        private async Task OpenCreateChar()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<CreateCharacter>("Create Character", options);
            var result = await dialog.Result;
            if (!result.Canceled)
                NavigationManager.NavigateTo("/Character/List", true);
        }

    }
}
