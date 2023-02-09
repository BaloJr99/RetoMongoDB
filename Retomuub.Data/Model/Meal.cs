using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retomuub.Data.Model
{
    public class Meal
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}