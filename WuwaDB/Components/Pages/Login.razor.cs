using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using WuwaDB.Components.MudDialog;
using WuwaDB.Services;

namespace WuwaDB.Components.Pages
{
    public partial class Login
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private LastestUrl LastestUrl { get; set; }
        //[Parameter] public string LoginUrl { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        protected override void OnInitialized()
        {
             OpenLoginDialog();
        }

        private async void OpenLoginDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var dialog = await DialogService.ShowAsync<LoginDialog>("Login", options);
            var result = await dialog.Result;
            if (!result.Canceled)
                NavigationManager.NavigateTo($"{LastestUrl.CheckLastUrl()}");
            else
                NavigationManager.NavigateTo($"{LastestUrl.CheckLastUrl()}");
        }
        
    }
}
