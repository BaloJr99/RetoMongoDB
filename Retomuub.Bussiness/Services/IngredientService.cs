using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Retomuub.Bussiness.Interfaces;
using Retomuub.Bussiness.Repositories;
using Retomuub.Data.Model;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Retomuub.Bussiness.Services
{
    public class IngredientService: IIngredientCollection
    {
        private readonly IMongoCollection<Ingredient> _ingredient;

        public IngredientService(IMongoCollection<Ingredient> ingredient)
        {
            _ingredient = ingredient;
        }

        public async Task DeleteIngredient(string id)
        {
            var filter = Builders<Ingredient>.Filter.Eq( s => s.Id, new ObjectId(id));
            await _ingredient.DeleteOneAsync( filter );
        }

        public async Task<List<Ingredient>> GetAllIngredients()
        {
            var ingredients = await _ingredient.FindAsync(new BsonDocument());
            return ingredients.ToList();
        }

        public async Task<Ingredient> GetIngredientById(string id)
        {
            var ingredient = await _ingredient.FindAsync(new BsonDocument{{ "_id", new ObjectId(id) }});
            return ingredient.First();
        }

        public async Task InsertIngredient(Ingredient ingredient)
        {
            await _ingredient.InsertOneAsync(ingredient);
        }

        public async Task UpdateIngredient(Ingredient ingredient)
        {
            var filter = Builders<Ingredient>.Filter.Eq(s => s.Id, ingredient.Id);

            await _ingredient.ReplaceOneAsync( filter, ingredient );
        }
    }
}