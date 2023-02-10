using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Retomuub.Data.DTO;

namespace Retomuub.Bussiness.Interfaces
{
    public interface IIngredientCollection
    {
        Task InsertIngredient(IngredientDTO ingredient);
        Task UpdateIngredient(IngredientDTO ingredient);
        Task DeleteIngredient(string id);
        Task<List<IngredientDTO>> GetAllIngredients();
        Task<IngredientDTO> GetIngredientById(string id);
    }
}