using System;
using System.Collections.Generic;
using System.Linq;
using BirthdayLibrary.Models;
using BirthdayLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BirthdayLibrary.Controllers
{
    public class BirthdayController : Controller
    {
        private readonly IBirthdayService _birthdayService;

        public BirthdayController(IBirthdayService birthdayService)
        {
            _birthdayService = birthdayService;
        }
        // GET: Birthday
        public ActionResult Index(int? pagina)
        {
            var birthday = _birthdayService.GetAll();

            ViewBag.AniversariantesDoDia = _birthdayService.BirthdaysOfTheDay();

            ViewBag.Pessoas = _birthdayService.OrderByBirthday();

            int paginSize = 8;
            int paginInicial = (pagina ?? 1);

            
            ModelState.Clear();

            return View(birthday.ToPagedList(paginInicial, paginSize));
        }

        // GET: Birthday/Details/5
        public ActionResult Details(int id)
        {
           BirthdayModel birthdayDetails = _birthdayService.GetById(id);
           return View(birthdayDetails);
        }

        // GET: Birthday/Create
        public ActionResult Create()
        {
           return View();
        }

        // POST: Birthday/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BirthdayModel birthdayCreate)
        {
            try
            {
                // TODO: Add insert logic here
               _birthdayService.Insert(birthdayCreate);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Birthday/Edit/5
        public ActionResult Edit(int id)
        {
            BirthdayModel birthdayEdit = _birthdayService.GetById(id);
            return View(birthdayEdit);
        }

        // POST: Birthday/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]BirthdayModel birthdayEdit)
        {
            try
            {
                // TODO: Add update logic here
                if(id != birthdayEdit.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    //_birthdayDB.Update(birthdayEdit);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(birthdayEdit);
            }
        }

        // GET: Birthday/Delete/5
        public IActionResult Delete(int id)
        {
            
            BirthdayModel birthdayDelete = _birthdayService.GetById(id);
           
            return View(birthdayDelete);
        }

        // POST: Birthday/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(BirthdayModel birthdayDelete)
        {
            try
            {
                // TODO: Add delete logic here
                _birthdayService.Delete(birthdayDelete.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}