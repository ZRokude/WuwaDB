using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog.CharacterDialog
{
    public partial class EditCharacterSkillDetailName
    {
        [Inject] AdminRepository AdminRepository { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public List<Character_Skill_Detail> CharacterSkillDetails { get; set; } = new();
        private Character_Skill_Detail NewCharacterSkillDetail { get; set; } = new();

        private async Task UpdateSkillDetailName() 
        {
            await AdminRepository.UpdatesAsync(NewCharacterSkillDetail);
            MudDialog.Close(DialogResult.Ok(true));
        }

    }
}
