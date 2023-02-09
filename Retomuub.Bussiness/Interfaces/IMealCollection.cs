using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Retomuub.Data.Model;

namespace Retomuub.Bussiness.Interfaces
{
    public interface IMealCollection
    {
        Task InsertMeal(Meal meal);
        Task UpdateMeal(Meal meal);
        Task DeleteMeal(Meal meal);
        Task<List<Meal>> GetAllMeals();
        Task<Meal> GetMealById(string id);
    }
}