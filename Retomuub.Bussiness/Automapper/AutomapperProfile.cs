using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Retomuub.Data.DTO;
using Retomuub.Data.Model;

namespace Retomuub.Bussiness.Automapper
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {   
            CreateMap<IngredientDTO, Ingredient>().ReverseMap();
            CreateMap<MealDTO, Meal>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}