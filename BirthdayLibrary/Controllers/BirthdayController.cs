using System;
using System.Collections.Generic;
using System.Linq;

using BirthdayLibrary.Models;
using BirthdayLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace BirthdayLibrary.Controllers
{
    public class BirthdayController : Controller
    {
        private readonly IBirthdayDB _birthdayDB;

        public BirthdayController(IBirthdayDB birthdayDB)
        {
            _birthdayDB = birthdayDB;
        }
        // GET: Birthday
        public ActionResult Index()
        {
            var birthday = _birthdayDB.GetAll();
            var paginIndex = 1;
            if (!string.IsNullOrEmpty(Request.Query["pagina"]))
            {
                paginIndex = Convert.ToInt32(Request.Query["pagina"]);
            }
            ViewBag.paginationDefault = paginIndex;
            ViewBag.byPagin = 5;
            ViewBag.totalRegistry = _birthdayDB.Total();
            ViewBag.pagination = _birthdayDB.Buscar(ViewBag.byPagin, paginIndex );
            DateTime todayBirthday = DateTime.Now;

            var birthdayList = _birthdayDB.GetAll();

            ViewBag.Pessoas = birthdayList;

            List<BirthdayModel> list = birthdayList.Where(p => p.DataNascimento.Day == todayBirthday.Day && p.DataNascimento.Month == todayBirthday.Month).ToList();

            ViewBag.AniversariantesDoDia = list;

            ViewBag.Pessoas = birthdayList.Where(x => x.DataNascimento.Year == todayBirthday.Year).ToList();

            ModelState.Clear();
            return View(birthday);
        }

        // GET: Birthday/Details/5
        public ActionResult Details(int id)
        {
            BirthdayModel birthdayDetails = _birthdayDB.GetById(id);
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
                _birthdayDB.Insert(birthdayCreate);

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
            BirthdayModel birthdayEdit = _birthdayDB.GetById(id);
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
                    _birthdayDB.Update(birthdayEdit);
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
            
            BirthdayModel birthdayDelete = _birthdayDB.GetById(id);
           
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

                _birthdayDB.Delete(birthdayDelete.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Ordenada()
        {

            

            return View();

        }
    }
}