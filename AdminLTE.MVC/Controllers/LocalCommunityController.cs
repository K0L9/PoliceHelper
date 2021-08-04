using AdminLTE.Models;
using AdminLTE.MVC.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Controllers
{
    public class LocalCommunityController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LocalCommunityController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(LocalCommunity lc)
        {
            return RedirectToAction(nameof(Index));
        }

        //TODO: Notifications (до прикладу при добавленні і т.д.)
    }
}
