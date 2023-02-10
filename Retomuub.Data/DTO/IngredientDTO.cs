using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retomuub.Data.DTO
{
    public class IngredientDTO
    {
        [Required]
        public ObjectId Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public decimal? Amount { get; set; } 
        [Required]
        public string MeasureType { get; set; } = null!;
    }
}