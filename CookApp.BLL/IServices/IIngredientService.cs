using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface IIngredientService
    {
        Task<Ingredient> GetIngredientsByProduct();
        Task<Ingredient> GetById();
        Task AddNewIngredient();
        Task UpdateIngredient();
        Task DeleteIngredient();

    }
}
