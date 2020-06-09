using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3DPrintingService.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _3DPrintingService.Pages
{
    public class DownloadModel : PageModel
    {
        [HttpGet("Download")]
        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppFile FileToDownload = new AppFile();
            _3DPS RequestDirector = new _3DPS();
            FileToDownload = RequestDirector.DownloadAppFile(id);

            Response.Clear();
            Response.ContentType = FileToDownload.FileType;


            return File(FileToDownload.Content, "application/force-download", FileToDownload.FileName);
        }
    }
}
