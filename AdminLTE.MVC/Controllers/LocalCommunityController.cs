using AdminLTE.Models;
using AdminLTE.Models.ViewModels;
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
            LocalCommunityVM lcvm = new LocalCommunityVM();
            lcvm.LocalCommunities = _db.LocalCommunities;
            lcvm.LocalCommunity = new LocalCommunity();

            return View(lcvm);
        }

        [HttpPost]
        public IActionResult Add(string title)
        {
            LocalCommunity lc = new LocalCommunity() { Title = title };
            _db.LocalCommunities.Add(lc);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //TODO: Notifications (до прикладу при добавленні і т.д.)
    }
}
