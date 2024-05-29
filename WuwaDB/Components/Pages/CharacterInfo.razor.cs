using System.Collections.Immutable;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Repository;
using WuwaDB.DBAccess.Entities.Character;

namespace WuwaDB.Components.Pages
{
    public partial class CharacterInfo
    {
        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private UserRepository UserRepository { get; set; }
        [Inject] public IDialogService DialogService { get; set; }
        [Parameter] public string CharacterName { get; set; }

        public Character_Skill CharacterSkill { get; set; } = new();
        public Character_Stats_Base CharacterStats { get; set; } = new();
        public Character character { get; set; } = new();

        protected override async void OnInitialized()
        {
            var _character = await UserRepository.FindCharacterAsync(CharacterName);
            if (_character is not null)
                character = _character; 
            StateHasChanged();
            
        }

        private async void OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<EditCharacter>();
            parameters.Add(x=> x.CharacterId, character.Id);
            var dialog = await DialogService.ShowAsync<EditCharacter>("Edit Character", parameters, options);
            var result = await dialog.Result;
        }
    }
}
