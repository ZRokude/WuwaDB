﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Enum;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Components.MudDialog.CharacterDialog
{
    public partial class EditCharacterSkill
    {
        [Inject] private AdminRepository AdminRepository { get; set; }
        [Inject] private UserRepository UserRepository { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Guid CharacterId { get; set; }
        [Parameter] public SkillType SkillType { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] IWebHostEnvironment HostEnvironment { get; set; }
        private Character_Skill CharacterSkill { get; set; } = new();
        private Character_Skill_Image CharacterSkillImage { get; set; } = new();
        private Guid? CharacterSkillId;
        private bool SkillExist;
        IBrowserFile file;
        private string imageData;
        private Character Character { get; set; }

        protected override async void OnInitialized()
        {
            Character = await UserRepository.GetDataAsync<Character>(new{Id = CharacterId});
            CharacterSkill = await UserRepository.GetDataAsync<Character_Skill>(
                new
                {
                    Type = SkillType,
                    CharacterId
                });
            if (CharacterSkill is not null)
            {
                SkillExist = true;
                CharacterSkillId = CharacterSkill.Id;
            }
            else
                CharacterSkill = new();
            StateHasChanged();
        }
        private async void GetImage()
        {
            string imageSrc = Convert.ToBase64String(CharacterSkillImage.Image);
            imageData = string.Format("data:image/jpeg;base64,{0}", imageSrc);
        }
        private async Task FilesChanged(InputFileChangeEventArgs e)
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
                        CharacterSkillImage.Image = stream.ToArray();
                    }
                }
                else
                    Console.WriteLine("Something make file can't be read");
            }
            GetImage();
            StateHasChanged();
        }
        private async Task SaveCharacterSkill()
        {
            CharacterSkill.Type = SkillType;

            if (SkillExist is not true)
            {
                CharacterSkill.CharacterId = CharacterId;
                await AdminRepository.SavesAsync(CharacterSkill);
            }
            else
                await AdminRepository.UpdatesAsync(CharacterSkill);
            if(!imageData.IsNullOrEmpty())
            {
                string fileName = $"{CharacterSkill.Name}.png";
                var pathDirectory = Path.Combine(HostEnvironment.WebRootPath, "Character", "SkillIcon", Character.Name);
                var path = Path.Combine(HostEnvironment.WebRootPath, "Character", "SkillIcon", Character.Name, fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                var base64Data = imageData.Replace("data:image/jpeg;base64,", string.Empty)
                    .Replace("data:image/png;base64,", string.Empty);
                byte[] imageBytes = Convert.FromBase64String(base64Data);
                await File.WriteAllBytesAsync(path, imageBytes);
            }
            StateHasChanged();
            MudDialog.Close(DialogResult.Ok(true));
        }

        private void OpenSkillDetailDialog()
        {
            var options = new DialogOptions { CloseButton = true, CloseOnEscapeKey = true };
            var parameters = new DialogParameters<EditCharacterSkillDetail>();
            parameters.Add(nameof(EditCharacterSkillDetail.SkillId), CharacterSkill.Id);
            DialogService.Show<EditCharacterSkillDetail>("Character Skill Detail", parameters, options);
        }
        private async void OpenSkillDescriptionDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, CloseButton = true };
            var parameters = new DialogParameters<EditCharacterSkillDescription>();
            parameters.Add(nameof(EditCharacterSkillDescription.SkillId), CharacterSkill.Id);
            DialogService.Show<EditCharacterSkillDescription>("Character Skill Description", parameters, options);
        }

    }

}
