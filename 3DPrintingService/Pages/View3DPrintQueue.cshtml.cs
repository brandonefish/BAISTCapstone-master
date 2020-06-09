using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3DPrintingService.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapstoneUI.Pages.Admin
{
    public class View3DFilesModel : PageModel
    {
        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }
        public AppFile newAppFile { get; set; }
        public List<AppFile> FileList { get; set; }

        public void OnGet()
        {
            _3DPS RequestDirector = new _3DPS();
            FileList = RequestDirector.GetAppFiles();
        }
    }
}