using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.Pages
{
    public partial class EditCharacter
    {
        [Inject] AdminRepository AdminRepository { get; set; }
        [Inject] UserRepository UserRepository { get; set;}
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        protected override async void OnInitialized()
        {

        }
    }
}
