using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Enum;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog
{
    public partial class EditCharacterSkill
    {
        [Inject] private AdminRepository AdminRepository { get; set;}
        [Inject] private UserRepository UserRepository { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid CharacterId { get; set; }
        [Parameter] public SkillType SkillType { get; set; }
        private Character_Skill CharacterSkill { get; set; } = new();
        private Character_Skill_Detail CharacterSkillDetail { get; set; } = new();
        private bool SkillExist;
        IBrowserFile file;
        protected override async void OnInitialized()
        {
            var propertyFilter = new
            {
                Type = SkillType,
                CharacterId = CharacterId
            };
            CharacterSkill = await UserRepository.GetDataAsync<Character_Skill>(propertyFilter);
            if (CharacterSkill is not null)
                SkillExist = true;
            else
                CharacterSkill = new();
            StateHasChanged();
        }
        
        private async Task SaveCharacterSkill()
        {
            if (SkillExist is not true)
                CharacterSkill.CharacterId = CharacterId;
            await AdminRepository.SavesAsync(CharacterSkillDetail);
            StateHasChanged();
            MudDialog.Close(DialogResult.Ok(true));
        }

        private async Task SaveCharacterSkillDetail()
        {

        }
        private 
    }
}
