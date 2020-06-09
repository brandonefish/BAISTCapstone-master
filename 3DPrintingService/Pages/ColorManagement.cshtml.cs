using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3DPrintingService.Domain;
using _3DPrintingService.TechnicalServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapstoneUI.Pages.Admin
{
    public class ColorManagementModel : PageModel
    {
        [BindProperty]
        public string ColorName { get; set; }
        [BindProperty]
        public string ColorNameRemove { get; set; }
        [BindProperty]
        public bool Available { get; set; }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string Message2 { get; set; }
        [BindProperty]
        public List<string> AvailableColors { get; set; }
        [BindProperty]
        public string ColorSelect { get; set; }

        [BindProperty]
        public List<Color> AllColors { get; set; }
        public void OnGet()
        {
            _3DPS RequestDirector = new _3DPS();
            AllColors = RequestDirector.GetAllColors();

            

        }
        public void OnPostAddColor()
        {
            bool Confirmation = false;
            _3DPS RequestDirector = new _3DPS();
            //AvailableColors = RequestDirector.GetColors();
            AllColors = new List<Color>();
            AllColors = RequestDirector.GetAllColors();
            // add color to db
            _3DPrintingService.Domain.Color color = new Color();
            color.ColorName = ColorName;
            color.Available = false;

            if (AllColors.Contains(color))
                Confirmation = RequestDirector.SetAvailable(ColorName);
            else
                Confirmation = RequestDirector.AddColor(ColorName);

            // update status message
            if (Confirmation)
                Message = "Added " + ColorName + " successfully.";
            else
                Message = "Failed to add color.";

            AllColors = RequestDirector.GetAllColors();
        }
        public void OnPostRemoveColor()
        {
            bool Confirmation = false;
            _3DPS RequestDirector = new _3DPS();
            // remove color from DB
            Confirmation = RequestDirector.RemoveColor(ColorNameRemove);
            // remove color from LIST
            AvailableColors = RequestDirector.GetColors();
            // update status message
            if (Confirmation)
                Message2 = "Removed " + ColorName + " successfully.";
            else
                Message2 = "Failed to remove color.";

            AllColors = RequestDirector.GetAllColors();
        }
        public IActionResult OnPostReset()
        {
            //return RedirectToPage("Index"); // sends you to index page -- CHOOOSE ONE OF THESE 2

            return RedirectToPage("Colors"); // resets the page

            //return Page(); // just for the return if you do not want to redirect
        }
    }
}