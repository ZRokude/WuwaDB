using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Enum;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog
{
    public partial class EditCharacter
    {
        [Inject] AdminRepository AdminRepository { get; set; }
        [Inject] UserRepository UserRepository { get; set;}
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid CharacterId { get; set; }

        private void OpenDialogStats() => OpenDialogEvent("Stat_Base");
        private void OpenDialogBasicAttack() => OpenDialogEvent(SkillType.Basic_Attack);
        private void OpenDialogResonanceSkill() => OpenDialogEvent(SkillType.Resonance_Skill);
        private void OpenDialogForteCircuit() => OpenDialogEvent(SkillType.Forte_Circuit);
        private void OpenDialogResonanceLiberation() => OpenDialogEvent(SkillType.Resonance_Liberation);
        private void OpenDialogIntroSkill() => OpenDialogEvent(SkillType.Intro_Skill);
        private void OpenDialogOutroSkill() => OpenDialogEvent(SkillType.Outro_Skill);
        private void OpenDialogEvent(SkillType? dialogResult)
        {
           
            MudDialog.Close(DialogResult.Ok(dialogResult));
        }
        private void OpenDialogEvent(string dialogResult)
        {

            MudDialog.Close(DialogResult.Ok(dialogResult));
        }
        private void Cancel() => MudDialog.Close(DialogResult.Cancel());
    }
}
