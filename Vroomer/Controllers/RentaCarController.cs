using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Vroomer.Models;

namespace Vroomer.Controllers
{
    public class RentaCarController : Controller
    {
        private readonly RentaCarDBContext _db;

        public RentaCarController(RentaCarDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<RentaCar> objrentedcarList = _db.RentedCars.ToList();
            return View(objrentedcarList);
        }




        // GET
        public IActionResult Create()
        {
            ViewBag.Cars = _db.Cars.ToList();
            ViewBag.Customers = _db.Customers.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RentaCar rentaCar)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the selected car from the database
                var car = _db.Cars.SingleOrDefault(c => c.id == rentaCar.CarId);
                decimal pricePerDay = Convert.ToDecimal(car.PricePerDAY);

                // Calculate the rental price
                TimeSpan rentalDuration = rentaCar.EndDate - rentaCar.StartDate;
                decimal rentalPrice = pricePerDay * (decimal)rentalDuration.TotalDays;

                // Check if the selected car is available for the specified period
                if (IsCarAvailable(rentaCar.CarId, rentaCar.StartDate, rentaCar.EndDate))
                {
                    // Set the rental price in the model and add it to the database
                    rentaCar.Price = rentalPrice;
                    _db.RentedCars.Add(rentaCar);
                    _db.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("StartDate", "The selected car is not available during this period.");
                }
            }

            ViewBag.Cars = _db.Cars.ToList();
            ViewBag.Customers = _db.Customers.ToList();
            return View(rentaCar);
        }

        private bool IsCarAvailable(int carId, DateTime startDate, DateTime endDate)
        {
            // Check if the car is already rented during the specified period
            var existingRental = _db.RentedCars
                .Where(r => r.CarId == carId)
                .FirstOrDefault(r =>
                    (startDate >= r.StartDate && startDate <= r.EndDate) ||
                    (endDate >= r.StartDate && endDate <= r.EndDate)
                );

            return existingRental == null;
        }

        //get

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CarFromDB = _db.RentedCars.Find(id);
            if (CarFromDB == null)
            {
                return NotFound();
            }
            return View(CarFromDB);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RentaCar rentaCar)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the selected car from the database
                var car = _db.Cars.SingleOrDefault(c => c.id == rentaCar.CarId);
                decimal pricePerDay = Convert.ToDecimal(car.PricePerDAY);

                // Calculate the rental price
                TimeSpan rentalDuration = rentaCar.EndDate - rentaCar.StartDate;
                decimal rentalPrice = pricePerDay * (decimal)rentalDuration.TotalDays;

                // Update the rental price in the model and save changes to the database
                rentaCar.Price = rentalPrice;
                _db.RentedCars.Update(rentaCar);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(rentaCar);
        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CarFromDB = _db.RentedCars.Find(id);
            if (CarFromDB == null)
            {
                return NotFound();
            }
            return View(CarFromDB);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteaReservation(int id)
        {
            var obj = _db.RentedCars.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.RentedCars.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Income()
        {
            var incomeByMonth = _db.RentedCars
                .GroupBy(r => new { r.StartDate.Year, r.StartDate.Month })
                .Select(group => new
                {
                    Month = $"{group.Key.Year}-{group.Key.Month}",
                    TotalIncome = group.Sum(r => r.Price)
                })
                .ToList();

            ViewBag.IncomeByMonth = incomeByMonth;

            return View();
        }
    }
}

