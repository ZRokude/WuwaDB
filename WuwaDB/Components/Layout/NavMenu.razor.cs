using MudBlazor;
using WuwaDB.Components.Pages;

namespace WuwaDB.Components.Layout
{
    public partial class NavMenu
    {
        public IDialogService DialogService { get; set; }
        private void OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            DialogService.Show<Login>();
        }

    }
}
