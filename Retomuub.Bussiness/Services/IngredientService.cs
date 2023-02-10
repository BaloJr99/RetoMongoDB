using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Retomuub.Bussiness.Interfaces;
using Retomuub.Bussiness.Repositories;
using Retomuub.Data.DTO;
using Retomuub.Data.Model;
using MongoDB.Driver;
using MongoDB.Bson;
using AutoMapper;

namespace Retomuub.Bussiness.Services
{
    public class IngredientService: IIngredientCollection
    {
        private readonly IMongoCollection<Ingredient> _ingredient;
        private readonly IMapper _mapper;

        public IngredientService(IMongoCollection<Ingredient> ingredient, IMapper mapper)
        {
            _ingredient = ingredient;
            _mapper = mapper;
        }

        public async Task DeleteIngredient(string id)
        {
            var filter = Builders<Ingredient>.Filter.Eq( s => s.Id, new ObjectId(id));
            await _ingredient.DeleteOneAsync( filter );
        }

        public async Task<List<IngredientDTO>> GetAllIngredients()
        {
            var ingredients = await _ingredient.FindAsync(new BsonDocument());
            return _mapper.Map<List<IngredientDTO>>(ingredients.ToList());
        }

        public async Task<IngredientDTO> GetIngredientById(string id)
        {
            var ingredient = await _ingredient.FindAsync(new BsonDocument{{ "_id", new ObjectId(id) }});
            return _mapper.Map<IngredientDTO>(ingredient.First());
        }

        public async Task InsertIngredient(IngredientDTO ingredientDTO)
        {
            await _ingredient.InsertOneAsync(_mapper.Map<Ingredient>(ingredientDTO));
        }

        public async Task UpdateIngredient(IngredientDTO ingredientDTO)
        {
            var filter = Builders<Ingredient>.Filter.Eq(s => s.Id, ingredientDTO.Id);
            await _ingredient.ReplaceOneAsync( filter, _mapper.Map<Ingredient>(ingredientDTO));
        }
    }
}