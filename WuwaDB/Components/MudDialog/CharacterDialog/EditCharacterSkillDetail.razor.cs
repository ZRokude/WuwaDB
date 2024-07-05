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
        [Inject] UserRepository UserRepository { get; set; }
        [Inject] AdminRepository AdminRepository { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid SkillId { get; set; }
        private Character_Skill_Detail CharacterSkillDetail { get; set; } = new();
        private Character_Skill_Detail_Number CharacterSkillDetailNumber { get; set; } = new();
        private List<Character_Skill_Detail_Number?> CharacterSkillDetailNumbers { get; set; } = new();
        private List<Character_Skill_Detail> CharacterSkillDetails { get; set; } = new();
        private int[]? SkillLevels;
        protected override async void OnInitialized()
        {
            CharacterSkillDetails = await UserRepository.GetToListAsync<Character_Skill_Detail>
                (new { CharacterSkillId = SkillId });
            CharacterSkillDetailNumbers = await UserRepository.GetToListAsync<Character_Skill_Detail_Number>
                (new { CharacterSkillId = SkillId }, new string[] { "Character_Skill_Detail" }, new string[] { "NumberMultipliers" });
            StateHasChanged();
        }
        private void AddIndexListNumber() =>
            CharacterSkillDetailNumber.NumberMultipliers.Add(new NumberMultiplier { Number = 0, Multiplier = null});
        private void MinIndexListNumber() =>
            CharacterSkillDetailNumber.NumberMultipliers.RemoveAt(CharacterSkillDetailNumber.NumberMultipliers.Count - 1);
        private async Task<IEnumerable<string>> SearchSkillDetailName(string value)
        {
            if (string.IsNullOrEmpty(value))
                return CharacterSkillDetails.Select(c=>c.SkillDetailsName).ToArray();
            return CharacterSkillDetails.Where(c=>c.SkillDetailsName.Contains(value, StringComparison.InvariantCulture)).Select(c=>c.SkillDetailsName).ToArray();
        }
        private async Task<IEnumerable<int>> SearchSkillLevel(string value)
        {
            if (Convert.ToInt32(value) == 0)
                return CharacterSkillDetailNumbers.Select(c=>c.Level).ToArray();
            if (int.TryParse(value, out int parsedValue))
                return CharacterSkillDetailNumbers.Where(c=>c.Level == parsedValue && c.CharacterSkillDetailId == CharacterSkillDetail.Id).Select(c=>c.Level).ToArray();
            // If parsing fails, return an empty list
            return Enumerable.Empty<int>();

        }
        private async Task TextChangedSkillDetailName(string value)
        {
            var matchSkillDetail = CharacterSkillDetails.FirstOrDefault(x => x.SkillDetailsName == value);
            if (matchSkillDetail is not null)
                CharacterSkillDetail = matchSkillDetail;
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
                CharacterSkillDetailNumbers.FirstOrDefault(
                    x => x.CharacterSkillDetailId == CharacterSkillDetails.Find(x => x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName)?.Id && x.Level.ToString() == value);
                if (matchSkillDetailLevel is not null)
                    CharacterSkillDetailNumber.NumberMultipliers = new List<NumberMultiplier>(matchSkillDetailLevel.NumberMultipliers);
                else
                    CharacterSkillDetailNumber.NumberMultipliers.Clear();

            }
        }
        private async Task Save()
        {
            if (CharacterSkillDetails.Find(x => x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName)
                is not null)
                CharacterSkillDetailNumber.CharacterSkillDetailId = CharacterSkillDetails.FirstOrDefault(x => x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName).Id;
            else
            {
                CharacterSkillDetail.CharacterSkillId = SkillId;
                await AdminRepository.SavesAsync(CharacterSkillDetail);
                CharacterSkillDetailNumber.CharacterSkillDetailId = CharacterSkillDetail.Id;
            }

            var Match = CharacterSkillDetailNumbers.Find(x => x.Level == CharacterSkillDetailNumber.Level
                                                                        && x.CharacterSkillDetailId == CharacterSkillDetailNumber.CharacterSkillDetailId);
            if (Match is not null)
            {
                await AdminRepository.UpdatesAsync(CharacterSkillDetailNumber);
                foreach(var NumberMultiplier in CharacterSkillDetailNumber.NumberMultipliers)
                {
                    await AdminRepository.UpdatesAsync(NumberMultiplier);
                }
            }
                
            else
                await AdminRepository.SavesAsync(CharacterSkillDetailNumber);
            MudDialog.Close(DialogResult.Ok(true));
        }
        private async Task Delete<T>(T entity) where T : class
        {
            var dialog = await DialogService.ShowAsync<ConfirmDialog>("Confirmation");
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                try
                {
                    await AdminRepository.DeleteAsync<T>(entity);
                }
                catch (Exception ex)
                {
                    Snackbar.Add(ex.Message, Severity.Error, config => { config.HideIcon = true; });
                }
                Snackbar.Add("Delete Successful", Severity.Success, config => { config.HideIcon = true; });
                StateHasChanged();
            }
        }
    }
}
