using Data.Interfaces;
using Data.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MongoDbRespository : IDataRepository
    {
        private readonly string _connectionString = "mongodb+srv://test-user-ti:0CfqQoHsAl4l6bbp@cluster0.uhkcm.azure.mongodb.net/RestaurantDB";
        private readonly string _dataBaseName = "RestaurantDB";
        private readonly string _mealsCollectionName = "Meals";

        private readonly IMongoCollection<Meal> _meals;

        public MongoDbRespository()
        {
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase(_dataBaseName);
            _meals = database.GetCollection<Meal>(_mealsCollectionName);
        }

        public async Task<string> IsDataRepositoryOk(string value)
        {
            return string.Format("All data is Ok {0}", value);
        }

        public async Task<List<Meal>> GetAllMeals()
        {
            return await _meals.Find(s => true).ToListAsync();
        }

        public async Task<bool> CreateNewMeal(Meal meal)
        {
            await _meals.InsertOneAsync(meal);
            if (string.IsNullOrEmpty(meal.Id))
                return false;
            return true;
        }

        public async Task<Meal> GetMealById(string mealId)
        {
            return await _meals.Find<Meal>(c => c.Id == mealId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateMeal(Meal meal)
        {
            await _meals.ReplaceOneAsync(c => c.Id == meal.Id, meal);
            return true;
        }

        public async Task<bool> DeleteMeal(Meal mealFounded)
        {
            await _meals.DeleteOneAsync(c => c.Id == mealFounded.Id);
            return true;
        }
    }
}
