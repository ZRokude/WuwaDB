﻿using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [Inject] IWebHostEnvironment HostEnvironment { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid CharacterId { get; set; }
        public Character Character { get; set; } = new();
        public Character_ImageModel CharacterImageModel { get; set; } = new();
        public Character_ImageCard  CharacterImageCard { get; set; }=new();
        private IBrowserFile file;
        private Dictionary<string, string> imageData = new();
        protected override async void OnInitialized()
        {
            Character = await UserRepository.GetDataAsync<Character>(new { Id = CharacterId });
            if (Character is not null)
            {
                 var checkCharacterImageCard = await UserRepository.GetDataAsync<Character_ImageCard>(new { CharacterId = CharacterId });
                 var checkCharacterImageModel = await UserRepository.GetDataAsync<Character_ImageModel>(new { CharacterId = CharacterId });
                if (checkCharacterImageCard is not null)
                {
                    CharacterImageCard = checkCharacterImageCard;
                    SetImage(nameof(CharacterImageCard), CharacterImageCard.Image);
                }
                    
                if (checkCharacterImageModel is not null)
                {
                    CharacterImageModel = checkCharacterImageModel;
                    SetImage(nameof(CharacterImageModel), CharacterImageModel.Image);
                }
                    
            }
            StateHasChanged();
        }

        private async void SetImage(string type, byte[] imageBytes)
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
                        var imageData = stream.ToArray();
                        //var propertyInfo = typeof(T).GetProperty("Image");
                        //propertyInfo.SetValue(typeof(T), stream.ToArray());
                        //var propertyValue = propertyInfo.GetValue(typeof(T)) as byte[];
                        SetImage(propertyName, imageData);
                    }
                }
                else
                    Console.WriteLine("Something make file can't be read");
            }
            
            StateHasChanged();
        }
        private async Task UpdateCharacter()
        {
            if (imageData.TryGetValue(nameof(CharacterImageCard), out var imageCard))
            {
                var base64String = imageCard.Replace("data:image/jpeg;base64,", string.Empty);
                CharacterImageCard.CharacterId = Character.Id;
                CharacterImageCard.Image = Convert.FromBase64String(base64String);
                var check = await UserRepository.GetDataAsync<Character_ImageCard>(new { CharacterId = Character.Id });
                if (check is not null)
                    await AdminRepository.UpdatesAsync(CharacterImageCard);
                else 
                    await AdminRepository.SavesAsync(CharacterImageCard);

            }
            if (imageData.TryGetValue(nameof(CharacterImageModel), out var imageModel))
            {
                var base64String = imageModel.Replace("data:image/jpeg;base64,", string.Empty);
                CharacterImageModel.CharacterId = Character.Id;
                CharacterImageModel.Image = Convert.FromBase64String(base64String);
                var check = await UserRepository.GetDataAsync<Character_ImageModel>(new { CharacterId = Character.Id });
                if (check is not null)
                    await AdminRepository.UpdatesAsync(CharacterImageModel);
                else
                    await AdminRepository.SavesAsync(CharacterImageModel);
                
            }
            foreach (var image in imageData)
            {
                string folderName = image.Key.Replace("CharacterImage", string.Empty);
                string fileName = $"{folderName}_{Character.Name}.png";
                var path = Path.Combine(HostEnvironment.WebRootPath, "Character", folderName, fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                var base64Data = image.Value.Replace("data:image/jpeg;base64,", string.Empty)
                    .Replace("data:image/png;base64,", string.Empty);
                byte[] imageBytes = Convert.FromBase64String(base64Data);
                await File.WriteAllBytesAsync(path, imageBytes);
            }
            await AdminRepository.UpdatesAsync(Character);
            StateHasChanged();
            MudDialog.Close(DialogResult.Ok(true));
        }

    }

}
