using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayLibrary.Models;
using BirthdayLibrary.Repositories;
using Microsoft.AspNetCore.Http;
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
            return View(birthday);
        }

        // GET: Birthday/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Birthday/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Birthday/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BirthdayModel birthdayModel)
        {
            try
            {
                // TODO: Add insert logic here
                _birthdayDB.Insert(birthdayModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Birthday/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BirthdayModel birthdayModel = _birthdayDB.GetById(id);
            if(birthdayModel == null)
            {
                return NotFound();
            }
            return View(birthdayModel);
        }

        // POST: Birthday/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Birthday/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Birthday/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}