using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog
{
    public partial class EditCharacterSkillDetail
    {
        [Inject] UserRepository UserRepository { get; set; }
        [Inject] AdminRepository AdminRepository { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid SkillId { get; set; }
        private Character_Skill_Detail CharacterSkillDetail { get; set; } = new();
        private Character_Skill_Detail_Number CharacterSkillDetailNumber { get; set; } = new();
        private List<Character_Skill_Detail_Number> CharacterSkillDetailNumbers { get; set; } = new();
        private List<Character_Skill_Detail> CharacterSkillDetails { get; set; } = new();

        private string[] SkillDetailNames;
        private int[] SkillLevels;
        protected override async void OnInitialized()
        {
            object propFilter = new
            {
                CharacterSkillId = SkillId,
            };
            CharacterSkillDetails = await UserRepository.GetToListAsync<Character_Skill_Detail>(propFilter);
            CharacterSkillDetailNumbers = await UserRepository.GetToListAsync<Character_Skill_Detail_Number>(propFilter);
            if (CharacterSkillDetails.Count > 0)
            {
                for(int i = 0; i< CharacterSkillDetails.Count; i++) 
                {
                    SkillDetailNames[i] = CharacterSkillDetails[i].SkillDetailsName;
                }
            }
            StateHasChanged();
        }
        private async Task TextChangedSkillDetailName(string value)
        {
            var matchSkillDetail = CharacterSkillDetails.FirstOrDefault(x => x.SkillDetailsName == value);
            if (matchSkillDetail is not null)
            {
                var matchSkillDetailNumbers = CharacterSkillDetailNumbers
                .Where(x => x.CharacterSkillDetailId == matchSkillDetail.Id)
                .ToList();
                SkillLevels = new int[matchSkillDetailNumbers.Count];
                for (int i = 0; i < matchSkillDetailNumbers.Count; i++)
                    SkillLevels[i] = matchSkillDetailNumbers[i].Level;
            }
        }
        private async Task Save()
        {

            CharacterSkillDetailNumber.CharacterSkillDetailId = CharacterSkillDetails.FirstOrDefault(x=>x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName).Id;
            await AdminRepository.SavesAsync(CharacterSkillDetailNumber);
        }
    }
}
