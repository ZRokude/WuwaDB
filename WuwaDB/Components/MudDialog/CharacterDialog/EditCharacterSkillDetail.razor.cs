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
        private Dictionary<int, Character_Skill_Detail_Number> CharacterSkillDetailNumberKey { get; set; } = new();
        protected override async void OnInitialized()
        {
            CharacterSkillDetails = await UserRepository.GetToListAsync<Character_Skill_Detail>
                (new { CharacterSkillId = SkillId });
            CharacterSkillDetailNumbers = await UserRepository.GetToListAsync<Character_Skill_Detail_Number>
                (new { CharacterSkillId = SkillId }, new string[] { "Character_Skill_Detail" }, new string[] { "NumberMultipliers" });
            StateHasChanged();
        }
        private void AddCharacterSkillDetailNumberKey() =>
            CharacterSkillDetailNumberKey[CharacterSkillDetailNumberKey.Count + 1] = new Character_Skill_Detail_Number();
        private void AddIndexListNumber(int key) =>
            CharacterSkillDetailNumberKey[key].NumberMultipliers.Add(new NumberMultiplier { Number = 0, Multiplier = null});
        private void MinIndexListNumber(int key )=>
                CharacterSkillDetailNumberKey[key].NumberMultipliers.RemoveAt(CharacterSkillDetailNumberKey[key].NumberMultipliers.Count - 1);
            
        private async Task<IEnumerable<string>> SearchSkillDetailName(string value)
        {
            if (string.IsNullOrEmpty(value))
                return CharacterSkillDetails.Select(c=>c.SkillDetailsName).ToArray();
            return CharacterSkillDetails.Where(c=>c.SkillDetailsName.Contains(value, StringComparison.InvariantCultureIgnoreCase)).Select(c=>c.SkillDetailsName).ToArray();
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

        private async void TextChangedSkillDetailName(string value)
        {
            var matchSkillDetail = CharacterSkillDetails.FirstOrDefault(x => x.SkillDetailsName == value);
            if (matchSkillDetail is not null)
                CharacterSkillDetail = matchSkillDetail;
            if (string.IsNullOrEmpty(value))
            {
                CharacterSkillDetailNumberKey.Clear();
            }
        }

        private void TextChangedSkillLevel(string value, int key)
        { 
            if (string.IsNullOrEmpty(value))
            {
                CharacterSkillDetailNumberKey[key].Level = 0;
                CharacterSkillDetailNumberKey[key].NumberMultipliers.Clear();
            }
            else
            {
                var matchSkillDetailLevel =
                CharacterSkillDetailNumbers.FirstOrDefault(
                    x => x.CharacterSkillDetailId == CharacterSkillDetails.Find(x => x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName)?.Id && x.Level.ToString() == value);
                if (matchSkillDetailLevel is not null)
                    CharacterSkillDetailNumberKey[key].NumberMultipliers = new List<NumberMultiplier>(matchSkillDetailLevel.NumberMultipliers);
                else
                    CharacterSkillDetailNumberKey[key].NumberMultipliers.Clear();

            }
        }
        private async Task Save()
        {
            if (CharacterSkillDetails.Find(x => x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName)
                is null)
            {
                CharacterSkillDetail.CharacterSkillId = SkillId;
                await AdminRepository.SavesAsync(CharacterSkillDetail);
            }
            foreach (var key in CharacterSkillDetailNumberKey.Keys)
            {
                if (CharacterSkillDetails.Find(x => x.SkillDetailsName == CharacterSkillDetail.SkillDetailsName) is not null)
                    CharacterSkillDetailNumberKey[key].CharacterSkillDetailId = CharacterSkillDetail.Id;
                var Match = CharacterSkillDetailNumbers.Find(x => x.Level == CharacterSkillDetailNumberKey[key].Level
                                                                        && x.CharacterSkillDetailId == CharacterSkillDetailNumber.CharacterSkillDetailId);
                if (Match is not null)
                {
                    await AdminRepository.UpdatesAsync(CharacterSkillDetailNumberKey[key]);
                    foreach (var NumberMultiplier in CharacterSkillDetailNumberKey[key].NumberMultipliers)
                    {
                        await AdminRepository.UpdatesAsync(NumberMultiplier);
                    }
                }
                else
                    await AdminRepository.SavesAsync(CharacterSkillDetailNumberKey[key]);
            }
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
