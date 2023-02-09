using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Retomuub.Bussiness.Interfaces;
using Retomuub.Data.Model;

namespace Retomuub.Client.Controllers
{
    public class MealController : Controller
    {
        private IMealCollection _meal;

        public MealController(IMealCollection meal)
        {
            _meal = meal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeals()
        {
            var meals = await _meal.GetAllMeals();
            return Ok(meals);
        }

        [HttpGet]
        [Route("/Meal/GetMeal/{id}")]
        public async Task<IActionResult> GetMeal(string id)
        {
            var meal = await _meal.GetMealById( id );
            return Ok(meal);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeal([FromBody]Meal meal)
        {
            await _meal.InsertMeal( meal );
            return Ok(new { success = true });
        }

        [HttpPut]
        [Route("/Meal/UpdateMeal/{id}")]
        public async Task<IActionResult> UpdateMeal([FromBody]Meal meal, string id)
        {
            meal.Id = new ObjectId(id);
            await _meal.UpdateMeal( meal );
            return Ok(new { success = true });
        }

        [HttpDelete]
        [Route("/Meal/DeleteMeal/{id}")]
        public async Task<IActionResult> DeleteMeal(string id)
        {
            await _meal.DeleteMeal( id );
            return Ok(new { success = true });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}