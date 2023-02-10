using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using Retomuub.Bussiness.Interfaces;
using Retomuub.Data.DTO;
using Retomuub.Data.Model;

namespace Retomuub.Bussiness.Services
{
    public class MealService: IMealCollection
    {
        private readonly IMongoCollection<Meal> _meal;
        private readonly IMongoCollection<Ingredient> _ingredient;
        private readonly IMapper _mapper;

        public MealService(IMongoCollection<Meal> meal, IMongoCollection<Ingredient> ingredient, IMapper mapper)
        {
            _meal = meal;
            _ingredient = ingredient;
            _mapper = mapper;
        }

        public async Task DeleteMeal(string id)
        {
            var filter = Builders<Meal>.Filter.Eq( s => s.Id, new ObjectId(id));
            await _meal.DeleteOneAsync( filter );
        }

        public async Task<List<MealDTO>> GetAllMeals()
        {
            var meals = (await _meal.FindAsync(new BsonDocument())).ToList();
            List<MealDTO> mealDTOs = _mapper.Map<List<MealDTO>>(meals);
            for (int i = 0; i < meals.Count(); i++) {
                var ids = meals.ElementAt(i).IngredientsId;
                mealDTOs.ElementAt(i).IngredientsList = _mapper.Map<List<IngredientDTO>>(_ingredient.AsQueryable().Where( ing => ids.Contains(ing.Id.ToString()) ).ToList());
            }
            return mealDTOs;
        }

        public async Task<MealDTO> GetMealById(string id)
        {
            var meal = await _meal.FindAsync(new BsonDocument{{ "_id", new ObjectId(id) }});
            MealDTO mealDTO = _mapper.Map<MealDTO>(meal.First());
            var ids =mealDTO.IngredientsId;
            mealDTO.IngredientsList = _mapper.Map<List<IngredientDTO>>(_ingredient.AsQueryable().Where( ing => ids.Contains(ing.Id.ToString()) ).ToList());
            return mealDTO;
        }

        public async Task InsertMeal(MealDTO mealDTO)
        {
            await _meal.InsertOneAsync(_mapper.Map<Meal>(mealDTO));
        }

        public async Task UpdateMeal(MealDTO mealDTO)
        {
            var meal = _mapper.Map<Meal>(mealDTO);
            var filter = Builders<Meal>.Filter.Eq(s => s.Id, meal.Id);
            await _meal.ReplaceOneAsync( filter, meal );
        }
    }
}