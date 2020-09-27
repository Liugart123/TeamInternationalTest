using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class OperationServices : IOperationServices
    {
        private readonly IDataRepository _dataRepository;

        public OperationServices(IDataRepository dataRep)
        {
            _dataRepository = dataRep;
        }

        public async Task<bool> CreateNewMeal(Meal meal)
        {
            return await _dataRepository.CreateNewMeal(meal);
        }

        public async Task<List<Meal>> GetAllMeals()
        {
            return await _dataRepository.GetAllMeals();
        }

        public async Task<string> IsServiceOk(string value)
        {
            return await _dataRepository.IsDataRepositoryOk(value);
        }

        public async Task<bool> UpdateMeal(Meal meal)
        {
            Meal mealFounded = await GetMealById(meal.Id);
            if (mealFounded == null) return false;
            return await _dataRepository.UpdateMeal(meal);
        }

        public async Task<bool> DeleteMeal(string mealId)
        {
            Meal mealFounded = await GetMealById(mealId);
            if (mealFounded == null) return false;
            return await _dataRepository.DeleteMeal(mealFounded);
        }

        public async Task<Meal> GetMealById(string mealId)
        {
            try
            {
                return await _dataRepository.GetMealById(mealId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
