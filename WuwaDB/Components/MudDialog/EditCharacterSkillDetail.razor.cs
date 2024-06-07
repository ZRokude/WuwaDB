using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WuwaDB.DBAccess.DataContext;
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog
{
    public partial class EditCharacterSkillDetail
    {
        [Inject] WuwaDbContext context { get; set; }
        [Inject] UserRepository UserRepository { get; set; }
        [Inject] AdminRepository AdminRepository { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid SkillId { get; set; }
        private Character_Skill_Detail CharacterSkillDetail { get; set; } = new();
        private Character_Skill_Detail_Number CharacterSkillDetailNumber { get; set; } = new();
        private List<Character_Skill_Detail_Number?> CharacterSkillDetailNumbers { get; set; } = new();
        private List<Character_Skill_Detail> CharacterSkillDetails { get; set; } = new();

        private string[] SkillDetailNames;
        private int[] SkillLevels;
        protected override async void OnInitialized()
        {
            object propFilter = new
            {
                CharacterSkillId = SkillId
            };
            CharacterSkillDetails = await UserRepository.GetToListAsync<Character_Skill_Detail>(propFilter);
            string[] propInclude = ["Character_Skill_Detail"];
            CharacterSkillDetailNumbers = await UserRepository.GetToListAsync<Character_Skill_Detail_Number>(propFilter, propInclude);
            if (CharacterSkillDetails.Count > 0)
            {
                SkillDetailNames = new string[CharacterSkillDetails.Count];
                for(int i = 0; i< CharacterSkillDetails.Count; i++) 
                {
                    SkillDetailNames[i] = CharacterSkillDetails[i].SkillDetailsName;
                }
            }
            StateHasChanged();
        }

        private async Task<IEnumerable<string>> SearchSkillDetailName(string value)
        {
            if (string.IsNullOrEmpty(value))
                return SkillDetailNames;
            return SkillDetailNames.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));

        }
        private async Task<IEnumerable<int>> SearchSkillLevel(string value)
        {
            if (Convert.ToInt32(value) == 0)
                return SkillLevels;
            if (int.TryParse(value, out int parsedValue))
                return SkillLevels.Where(x => x.ToString().StartsWith(value));
            // If parsing fails, return an empty list
            return Enumerable.Empty<int>();

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

            if (string.IsNullOrEmpty(value))
            {
                CharacterSkillDetailNumber.Level = 0;
                CharacterSkillDetailNumber.Number = 0;
                CharacterSkillDetailNumber.Multiplier = null;
                SkillLevels = null;
            }
            
        }

        private async Task TextChangedSkillLevel(string value)
        {
            var matchSkillDetailLevel = CharacterSkillDetailNumbers.FirstOrDefault(x => x.Level.ToString() == value);
            if (matchSkillDetailLevel is not null)
            {
                var SkillDetailId =
                    CharacterSkillDetails.FirstOrDefault(x =>
                        x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName).Id;
                var matchSkillDetailNumbers = CharacterSkillDetailNumbers.Where(x =>
                    x.Level.ToString() == value
                    && x.CharacterSkillDetailId == SkillDetailId).ToList();
                CharacterSkillDetailNumber.Number = matchSkillDetailNumbers[0].Number;
                CharacterSkillDetailNumber.Multiplier = matchSkillDetailNumbers[0].Multiplier;
            }

            if (string.IsNullOrEmpty(value))
            {
                CharacterSkillDetailNumber.Number = 0;
                CharacterSkillDetailNumber.Multiplier = null;
            }
        }
        private async Task Save()
        {
            if (CharacterSkillDetails.FirstOrDefault(x => x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName)
                is not null)
                CharacterSkillDetailNumber.CharacterSkillDetailId = CharacterSkillDetails.FirstOrDefault(x => x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName).Id;
            else
            {
                CharacterSkillDetail.CharacterSkillId = SkillId;
                await AdminRepository.SavesAsync(CharacterSkillDetail);
                CharacterSkillDetailNumber.CharacterSkillDetailId = CharacterSkillDetail.Id;
            }

            var Match = CharacterSkillDetailNumbers.FirstOrDefault(x => x.Level == CharacterSkillDetailNumber.Level 
                                                                        && x.CharacterSkillDetailId == CharacterSkillDetailNumber.CharacterSkillDetailId);
            if (Match is not null)
                await AdminRepository.UpdatesAsync(CharacterSkillDetailNumber);
            else
                await AdminRepository.SavesAsync(CharacterSkillDetailNumber);
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}
