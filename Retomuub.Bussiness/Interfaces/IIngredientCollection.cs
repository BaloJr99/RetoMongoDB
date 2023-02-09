using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Retomuub.Data.Model;

namespace Retomuub.Bussiness.Interfaces
{
    public interface IIngredientCollection
    {
        Task InsertIngredient(Ingredient ingredient);
        Task UpdateIngredient(Ingredient ingredient);
        Task DeleteIngredient(string id);
        Task<List<Ingredient>> GetAllIngredients();
        Task<Ingredient> GetIngredientById(string id);
    }
}