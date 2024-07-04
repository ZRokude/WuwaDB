using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.DataContext;
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog.CharacterDialog
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

        private string[]? SkillDetailNames;
        private int[]? SkillLevels;
        protected override async void OnInitialized()
        {
            CharacterSkillDetails = await UserRepository.GetToListAsync<Character_Skill_Detail>
                (new { CharacterSkillId = SkillId });
            CharacterSkillDetailNumbers = await UserRepository.GetToListAsync<Character_Skill_Detail_Number>
                (new { CharacterSkillId = SkillId }, new string[] { "Character_Skill_Detail"}, new string[] {"NumberMultipliers"});
            if (CharacterSkillDetails.Count > 0)
            {
                SkillDetailNames = new string[CharacterSkillDetails.Count];
                for (int i = 0; i < CharacterSkillDetails.Count; i++)
                {
                    SkillDetailNames[i] = CharacterSkillDetails[i].SkillDetailsName;
                }
            }
            StateHasChanged();
        }
        private void AddIndexListNumber() =>
            CharacterSkillDetailNumber.NumberMultipliers.Add(new NumberMultiplier { Number = 0, Multiplier = null});
        private void MinIndexListNumber() =>
            CharacterSkillDetailNumber.NumberMultipliers.RemoveAt(CharacterSkillDetailNumber.NumberMultipliers.Count - 1);
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
            var matchSkillDetail = CharacterSkillDetails.First(x => x.SkillDetailsName == value);
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
                SkillLevels = null;
            }

        }

        private void TextChangedSkillLevel(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                CharacterSkillDetailNumber.Level = 0;
                CharacterSkillDetailNumber.NumberMultipliers.Clear();
            }
            else
            {
                var matchSkillDetailLevel =
                CharacterSkillDetailNumbers.Find(
                    x => x.CharacterSkillDetailId == CharacterSkillDetails.Find(x => x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName)?.Id && x.Level.ToString() == value) ?? null;
                if (matchSkillDetailLevel is not null)
                {
                    CharacterSkillDetailNumber.NumberMultipliers = new List<NumberMultiplier>(matchSkillDetailLevel.NumberMultipliers);
                }
            }
        }
        private async Task Save()
        {
            if (CharacterSkillDetails.Find(x => x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName)
                is not null)
                CharacterSkillDetailNumber.CharacterSkillDetailId = CharacterSkillDetails.First(x => x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName).Id;
            else
            {
                CharacterSkillDetail.CharacterSkillId = SkillId;
                await AdminRepository.SavesAsync(CharacterSkillDetail);
                CharacterSkillDetailNumber.CharacterSkillDetailId = CharacterSkillDetail.Id;
            }

            var Match = CharacterSkillDetailNumbers.Find(x => x.Level == CharacterSkillDetailNumber.Level
                                                                        && x.CharacterSkillDetailId == CharacterSkillDetailNumber.CharacterSkillDetailId);
            if (Match is not null)
                await AdminRepository.UpdatesAsync(CharacterSkillDetailNumber);
            else
                await AdminRepository.SavesAsync(CharacterSkillDetailNumber);
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}
