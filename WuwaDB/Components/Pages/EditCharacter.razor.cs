using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.Pages
{
    public partial class EditCharacter
    {
        [Inject] private UserRepository UserRepository { get; set; }
        [Inject] private AdminRepository AdminRepository { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid CharacterId { get; set; }
        private Character_Stats_Base CharacterStats { get; set; } = new();
        private bool StatsExist = false;
        protected override async void OnInitialized()
        {
            var filterObject = new { CharacterId = CharacterId };
            CharacterStats = await UserRepository.GetDataAsync<Character_Stats_Base>(filterObject);
            if (CharacterStats is not null)
                StatsExist = true;
            else
                CharacterStats = new();
                
            StateHasChanged();
        }

        private async Task StatsExistEvent()
        {
            if (StatsExist is true)
                await UpdateCharacterStatsAsync();
            else
                await AddCharacterStatsAsync();

            MudDialog.Close(DialogResult.Ok(true));
        }

        private async Task UpdateCharacterStatsAsync()
        {

        }
        private async Task AddCharacterStatsAsync()
        {
            CharacterStats.CharacterId = CharacterId;
            await AdminRepository.SavesAsync(CharacterStats);
            StateHasChanged();
            
        }
    }
}
