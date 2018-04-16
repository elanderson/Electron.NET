using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ElectronNET.API.Entities;
using ElectronNET.API;

namespace ElectronTest.Controllers
{
    public class TrayController : Controller
    {
        public IActionResult Index()
        {
            if (!HybridSupport.IsElectronActive ||
                Electron.Tray.MenuItems.Count != 0)
            {
                return Ok();
            }

            var menu = new MenuItem[] {
                new MenuItem { Label = "Create Contact", Click = () => Electron.WindowManager.BrowserWindows.First().LoadURL($"http://localhost:{BridgeSettings.WebPort}/Contacts/Create")},
                new MenuItem { Label = "Remove", Click = () => Electron.Tray.Destroy()}
            };

            Electron.Tray.Show("/Assets/Stock-Person.png", menu);
            Electron.Tray.SetToolTip("Contact Management");

            return Ok();
        }
    }
}