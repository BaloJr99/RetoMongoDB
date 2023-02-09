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
    public class IngredientController : Controller
    {
        private IIngredientCollection _ingredient;

        public IngredientController(IIngredientCollection ingredient)
        {
            _ingredient = ingredient;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredient.GetAllIngredients();
            return Ok(ingredients);
        }

        [HttpGet]
        [Route("/Ingredient/GetIngredient/{id}")]
        public async Task<IActionResult> GetIngredient(string id)
        {
            var ingredient = await _ingredient.GetIngredientById( id );
            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody]Ingredient ingredient)
        {
            await _ingredient.InsertIngredient( ingredient );
            return Ok(new { success = true });
        }

        [HttpPut]
        [Route("/Ingredient/UpdateIngredient/{id}")]
        public async Task<IActionResult> UpdateIngredient([FromBody]Ingredient ingredient, string id)
        {
            ingredient.Id = new ObjectId(id);
            await _ingredient.UpdateIngredient( ingredient );
            return Ok(new { success = true });
        }

        [HttpDelete]
        [Route("/Ingredient/DeleteIngredient/{id}")]
        public async Task<IActionResult> DeleteIngredient(string id)
        {
            await _ingredient.DeleteIngredient( id );
            return Ok(new { success = true });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}