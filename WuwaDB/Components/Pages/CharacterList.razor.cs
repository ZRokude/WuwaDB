using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using System.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Enum;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.Pages
{
    public partial class CharacterList
    {
        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private UserRepository UserRepository { get; set; }
        private List<Character> CharList { get; set; } = new();
        protected override async void OnInitialized()
        {
            CharList = await UserRepository.GetCharacterAsync();

            StateHasChanged();
        }

        string GetWeaponImage(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.BroadBlade:
                    return "LustrousRazor";
                case WeaponType.Gauntlets:
                    return "AbyssSurge";
                case WeaponType.Gun:
                    return "StaticMist";
                case WeaponType.Rectifier:
                    return "CosmicRipples";
                case WeaponType.Sword:
                    return "EmeraldOfGenesis";
                default:
                    return null;
            }
        }
    }
}
