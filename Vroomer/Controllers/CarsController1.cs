using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vroomer.Models;

namespace Vroomer.Controllers
{
    public class CarsController1 : Controller
    {
        private readonly CarDbContext _db;
        public CarsController1(CarDbContext db)
        {
            _db = db;
        }
            
        public IActionResult Index()
        {
            IEnumerable<Car> objCarsList = _db.Cars.ToList();
            return View(objCarsList);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car obj)
        {
            if (ModelState.IsValid)
            {
                _db.Cars.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
           
        }
        //GET
        public IActionResult Edit(int? ID)
        {
            if (ID== null || ID == 0)
            {
                return NotFound();
            }
            var CarFromDB = _db.Cars.Find(ID);
            if (CarFromDB == null)
            {
                return NotFound();
            }
            return View(CarFromDB);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Car obj)
        {
            if (ModelState.IsValid)
            {
                _db.Cars.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        //GET
        public IActionResult Delete(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var CarFromDB = _db.Cars.Find(ID);
            if (CarFromDB == null)
            {
                return NotFound();
            }
            return View(CarFromDB);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAcar(int?ID)
        {
            var obj = _db.Cars.Find(ID);
            if (obj==null)
            {
                return NotFound();
            }
                _db.Cars.Remove(obj);
                _db.SaveChanges();
                 return RedirectToAction("Index");
           

        }
    }
}
