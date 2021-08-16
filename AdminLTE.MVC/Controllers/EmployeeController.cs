using AdminLTE.Helpers;
using AdminLTE.Models;
using AdminLTE.Models.ViewModels;
using AdminLTE.MVC.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdminLTE.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _wb;
        public EmployeeController(ApplicationDbContext db, IWebHostEnvironment wb)
        {
            _db = db;
            _wb = wb;
        }

        public IActionResult Index(EmployeesLocalCommunitiesVM otherVm = null, int page = 1, IndexMode mode = IndexMode.Show, MessageType message = MessageType.NONE)
        {
            int pageSize = 10;

            IEnumerable<Employee> employees;

            if (otherVm == null || otherVm.Filter == null || otherVm.Filter.LCId == 0)
                employees = _db.Employees.Include(x => x.LocalCommunity);
            else
                employees = _db.Employees.Where(x => x.LocalCommunityId == otherVm.Filter.LCId).Include(x => x.LocalCommunity);

            int empCount = employees.Count();
            employees = employees.Skip((page - 1) * pageSize).Take(pageSize);

            EmployeesLocalCommunitiesVM vm = new EmployeesLocalCommunitiesVM()
            {
                Employees = employees,
                LocalCommunities = _db.LocalCommunities.Select(x => new SelectListItem()
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                }),
                Filter = otherVm.Filter == null ? new EmployeesFilterVM() : otherVm.Filter,
                Mode = mode,
                PageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = empCount },
                IsFilter = false
            };

            switch (message)
            {
                case MessageType.ADD:
                    ViewBag.JavaScriptFunction = string.Format("ShowMessage('{0}', '{1}');", "Робітник успішно добавлений", "success");
                    break;
                case MessageType.REMOVE:
                    ViewBag.JavaScriptFunction = string.Format("ShowMessage('{0}', '{1}');", "Робітник успішно видалений", "success");
                    break;
                case MessageType.EDIT:
                    ViewBag.JavaScriptFunction = string.Format("ShowMessage('{0}', '{1}');", "Робітник успішно відредагований", "success");
                    break;
                case MessageType.ERROR:
                    ViewBag.JavaScriptFunction = string.Format("ShowMessage('{0}', '{1}');", "Виникла помилка", "error");
                    break;
            }

            return View(vm);
        }
        public IActionResult Remove()
        {
            return RedirectToAction(nameof(Index), new { mode = IndexMode.SelectForRemove });
        }
        public IActionResult Edit()
        {
            return RedirectToAction(nameof(Index), new { mode = IndexMode.SelectForEdit });
        }
        [HttpPost]
        public IActionResult Edit(int id)
        {
            return RedirectToAction(nameof(Upsert), new { id = id });
        }
        [HttpPost]
        public IActionResult Remove(int id)
        {
            try
            {
                Employee emp = _db.Employees.Find(id);
                if (emp == null)
                    return NotFound();

                _db.Remove(emp);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index), new { otherVm = (EmployeesLocalCommunitiesVM)null, page = 1, mode = IndexMode.Show, message = MessageType.REMOVE });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index), new { otherVm = (EmployeesLocalCommunitiesVM)null, page = 1, mode = IndexMode.Show, message = MessageType.ERROR });
            }

        }
        public IActionResult Upsert(int? id)
        {
            EmployeeLCVM lCVM = new EmployeeLCVM()
            {
                LocalCommunities = _db.LocalCommunities.Select(x => new SelectListItem()
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                })
            };

            if (id == null)
                lCVM.Employee = new Employee();
            else
                lCVM.Employee = _db.Employees.Find(id);

            if (lCVM.Employee == null)
                return NotFound();

            return View(lCVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(EmployeeLCVM emp)
        {
            //HACK: Тут косяк. Чогось модель не проходить валідацію, через те, що employee.LocalCommunity == null, тому я провіряю чи помилка лоше одна, конкретно по ньому
            if (!ModelState.IsValid && ModelState.ErrorCount == 1)
            {
                MessageType messType = MessageType.ADD;
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _wb.WebRootPath;
                string upload = webRootPath + ENV.ImagePath;

                string fileName = "";
                string extensions = "";
                if (files.Count() == 0)
                {
                    fileName = ENV.NoImageName;
                }
                else
                {
                    fileName = Guid.NewGuid().ToString();
                    extensions = Path.GetExtension(files[0].FileName);
                }

                if (emp.Employee.Id == 0)
                {
                    if (files.Count() != 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extensions), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                    }

                    emp.Employee.PhotoPath = fileName + extensions;
                    _db.Employees.Add(emp.Employee);
                }
                else
                {
                    messType = MessageType.EDIT;
                    var formObject = _db.Employees.AsNoTracking().FirstOrDefault(u => u.Id == emp.Employee.Id);
                    if (files.Count > 0)
                    {
                        var oldFile = Path.Combine(upload, formObject.PhotoPath);
                        if (System.IO.File.Exists(oldFile))
                            System.IO.File.Delete(oldFile);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extensions), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        emp.Employee.PhotoPath = fileName + extensions;
                    }
                    else
                    {
                        emp.Employee.PhotoPath = formObject.PhotoPath;
                    }
                    _db.Employees.Update(emp.Employee);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index), new { otherVm = (EmployeesLocalCommunitiesVM)null, page = 1, mode = IndexMode.Show, message = messType });
            }
            return RedirectToAction(nameof(Index), new { otherVm = (EmployeesLocalCommunitiesVM)null, page = 1, mode = IndexMode.Show, message = MessageType.ERROR });
        }
        public IActionResult Cancel()
        {
            return RedirectToAction(nameof(Upsert));
        }
    }
}
