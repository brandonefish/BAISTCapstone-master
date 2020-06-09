using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3DPrintingService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapstoneUI.Pages.Admin
{
    public class ViewSearchRequestsModel : PageModel
    {
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string MultiPart { get; set; }

        public List<SearchRequest> SearchRequests { get; set; }


        public void OnGet()
        {
            SearchRequests = new List<SearchRequest>();
            _3DPS RequestDirector = new _3DPS();
            SearchRequests = RequestDirector.FindSearchRequests();
        }
    }
}