using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using WuwaDB.Components.MudDialog;
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Login;
using WuwaDB.DBAccess.Repository;
using WuwaDB.Services;

namespace WuwaDB.Components.Pages
{
    public partial class Login
    {
        [Parameter] public string Url { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private LastestUrl LastestUrl { get; set; }
        [Inject] private UserRepository UserRepository { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        
        private Login_Info? LoginInfo { get; set; }
        protected override async void OnInitialized()
        {
            if (Guid.TryParse(Url, out var NewGuid))
            {
                LoginInfo = await UserRepository.GetDataAsync<Login_Info>(new { LoginUrl = NewGuid });
                if (LoginInfo is not null)
                    OpenLoginDialog();
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if(LoginInfo is null)
                    NavigationManager.NavigateTo($"{LastestUrl.CheckLastUrl()}");
                
            }
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
