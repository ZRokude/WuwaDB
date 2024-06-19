using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Enum;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog.CharacterDialog
{
    public partial class EditCharacter
    {
        [Inject] AdminRepository AdminRepository { get; set; }
        [Inject] UserRepository UserRepository { get; set;}
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid CharacterId { get; set; }
        [Inject] private IDialogService DialogService { get; set; }

        private void OpenDialogStats() => OpenDialogEditStat();
        private void OpenDialogBasicAttack() => OpenDialogSkill(SkillType.Basic_Attack);
        private void OpenDialogResonanceSkill() => OpenDialogSkill(SkillType.Resonance_Skill);
        private void OpenDialogForteCircuit() => OpenDialogSkill(SkillType.Forte_Circuit);
        private void OpenDialogResonanceLiberation() => OpenDialogSkill(SkillType.Resonance_Liberation);
        private void OpenDialogIntroSkill() => OpenDialogSkill(SkillType.Intro_Skill);
        private void OpenDialogOutroSkill() => OpenDialogSkill(SkillType.Outro_Skill);
        private async void OpenDialogSkill(SkillType type)
        {
            var skillType = string.Join(" ",
                type.ToString().Split("_").Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1).ToLower()));
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<EditCharacterSkill>();
            parameters.Add(x=>x.CharacterId, CharacterId);
            parameters.Add(x=>x.SkillType, type);
            var dialog = await DialogService.ShowAsync<EditCharacterSkill>(skillType, parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
                MudDialog.Close(DialogResult.Ok(true));
        }
        private async void OpenDialogEditStat()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<EditCharacterStats>();
            parameters.Add(x => x.CharacterId, CharacterId);
            var dialog = await DialogService.ShowAsync<EditCharacterStats>("Edit Character Stats", parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
                MudDialog.Close(DialogResult.Ok(true));
        }

        private async void OpenDialogCharacterInfo()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, CloseButton = true };
            var parameters = new DialogParameters<EditCharacterInfo>();
            parameters.Add(x=>x.CharacterId, CharacterId);
            var dialog = await DialogService.ShowAsync<EditCharacterInfo>("Edit Character Info", parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
                MudDialog.Close(DialogResult.Ok(true));
        }
        private void Cancel() => MudDialog.Close(DialogResult.Cancel());
    }
}
