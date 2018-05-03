using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ElectronNET.API;
using ElectronNET.API.Entities;

namespace ElectronTest.Controllers
{
    public class DialogsController : Controller
    {
        private static bool saveAdded;

        public IActionResult Index()
        {
            if (!HybridSupport.IsElectronActive || saveAdded) return Ok();

            Electron.IpcMain.On("save-dialog", async (args) =>
            {
                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                var options = new SaveDialogOptions
                {
                    Title = "Save contact as JSON",
                    Filters = new FileFilter[]
                    {
                        new FileFilter { Name = "JSON", Extensions = new string[] {"json" } }
                    }
                };

                var result = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, options);
                Electron.IpcMain.Send(mainWindow, "save-dialog-reply", result);
            });

            saveAdded = true;

            return Ok();
        }
    }
}