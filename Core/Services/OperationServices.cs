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

        public async Task<bool> CreateNewPost(Post post)
        {
            return await _dataRepository.CreateNewPost(post);
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _dataRepository.GetAllPosts();
        }

        public async Task<string> IsServiceOk(string value)
        {
            return await _dataRepository.IsDataRepositoryOk(value);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            Post postFounded = await GetPostById(post.Id);
            if (postFounded == null) return false;
            return await _dataRepository.UpdatePost(post);
        }

        public async Task<bool> DeletePost(string postId)
        {
            Post postFounded = await GetPostById(postId);
            if (postFounded == null) return false;
            return await _dataRepository.DeletePost(postFounded);
        }

        public async Task<Post> GetPostById(string postId)
        {
            try
            {
                return await _dataRepository.GetPostById(postId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
