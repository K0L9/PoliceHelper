using AdminLTE.Helpers;
using AdminLTE.Models;
using AdminLTE.Models.ViewModels;
using AdminLTE.MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Controllers
{
    public enum MessageType
    {
        NONE,
        ADD,
        REMOVE,
        EDIT,
        EXIST_WORKERS,
        ERROR,
    }
    public class LocalCommunityController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LocalCommunityController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int page = 1, MessageType message = MessageType.NONE)
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

            switch (message)
            {
                case MessageType.ADD:
                    ViewBag.JavaScriptFunction = string.Format("ShowMessage('{0}', '{1}');", "Громада успішно добавлена", "success");
                    break;
                case MessageType.REMOVE:
                    ViewBag.JavaScriptFunction = string.Format("ShowMessage('{0}', '{1}');", "Громада успішно видалена", "success");
                    break;
                case MessageType.EDIT:
                    ViewBag.JavaScriptFunction = string.Format("ShowMessage('{0}', '{1}');", "Громада успішно відредагована", "success");
                    break;
                case MessageType.EXIST_WORKERS:
                    ViewBag.JavaScriptFunction = string.Format("ShowMessage('{0}', '{1}');", "Неможливо видалити громаду, видаліть працівників", "error");
                    break;
                case MessageType.ERROR:
                    ViewBag.JavaScriptFunction = string.Format("ShowMessage('{0}', '{1}');", "Виникла помилка", "error");
                    break;
            }

            return View(lcvm);
        }
        public IActionResult Add(string title)
        {
            if (ModelState.IsValid)
            {
                LocalCommunity lc = new LocalCommunity() { Title = title };
                _db.LocalCommunities.Add(lc);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index), new { page = 1, message = MessageType.ADD });
            }

            return RedirectToAction(nameof(Index), new { page = 1, message = MessageType.ERROR });
        }
        public IActionResult Edit(int id, string title)
        {
            try
            {
                LocalCommunity lc = _db.LocalCommunities.Find(id);
                if (lc == null)
                    return NotFound();

                lc.Title = title;
                _db.SaveChanges();
                return RedirectToAction(nameof(Index), new { page = 1, message = MessageType.EDIT });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index), new { page = 1, message = MessageType.ERROR });
            }
        }
        [HttpPost]
        public IActionResult Remove(int id)
        {
            try
            {
                LocalCommunity lc = _db.LocalCommunities.Include(x => x.Employees).SingleOrDefault(x => x.Id == id);
                if (lc == null)
                    return NotFound();

                if (lc.Employees.Count != 0)
                    return RedirectToAction(nameof(Index), new { page = 1, message = MessageType.EXIST_WORKERS });

                _db.LocalCommunities.Remove(lc);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index), new { page = 1, message = MessageType.REMOVE });
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index), new { page = 1, message = MessageType.ERROR });
            }

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
