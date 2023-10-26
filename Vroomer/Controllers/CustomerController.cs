using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vroomer.Models;

namespace Vroomer.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerDBContext _db;
        public CustomerController(CustomerDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Customer> objCustomerList = _db.Customers.ToList();
            return View(objCustomerList);
        }
            //GET
            public IActionResult Create()
            {
                return View();
            }
        //post
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Customer obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Customers.Add(obj);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(obj);

        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Check if a customer with the same ID already exists
                bool customerExists = await _db.Customers.AnyAsync(c => c.id == customer.id);

                if (customerExists)
                {
                    ModelState.AddModelError("Id", "A customer with the same ID already exists.");
                    return View(customer);
                }

                // If the ID is unique, proceed with inserting the new customer
                _db.Add(customer);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        //GET
        public IActionResult Edit(string? ID)
            {
                if (ID == null || ID == "")
                {
                    return NotFound();
                }
                var CustomerFromDB = _db.Customers.Find(ID);
                if (CustomerFromDB == null)
                {
                    return NotFound();
                }
                return View(CustomerFromDB);
            }
            //post
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Customer customer)
            {
                if (ModelState.IsValid)
                {
                    _db.Customers.Update(customer);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(customer);

            }
            //GET
            public IActionResult Delete(string? id)
            {
                if (id == null || id == "")
                {
                    return NotFound();
                }
                var CarFromDB = _db.Customers.Find(id);
                if (CarFromDB == null)
                {
                    return NotFound();
                }
                return View(CarFromDB);
            }
            //post
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteAcustomer(string? id)
            {
                var obj = _db.Customers.Find(id);
                if (obj == null)
                {
                    return NotFound();
                }
                _db.Customers.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");


            }
        
    }
}
