using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retomuub.Data.Model
{
    public class Ingredient
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public string MeasureType { get; set; } = null!;
    }
}