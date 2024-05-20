using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.DBAccess.Repository;
using WuwaDB.DBAccess.Entities.Character;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Components.Forms;

namespace WuwaDB.Components.Pages
{
    public partial class CreateCharacter
    {
        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private AdminRepository AdminRepository { get; set; }
        [Inject] private IWebHostEnvironment HostEnvironment { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        private Character Character { get; set; } = new Character();
        IBrowserFile file;
        private async Task Submit()                                             
        {
            if (file is not null)
            {

                //if (File.Exists(Path.Combine(HostEnvironment.WebRootPath, Character.Image)))
                //{
                //    File.Delete(Path.Combine(HostEnvironment.WebRootPath, Character.Image));
                //}

                // Check if the filename with the current image filename from db exists
                var path = Path.Combine(HostEnvironment.WebRootPath,"Character_Icon", file.Name);
                Character.Image = file.Name;
                int i = 1;
                while (File.Exists(path))
                {
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                    var extension = Path.GetExtension(file.Name);
                    var newFileName = $"{fileNameWithoutExtension}({i}){extension}";
                    path = Path.Combine(HostEnvironment.WebRootPath, "Character_Icon", newFileName);
                    Character.Image = newFileName;
                    i++;
                }
                var stream = file.OpenReadStream(maxAllowedSize:30*1024*1024);
                if (stream.CanRead)
                {
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        try
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error);
                        }
                    }
                }
            }
            await AdminRepository.CreateCharacter(Character);
            StateHasChanged();
            MudDialog.Close(DialogResult.Ok(true));
        }
        private void Cancel() => MudDialog.Cancel();

        private void OnFilesChanged(InputFileChangeEventArgs e)
        {
            file = e.File;
            if (file.Size > 20971520 )
            {
                Snackbar.Add("File is exceed limits, please try to put lower size", Severity.Warning);
                file = null;
            }
        }
    }
       
}
