using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Retomuub.Bussiness.Interfaces;
using Retomuub.Data.DTO;

namespace Retomuub.Client.Controllers
{    
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> CreateIngredient([FromBody]IngredientDTO ingredient)
        {
            if( ModelState.IsValid ){
                await _ingredient.InsertIngredient( ingredient );
            }
            return Ok(new { success = ModelState.IsValid });
        }

        [HttpPut]
        [Route("/Ingredient/UpdateIngredient/{id}")]
        public async Task<IActionResult> UpdateIngredient([FromBody]IngredientDTO ingredient, string id)
        {
            ingredient.Id = ObjectId.Parse(id);
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