using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MudBlazor;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog.CharacterDialog
{
    public partial class EditCharacterInfo
    {
        [Inject] AdminRepository AdminRepository { get; set; }
        [Inject] UserRepository UserRepository { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid CharacterId { get; set; }
        public Character Character { get; set; } = new();
        private IBrowserFile file;
        private Dictionary<string, string> imageData = new();
        protected override async void OnInitialized()
        {
            Character = await UserRepository.GetDataAsync<Character>(new { Id = CharacterId });
            if (Character is not null)
            {
                if (Character.ImageCard is not null)
                    SetImage(nameof(Character.ImageCard), Character.ImageCard);
                if (Character.ImageModel is not null)
                    SetImage(nameof(Character.ImageModel), Character.ImageModel);
            }
            StateHasChanged();
        }

        private async void SetImage(string type, byte[] imageBytes)
        {
            string imageSrc = Convert.ToBase64String(imageBytes);
            imageData.TryAdd(type, string.Format("data:image/jpeg;base64,{0}", imageSrc));
        }

        private async void OnChangedImageModel(InputFileChangeEventArgs e) =>
            await OnFilesChanged(e, nameof(Character.ImageModel));
        private async void OnChangedImageCard(InputFileChangeEventArgs e) =>
            await OnFilesChanged(e, nameof(Character.ImageCard));
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
                        SetImage(propertyName, propertyValue);
                    }
                }
                else
                    Console.WriteLine("Something make file can't be read");
            }
            
            StateHasChanged();
        }
        private async Task UpdateCharacter()
        {
            await AdminRepository.UpdatesAsync(Character);
            StateHasChanged();
            MudDialog.Close(DialogResult.Ok(true));
        }

    }

}
