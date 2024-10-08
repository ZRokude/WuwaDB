﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.JavaScript;
using System.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Memory;
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
        [Inject] IMemoryCache MemoryCache { get; set; }
        private List<Character> Characters { get; set; } = new();
        private List<Character_ImageCard> CharacterImageCards { get; set; } = new();

        private bool isLoading = false;
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            Characters = await UserRepository.GetToListAsync<Character>();
            isLoading = false;
            StateHasChanged();
        }

        private string GetImageRoot(string Name) => $"Character/Card/Card_{Name}.png";

        private string GetRarity(RarityType rarity) => rarity.ToString();
        
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
