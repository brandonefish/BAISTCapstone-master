using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using _3DPrintingService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CapstoneUI.Pages
{
    public class SubmitSearchRequestModel : PageModel
    {
        bool Confirmation { get; set; }

        [BindProperty]
        public string Description { get; set; }
        [BindProperty, Required]
        public string MultiPart { get; set; }


        public string Message { get; set; }

        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            _3DPS RequestDirector = new _3DPS();

            Confirmation = RequestDirector.CreateSearchRequest(MultiPart, Description);

            Message = "Your search request has been submitted!";
        }
    }
}
