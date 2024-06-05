using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog
{
    public partial class EditCharacterStats
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

        private async Task Save()
        {
            if (StatsExist is not true)
                CharacterStats.CharacterId = CharacterId;
            await AdminRepository.SavesAsync(CharacterStats);
            StateHasChanged();
            MudDialog.Close(DialogResult.Ok(true));
        }

        public void Overload(int a, string b, object c)
        {
            int z = a + Convert.ToInt32(b);
        }
        public void Overload(int a, string b)
        {

        }
        public void Overload(int a)
        {

        }

        public void Overload()
        {

        }
    }
}
