using _3DPrintingService.TechnicalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3DPrintingService.Domain
{
    public class _3DPS
    {

        // BRANDON - Search Request
        public bool CreateSearchRequest(string multiPart, string description)
        {
            bool Confirmation;
            SearchRequestServices PrintManager = new SearchRequestServices();
            Confirmation = PrintManager.AddSearchRequest(multiPart, description);
            return Confirmation;
        }

        // BRANDON - Get search requests
        public List<SearchRequest> FindSearchRequests()
        {
            List<SearchRequest> requests;
            SearchRequestServices SearchRequestManager = new SearchRequestServices();
            requests = SearchRequestManager.GetSearchRequests();
            return requests;
        }

        // COLE - Color Management
        public bool AddColor(string colorName)
        {
            bool Confirmation = false;
            ColorManagementServices ColorManager = new ColorManagementServices();
            Confirmation = ColorManager.AddColor(colorName); // add color to db

            return Confirmation;
        }
        public bool SetAvailable(string colorName)
        {
            bool Confirmation = false;
            ColorManagementServices ColorManager = new ColorManagementServices();
            Confirmation = ColorManager.SetAvailable(colorName);

            return Confirmation;
        }

        // COLE - COLOR MANAGEMENT
        public bool RemoveColor(string colorNameRemove)
        {
            bool Confirmation = false;
            ColorManagementServices ColorManager = new ColorManagementServices();
            Confirmation = ColorManager.RemoveColor(colorNameRemove);

            return Confirmation;
        }

        // COLE - COLOR MANAGEMENT
        public List<string> GetColors()
        {
            List<string> availableColors;
            ColorManagementServices ColorManager = new ColorManagementServices();
            availableColors = ColorManager.GetAvailableColors();
            return availableColors;
        }

        // COLE - Color Management
        public List<Color> GetAllColors()
        {
            List<Color> allColors = new List<Color>();
            ColorManagementServices ColorManager = new ColorManagementServices();
            allColors = ColorManager.GetAllColors();
            return allColors;
        }

        // ASHTON - SUBMIT 3D FILE
        public bool UploadAppFile(AppFile newFile)
        {
            bool confirmation = false;
            AppFileServices AppFileManager = new AppFileServices();
            confirmation = AppFileManager.UploadAppFile(newFile);

            return confirmation;
        }

        // ASHTON - VIEW 3D FILE
        public List<AppFile> GetAppFiles()
        {
            List<AppFile> appfiles = new List<AppFile>();
            AppFileServices AppFileManager = new AppFileServices();
            appfiles = AppFileManager.GetUploadedFiles();

            return appfiles;
        }

        // ASHTON - VIEW 3D FILE
        public AppFile DownloadAppFile(int id)
        {
            AppFile DownloadFile = new AppFile();

            AppFileServices AppFileManager = new AppFileServices();
            DownloadFile = AppFileManager.DownloadAppFile(id);
            return DownloadFile;
        }
    }
}
