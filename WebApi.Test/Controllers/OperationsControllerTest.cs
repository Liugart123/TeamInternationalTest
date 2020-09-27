using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebApi.Test.Controllers
{
    [TestClass]
    public class OperationsControllerTest
    {
        private readonly Mock<IOperationServices> _operationServices;
        private readonly Meal _fakeMealTest;

        public OperationsControllerTest()
        {
            _operationServices = new Mock<IOperationServices>();
            _operationServices.Setup(x => x.IsServiceOk(It.IsAny<string>())).ReturnsAsync("Hello World!");

            _fakeMealTest = new Meal()
            {
                Id = "1234",
                Name = "Pizza",
                Decorations = "Gaseosa",
                Origin = "Italia",
                Price = "3000"
            };
        }

        [TestMethod]
        public async Task IsServiceOk_Returns_Ok()
        {
            const string expectedRes = "Hello World!";
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.VerifyIfApiIsOk("Hello World!");
            Assert.IsNotNull(result);
            var objectResult = result as OkObjectResult;
            Assert.AreEqual(expectedRes, objectResult.Value);
        }

        [TestMethod]
        public async Task TestGetAllMeals_Returns_Ok()
        {
            var list = new List<Meal>();
            list.Add(_fakeMealTest);

            _operationServices.Setup(x => x.GetAllMeals()).Returns(Task.FromResult(list));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.GetAllMeals();
            Assert.IsNotNull(result);
            var objectResult = result as OkObjectResult;
            var resultValueList = objectResult.Value as List<Meal>;
            var resultValue = resultValueList.FirstOrDefault();

            Assert.AreEqual(_fakeMealTest.Id, resultValue.Id);
            Assert.AreEqual(_fakeMealTest.Name, resultValue.Name);
            Assert.AreEqual(_fakeMealTest.Decorations, resultValue.Decorations);
            Assert.AreEqual(_fakeMealTest.Origin, resultValue.Origin);
            Assert.AreEqual(_fakeMealTest.Price, resultValue.Price);
        }

        [TestMethod]
        public async Task GetMealById_Return_data()
        {
            _operationServices.Setup(x => x.GetMealById(It.IsAny<string>())).Returns(Task.FromResult(_fakeMealTest));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.GetMealById("1234");
            Assert.IsNotNull(result);
            var objectResult = result as OkObjectResult;
            var resultValueList = objectResult.Value as Meal;

            Assert.AreEqual(_fakeMealTest.Id, resultValueList.Id);
            Assert.AreEqual(_fakeMealTest.Name, resultValueList.Name);
            Assert.AreEqual(_fakeMealTest.Decorations, resultValueList.Decorations);
            Assert.AreEqual(_fakeMealTest.Origin, resultValueList.Origin);
            Assert.AreEqual(_fakeMealTest.Price, resultValueList.Price);
        }

        [TestMethod]
        public async Task GetMealById_Return_NotFound()
        {
            _operationServices.Setup(x => x.GetMealById(It.IsAny<string>())).Returns(Task.FromResult((Meal)null));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.GetMealById("1234");
            Assert.IsNotNull(result);
            var objectResult = result as StatusCodeResult;
            Assert.AreEqual(404, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task TestCreateNewMeal_Return_True()
        {
            _operationServices.Setup(x => x.CreateNewMeal(It.IsAny<Meal>())).Returns(Task.FromResult(true));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.CreateNewMeal(_fakeMealTest);
            Assert.IsNotNull(result);
            var objectResult = result as OkObjectResult;
            Assert.IsTrue((bool)objectResult.Value);
        }

        [TestMethod]
        public async Task TestCreateNewMeal_Return_False()
        {
            _operationServices.Setup(x => x.CreateNewMeal(It.IsAny<Meal>())).Returns(Task.FromResult(false));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.CreateNewMeal(_fakeMealTest);
            Assert.IsNotNull(result);
            var objectResult = result as OkObjectResult;
            Assert.IsFalse((bool)objectResult.Value);
        }

        [TestMethod]
        public async Task TestUpdateMeal_Return_NoContent()
        {
            _operationServices.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(Task.FromResult(true));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.UpdateMeal(_fakeMealTest);
            Assert.IsNotNull(result);
            var objectResult = result as StatusCodeResult;
            Assert.AreEqual(204, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task TestUpdateMeal_Return_NotFound()
        {
            _operationServices.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(Task.FromResult(false));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.UpdateMeal(_fakeMealTest);
            Assert.IsNotNull(result);
            var objectResult = result as StatusCodeResult;
            Assert.AreEqual(404, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task TestDeleteMeal_Return_NoContent()
        {
            _operationServices.Setup(x => x.DeleteMeal(It.IsAny<string>())).Returns(Task.FromResult(true));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.DeleteMeal("123");
            Assert.IsNotNull(result);
            var objectResult = result as StatusCodeResult;
            Assert.AreEqual(204, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task TestDeleteMeal_Return_NotFound()
        {
            _operationServices.Setup(x => x.DeleteMeal(It.IsAny<string>())).Returns(Task.FromResult(false));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.DeleteMeal("123");
            Assert.IsNotNull(result);
            var objectResult = result as StatusCodeResult;
            Assert.AreEqual(404, objectResult.StatusCode);
        }
    }
}
