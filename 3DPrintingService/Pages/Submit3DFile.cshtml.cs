using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _3DPrintingService.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapstoneUI.Pages
{
    public class Submit3DFileModel : PageModel
    {
        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }
        public AppFile newAppFile { get; set; }
        public List<AppFile> FileList { get; set; }
        [BindProperty]
        public List<string> AvailableColors { get; set; }
        [BindProperty]
        public string ColorSelect { get; set; }
        [BindProperty]
        [MaxLength(300)]
        public string Comments { get; set; }
        public string Message { get; set; }

        public MemoryStream memoryStream { get; set; }
        public void OnGet()
        {
            _3DPS RequestDirector = new _3DPS();
            FileList = RequestDirector.GetAppFiles();
            AvailableColors = RequestDirector.GetColors();
        }
        public void OnPost()
        {
            _3DPS RequestDirector = new _3DPS();
            FileList = RequestDirector.GetAppFiles();

        }

        public async Task<IActionResult> OnPostUploadAsync()
        {


            using (memoryStream = new MemoryStream())
            {
                await FileUpload.FormFile.CopyToAsync(memoryStream);

                // Upload the file if less than 50 MB
                if (memoryStream.Length < 5068435456)
                {
                    var file = new AppFile()
                    {
                        Content = memoryStream.ToArray()
                    };

                    newAppFile = new AppFile();

                    _3DPS RequestDirector = new _3DPS();
                    bool confirmation;
                    file.FileName = FileUpload.FormFile.FileName;
                    file.FileType = Path.GetExtension(FileUpload.FormFile.FileName);
                    file.ColorName = ColorSelect;
                    file.Comments = Comments;
                    confirmation = RequestDirector.UploadAppFile(file);

                    FileList = RequestDirector.GetAppFiles();
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }

            Message = "File successfully uploaded!";
            return Page();
        }
    }
    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}

