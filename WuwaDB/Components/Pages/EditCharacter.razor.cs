using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.Pages
{
    public partial class EditCharacter
    {
        [Inject] private UserRepository UserRepository { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid characterId { get; set; }
        private Character_Stats_Base CharacterStats { get; set; }
        private bool StatsExist;
        protected override async void OnInitialized()
        {
            base.OnInitialized();

            CharacterStats = await UserRepository.GetCharacterStatsBaseAsync(characterId);
            if (CharacterStats is not null)
                StatsExist = true;
        }

        private async Task StatsExistEvent()
        {
            if (StatsExist is true)
            {
                UpdateCharacterStatsAsync();
            }
        }

        private async Task UpdateCharacterStatsAsync()
        {

        }
        private async Task AddCharacterStatsAsync()
        {

        }
    }
}
