using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog.CharacterDialog
{
    public partial class DeleteCharacter
    {
        [Inject] AdminRepository AdminRepository { get; set; }
        [Inject] UserRepository UserRepository { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid CharacterId { get; set; }
        private async void Submit() 
        {
            await AdminRepository.DeleteAsync<Character>(new { Id = CharacterId });
            MudDialog.Close(DialogResult.Ok(true));
        }
        private async void Cancel() => MudDialog.Close(DialogResult.Cancel());

    }
}
