using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog.CharacterDialog
{
    public partial class EditCharacterSkillDescription
    {
        [Inject] UserRepository UserRepository { get; set; }
        [Inject] AdminRepository AdminRepository { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid SkillId { get; set; }
        public List<Character_Skill_Description> CharacterSkillDescriptions { get; set; } = new();
        public Character_Skill_Description CharacterSkillDescription { get; set; } = new();
        public string[] DescTitles;
        protected override async void OnInitialized()
        {
            CharacterSkillDescriptions = await UserRepository.GetToListAsync<Character_Skill_Description>(new { CharacterSkillId = SkillId });
            if (CharacterSkillDescriptions.Count > 0)
            {
                DescTitles = new string[CharacterSkillDescriptions.Count];
                for (int i = 0; i < CharacterSkillDescriptions.Count; i++)
                {
                    DescTitles[i] = CharacterSkillDescriptions[i].DescriptionTitle;
                }
            }
            StateHasChanged();
        }
        private async Task<IEnumerable<string>> SearchDescTitle(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DescTitles;
            return DescTitles.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));

        }
        private async Task TextChanged(string value)
        {
            if (CharacterSkillDescriptions.Find(x => x.DescriptionTitle == value) is not null)
            {
                CharacterSkillDescription.Description = CharacterSkillDescriptions.FirstOrDefault(x => x.DescriptionTitle == value).Description;
                StateHasChanged();
            }
            else if (string.IsNullOrEmpty(value))
            {
                CharacterSkillDescription.Description = "";
            }
        }
        private async Task Save()
        {
            CharacterSkillDescription.CharacterSkillId = SkillId;
            var Match = CharacterSkillDescriptions.Find(x => x.DescriptionTitle == CharacterSkillDescription.DescriptionTitle);
            if (Match is not null)
            {
                CharacterSkillDescription.CharacterSkillId = CharacterSkillDescriptions.FirstOrDefault(x => x.DescriptionTitle == CharacterSkillDescription.DescriptionTitle).CharacterSkillId;
                await AdminRepository.UpdatesAsync(CharacterSkillDescription);
            }
            else
                await AdminRepository.SavesAsync(CharacterSkillDescription);
            StateHasChanged();
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}
