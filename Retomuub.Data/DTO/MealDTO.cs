using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Retomuub.Data.DTO
{
    public class MealDTO
    {
        [Required]
        public ObjectId Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public List<IngredientDTO> IngredientsList { get; set; } = new List<IngredientDTO>();
        public List<string> IngredientsId { get; set; } = new List<string>();
    }
}