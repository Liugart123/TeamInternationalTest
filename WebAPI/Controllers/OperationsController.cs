using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Auth;

namespace WebAPI.Controllers
{
    [Route("api/operation")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationServices _operationServices;

        public OperationsController(IOperationServices opService)
        {
            _operationServices = opService;
        }

        [HttpGet]
        [Route("IsApiOk")]
        [ApiKeyAuth]
        public async Task<IActionResult> VerifyIfApiIsOk(string value)
        {
            string result = await SafeGet(s => s.IsServiceOk(value));
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllMeals")]
        [ApiKeyAuth]
        public async Task<IActionResult> GetAllMeals()
        {
            List<Meal> meals = await SafeGet(s => s.GetAllMeals());
            return Ok(meals);
        }

        [HttpGet]
        [Route("GetMealById")]
        [ApiKeyAuth]
        public async Task<IActionResult> GetMealById(string mealId)
        {
            Meal meal = await SafeGet(s => s.GetMealById(mealId));
            if (meal == null)
                return NotFound();
            return Ok(meal);
        }

        [HttpPost]
        [Route("CreateNewMeal")]
        [ApiKeyAuth]
        public async Task<IActionResult> CreateNewMeal(Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool result = await SafeGet(s => s.CreateNewMeal(meal));
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateMeal")]
        [ApiKeyAuth]
        public async Task<IActionResult> UpdateMeal(Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool res = await SafeGet(s => s.UpdateMeal(meal));
            if (res)
                return NoContent();
            return NotFound();
        }

        [HttpDelete]
        [Route("DeleteMeal")]
        [ApiKeyAuth]
        public async Task<IActionResult> DeleteMeal(string mealId)
        {
            bool res = await SafeGet(s => s.DeleteMeal(mealId));
            if (res)
                return NoContent();
            return NotFound();
        }

        //Safe get to handle exceptions
        private T SafeGet<T>(Func<IOperationServices, T> getter)
        {
            try
            {
                return getter(_operationServices);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
