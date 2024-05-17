using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.Components.Pages;

namespace WuwaDB.Components.Layout
{
    public partial class NavMenu
    {
        [Inject] public IDialogService DialogService { get; set; }
        private async Task OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<Login>();
            parameters.Add(x => x.ContentText, "SimpleDialog");
            var dialog = await DialogService.ShowAsync<Login>("Login",parameters, options);
            var result = await dialog.Result;

        }



    }
}
