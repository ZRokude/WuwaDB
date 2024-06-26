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
        private List<Character> Characters { get; set; } = new();

        private Dictionary<string, string> imageCard = new();

        private bool isLoading = false;
        protected override async void OnInitialized()
        {
            isLoading = true;

            Characters = await UserRepository.GetToListAsync<Character>();
            isLoading = false;
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (Characters.Count > 0)
                {
                    foreach (var Character in Characters)
                    {
                        var Image = await UserRepository.GetDataAsync<Character_ImageCard>(new { CharacterId = Character.Id });
                        if (Image is not null)
                            SetImage(Character.Name, Image.Image);
                    }
                }
                await InvokeAsync(StateHasChanged);
            }
        }
        private void SetImage(string type, byte[] image)
        {
            string imageSrc = Convert.ToBase64String(image);
            string imageString = string.Format("data:image/jpeg;base64,{0}", imageSrc);
            imageCard.TryAdd(type, imageString);
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
