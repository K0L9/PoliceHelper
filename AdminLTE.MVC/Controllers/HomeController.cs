using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminLTE.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace AdminLTE.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Save(string image)
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveImage()
        {

            int len = (int)HttpContext.Request.Body.Length;
            byte[] buffer = new byte[len];
            int c = Request.Body.Read(buffer, 0, len);
            string png64 = Encoding.UTF8.GetString(buffer, 0, len);
            byte[] png = Convert.FromBase64String(png64);
            System.IO.File.WriteAllBytes("c:\\a.png", png);
            //string pngz = Encoding.UTF8.GetString(png, 0, png.Length);
            //code.....
            return RedirectToAction("Index", "Home");
        }
    }
}
