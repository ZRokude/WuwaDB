using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;

namespace WuwaDB.Components.MudDialog
{
    public partial class ConfirmDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        private async void Confirm() => MudDialog.Close(DialogResult.Ok(true));
        private async void Cancel() => MudDialog.Cancel();
    }
}
