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
        [Route("GetAllPosts")]
        [ApiKeyAuth]
        public async Task<IActionResult> GetAllPosts()
        {
            List<Post> posts = await SafeGet(s => s.GetAllPosts());
            return Ok(posts);
        }

        [HttpGet]
        [Route("GetPostById")]
        [ApiKeyAuth]
        public async Task<IActionResult> GetPostById(string postId)
        {
            Post post = await SafeGet(s => s.GetPostById(postId));
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpPost]
        [Route("CreateNewPost")]
        [ApiKeyAuth]
        public async Task<IActionResult> CreateNewPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool result = await SafeGet(s => s.CreateNewPost(post));
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdatePost")]
        [ApiKeyAuth]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool res = await SafeGet(s => s.UpdatePost(post));
            if (res)
                return NoContent();
            return NotFound();
        }

        [HttpDelete]
        [Route("DeletePost")]
        [ApiKeyAuth]
        public async Task<IActionResult> DeletePost(string postId)
        {
            bool res = await SafeGet(s => s.DeletePost(postId));
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
