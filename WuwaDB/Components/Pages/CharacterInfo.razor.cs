using System.Collections.Immutable;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.Components.MudDialog;
using WuwaDB.DBAccess.Repository;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Enum;

namespace WuwaDB.Components.Pages
{
    public partial class CharacterInfo
    {
        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private UserRepository UserRepository { get; set; }
        [Inject] public IDialogService DialogService { get; set; }
        [Parameter] public string CharacterName { get; set; }

        public Character_Skill CharacterSkill { get; set; } = new();
        public Character_Stats_Base CharacterStats { get; set; } = new();
        public Character character { get; set; } = new();

        protected override async void OnInitialized()
        {
            var _character = await UserRepository.FindCharacterAsync(CharacterName);
            if (_character is not null)
                character = _character; 
            StateHasChanged();
            
        }

        private async void OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<EditCharacter>();
            parameters.Add(x=> x.CharacterId, character.Id);
            var dialog = await DialogService.ShowAsync<EditCharacter>("Edit Character", parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                switch (result.Data)
                {
                    case "Stat_Base":
                        OpenDialogEditStat();
                        break;
                    case SkillType.Basic_Attack:
                        OpenDialogSkill(SkillType.Basic_Attack);
                        break;
                    case SkillType.Resonance_Skill:
                        OpenDialogSkill(SkillType.Resonance_Skill);
                        break;
                    case SkillType.Forte_Circuit:
                        OpenDialogSkill(SkillType.Forte_Circuit);
                        break;
                    case SkillType.Resonance_Liberation:
                        OpenDialogSkill(SkillType.Resonance_Liberation);
                        break;
                    case SkillType.Intro_Skill:
                        OpenDialogSkill(SkillType.Intro_Skill);
                        break;
                    case SkillType.Outro_Skill:
                        OpenDialogSkill(SkillType.Outro_Skill);
                        break;
                }
            }
        }

        private async void OpenDialogEditStat()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<EditCharacterStats>();
            parameters.Add(x => x.CharacterId, character.Id);
            var dialog = await DialogService.ShowAsync<EditCharacterStats>("Edit Character Stats", parameters, options);
            var result = dialog.Result;
        }

        private async void OpenDialogSkill(SkillType type)
        {
            var skillType = string.Join(" ",
                type.ToString().Split("_").Select(w => w.Substring(0,1).ToUpper() + w.Substring(1).ToLower()));
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<EditCharacterSkill>();
            parameters.Add(x => x.CharacterId, character.Id);
            parameters.Add(x => x.SkillType, type);
            var dialog = await DialogService.ShowAsync<EditCharacterSkill>(skillType, parameters, options);
            var result= dialog.Result; 
        }
    }
}
