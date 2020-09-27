using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IOperationServices
    {
        Task<string> IsServiceOk(string value);
        Task<List<Meal>> GetAllMeals();
        Task<bool> CreateNewMeal(Meal meal);
        Task<bool> UpdateMeal(Meal meal);
        Task<bool> DeleteMeal(string mealId);
        Task<Meal> GetMealById(string mealId);
    }
}
