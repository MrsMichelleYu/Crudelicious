using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Crudelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace Crudelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = dbContext.Dishes.OrderByDescending(dish => dish.CreatedAt).ToList();
            return View(AllDishes);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("add")]
        public IActionResult Add(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                dbContext.Dishes.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("New");
        }

        [HttpGet("/{dishId}")]
        public IActionResult Description(int dishId)
        {
            Dish thisDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            return View("Description", thisDish);
        }

        [HttpGet("/delete/{dishId}")]
        public IActionResult Delete(int dishId)
        {
            Dish thisDish = dbContext.Dishes.SingleOrDefault(dish => dish.DishId == dishId);
            dbContext.Dishes.Remove(thisDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/edit/{dishId}")]
        public IActionResult Edit(int dishId)
        {
            Dish thisDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            return View("Edit", thisDish);
        }

        [HttpPost("/update/{dishId}")]
        public IActionResult Update(Dish formInfo, int dishId)
        {
            Dish thisDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            if (ModelState.IsValid)
            {
                thisDish.Name = formInfo.Name;
                thisDish.Chef = formInfo.Chef;
                thisDish.Tastiness = formInfo.Tastiness;
                thisDish.Calories = formInfo.Calories;
                thisDish.Description = formInfo.Description;
                thisDish.CreatedAt = DateTime.Now;
                thisDish.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", thisDish);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
