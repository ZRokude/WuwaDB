using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Entities.Character;

namespace WuwaDB.Components.MudDialog
{
    public partial class EditCharacterSkillDetail
    {
        
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid CharacterSkillTypeId { get; set; }

        private Character_Skill_Detail_Number CharacterSkillDetailNumber { get; set; }
        protected override async void OnInitialized()
        {

        }
        private async Task Save()
        {

        }
    }
}
