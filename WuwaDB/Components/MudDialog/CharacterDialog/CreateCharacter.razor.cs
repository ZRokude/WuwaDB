using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Repository;
using WuwaDB.DBAccess.Entities.Character;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Components.Forms;

namespace WuwaDB.Components.MudDialog.CharacterDialog
{
    public partial class CreateCharacter
    {
        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private AdminRepository AdminRepository { get; set; }
        [Inject] private UserRepository UserRepository { get; set; }
        [Inject] private IWebHostEnvironment HostEnvironment { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        private Character Character { get; set; } = new();
        private Character_ImageCard CharacterImageCard { get; set; } = new();
        private Character_ImageModel CharacterImageModel { get; set;} = new();
        IBrowserFile file;
        private Dictionary<string, string> imageData = new();
        private async Task Submit()
        {
            await AdminRepository.SavesAsync(Character);
            var checkCharacterImageCard = await UserRepository.GetDataAsync<Character_ImageCard>(new {CharacterId = Character.Id});
            var checkCharacterImageModel = await UserRepository.GetDataAsync<Character_ImageModel>(new {CharacterId = Character.Id});
            if (checkCharacterImageCard != null)
                await AdminRepository.UpdatesAsync(CharacterImageCard);
            else
                await AdminRepository.SavesAsync(CharacterImageCard);
            if(checkCharacterImageModel is not null)
                await AdminRepository.UpdatesAsync(CharacterImageModel);
            else
                await AdminRepository.SavesAsync(CharacterImageModel);
            StateHasChanged();
            MudDialog.Close(DialogResult.Ok(true));
        }
        private void Cancel() => MudDialog.Cancel();
        private async void GetImage(string type, byte[] imageBytes)
        {
            string imageSrc = Convert.ToBase64String(imageBytes);
            if (!imageData.ContainsKey(type))
                imageData.TryAdd(type, string.Format("data:image/jpeg;base64,{0}", imageSrc));
            else
                imageData[type] = imageSrc;
        }
        private async void OnChangedImageModel(InputFileChangeEventArgs e) =>
            await OnFilesChanged(e, nameof(CharacterImageModel));
        private async void OnChangedImageCard(InputFileChangeEventArgs e) =>
            await OnFilesChanged(e, nameof(CharacterImageCard));
        private async Task OnFilesChanged(InputFileChangeEventArgs e, string propertyName)
        {
            file = e.File;
            if (file.Size > 20971520)
            {
                Snackbar.Add("File is exceed limits, please try to put lower size", Severity.Warning);
                file = null;
            }
            if (file is not null)
            {
                var fileRead = file.OpenReadStream(maxAllowedSize: 30 * 1024 * 1024);
                if (fileRead.CanRead)
                {
                    using (var stream = new MemoryStream())
                    {
                        await fileRead.CopyToAsync(stream);
                        var propertyInfo = typeof(Character).GetProperty(propertyName);
                        propertyInfo.SetValue(Character, stream.ToArray());
                        var propertyValue = propertyInfo.GetValue(Character) as byte[];
                        GetImage(propertyName, propertyValue);
                    }
                }
                else
                    Console.WriteLine("Something make file can't be read");
            }
            StateHasChanged();
        }
    }

}
