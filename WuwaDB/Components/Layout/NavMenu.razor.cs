using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.Components.Pages;

namespace WuwaDB.Components.Layout
{
    public partial class NavMenu
    {
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


    }
}
