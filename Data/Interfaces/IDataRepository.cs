using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDataRepository
    {
        Task<string> IsDataRepositoryOk(string value);
        Task<List<Meal>> GetAllMeals();
        Task<bool> CreateNewMeal(Meal meal);
        Task<Meal> GetMealById(string mealId);
        Task<bool> UpdateMeal(Meal meal);
        Task<bool> DeleteMeal(Meal mealFounded);
    }
}
