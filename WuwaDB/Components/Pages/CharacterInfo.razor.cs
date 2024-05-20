using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using WuwaDB.DBAccess.Repository;
using WuwaDB.DBAccess.Entities.Character;

namespace WuwaDB.Components.Pages
{
    public partial class CharacterInfo
    {
        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private UserRepository UserRepository { get; set; }

        [Parameter]
        public string CharacterName { get; set; }
        public Character_Skill CharacterSkill { get; set; }
        public Character_Skill_Perform  CharacterSkillPerform { get; set; }
        public Character_Skill_Perform_Level CharacterSkillPerformLevel { get; set; }

        public Character_Stats_Base CharacterStats { get; set; }
        
        protected override async void OnInitialized()
        {
            

            StateHasChanged();
        }
    }
}
