using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Retomuub.Data.DTO;

namespace Retomuub.Bussiness.Interfaces
{
    public interface IMealCollection
    {
        Task InsertMeal(MealDTO meal);
        Task UpdateMeal(MealDTO meal);
        Task DeleteMeal(string id);
        Task<List<MealDTO>> GetAllMeals();
        Task<MealDTO> GetMealById(string id);
    }
}