using AdminLTE.Helpers;
using AdminLTE.Models;
using AdminLTE.Models.ViewModels;
using AdminLTE.MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;

            IEnumerable<LocalCommunity> localCommunities = _db.LocalCommunities.Include(x => x.Employees);
            int lcCount = localCommunities.Count();
            localCommunities = localCommunities.Skip((page - 1) * pageSize).Take(pageSize);

            LocalCommunityVM lcvm = new LocalCommunityVM() 
            {
                LocalCommunities = localCommunities,
                LocalCommunity = new LocalCommunity(),
                PageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = lcCount }
            };

            return View(lcvm);
        }
        public IActionResult Add(string title)
        {
            LocalCommunity lc = new LocalCommunity() { Title = title };
            _db.LocalCommunities.Add(lc);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //TODO: Notifications (до прикладу при добавленні і т.д.)
        //TODO: Create pagination
        public IActionResult Edit(int id, string title)
        {
            LocalCommunity lc = _db.LocalCommunities.Find(id);
            if (lc == null)
                return NotFound();

            lc.Title = title;
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Remove(int id)
        {
            LocalCommunity lc = _db.LocalCommunities.Find(id);
            if (lc == null)
                return NotFound();

            _db.LocalCommunities.Remove(lc);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult ChooseOption(string title, int? id)
        {
            if (id == -1)
                return RedirectToAction(nameof(Add), new { title = title });
            else
                return RedirectToAction(nameof(Edit), new { id = id, title = title });
        }
    }
}

//TODO: перевести це на автомат. відсилку. (забрать нейм в інпутах, приймать в методі ChooseOption просто VM (Той же що приходить у view'шку))
