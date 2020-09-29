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
        private readonly Post _fakePostModel;

        public OperationsControllerTest()
        {
            _operationServices = new Mock<IOperationServices>();
            _operationServices.Setup(x => x.IsServiceOk(It.IsAny<string>())).ReturnsAsync("Hello World!");

            _fakePostModel = new Post()
            {
                Id = "1234",
                BlogId = "1",
                CreatedDate = "01-01-2020",
                EntryName = "Mi primera entrada",
                EntryText = "Esta es la primera publicacion que se realiza por medio del web api"
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
        public async Task TestGetAllPost_Returns_Ok()
        {
            var list = new List<Post>();
            list.Add(_fakePostModel);

            _operationServices.Setup(x => x.GetAllPosts()).Returns(Task.FromResult(list));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.GetAllPosts();
            Assert.IsNotNull(result);
            var objectResult = result as OkObjectResult;
            var resultValueList = objectResult.Value as List<Post>;
            var resultValue = resultValueList.FirstOrDefault();

            Assert.AreEqual(_fakePostModel.Id, resultValue.Id);
            Assert.AreEqual(_fakePostModel.BlogId, resultValue.BlogId);
            Assert.AreEqual(_fakePostModel.CreatedDate, resultValue.CreatedDate);
            Assert.AreEqual(_fakePostModel.EntryName, resultValue.EntryName);
            Assert.AreEqual(_fakePostModel.EntryText, resultValue.EntryText);
        }

        [TestMethod]
        public async Task GetPostById_Return_data()
        {
            _operationServices.Setup(x => x.GetPostById(It.IsAny<string>())).Returns(Task.FromResult(_fakePostModel));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.GetPostById("1234");
            Assert.IsNotNull(result);
            var objectResult = result as OkObjectResult;
            var resultValueList = objectResult.Value as Post;

            Assert.AreEqual(_fakePostModel.Id, resultValueList.Id);
            Assert.AreEqual(_fakePostModel.BlogId, resultValueList.BlogId);
            Assert.AreEqual(_fakePostModel.CreatedDate, resultValueList.CreatedDate);
            Assert.AreEqual(_fakePostModel.EntryName, resultValueList.EntryName);
            Assert.AreEqual(_fakePostModel.EntryText, resultValueList.EntryText);
        }

        [TestMethod]
        public async Task GetPostById_Return_NotFound()
        {
            _operationServices.Setup(x => x.GetPostById(It.IsAny<string>())).Returns(Task.FromResult((Post)null));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.GetPostById("1234");
            Assert.IsNotNull(result);
            var objectResult = result as StatusCodeResult;
            Assert.AreEqual(404, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task TestCreateNewPost_Return_True()
        {
            _operationServices.Setup(x => x.CreateNewPost(It.IsAny<Post>())).Returns(Task.FromResult(true));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.CreateNewPost(_fakePostModel);
            Assert.IsNotNull(result);
            var objectResult = result as OkObjectResult;
            Assert.IsTrue((bool)objectResult.Value);
        }

        [TestMethod]
        public async Task TestCreateNewPost_Return_False()
        {
            _operationServices.Setup(x => x.CreateNewPost(It.IsAny<Post>())).Returns(Task.FromResult(false));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.CreateNewPost(_fakePostModel);
            Assert.IsNotNull(result);
            var objectResult = result as OkObjectResult;
            Assert.IsFalse((bool)objectResult.Value);
        }

        [TestMethod]
        public async Task TestUpdatePost_Return_NoContent()
        {
            _operationServices.Setup(x => x.UpdatePost(It.IsAny<Post>())).Returns(Task.FromResult(true));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.UpdatePost(_fakePostModel);
            Assert.IsNotNull(result);
            var objectResult = result as StatusCodeResult;
            Assert.AreEqual(204, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task TestUpdatePost_Return_NotFound()
        {
            _operationServices.Setup(x => x.UpdatePost(It.IsAny<Post>())).Returns(Task.FromResult(false));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.UpdatePost(_fakePostModel);
            Assert.IsNotNull(result);
            var objectResult = result as StatusCodeResult;
            Assert.AreEqual(404, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task TestDeletePost_Return_NoContent()
        {
            _operationServices.Setup(x => x.DeletePost(It.IsAny<string>())).Returns(Task.FromResult(true));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.DeletePost("123");
            Assert.IsNotNull(result);
            var objectResult = result as StatusCodeResult;
            Assert.AreEqual(204, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task TestDeletePost_Return_NotFound()
        {
            _operationServices.Setup(x => x.DeletePost(It.IsAny<string>())).Returns(Task.FromResult(false));
            var optController = new OperationsController(_operationServices.Object);
            var result = await optController.DeletePost("123");
            Assert.IsNotNull(result);
            var objectResult = result as StatusCodeResult;
            Assert.AreEqual(404, objectResult.StatusCode);
        }
    }
}
