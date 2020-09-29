using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDataRepository
    {
        Task<string> IsDataRepositoryOk(string value);
        Task<List<Post>> GetAllPosts();
        Task<bool> CreateNewPost(Post meal);
        Task<Post> GetPostById(string mealId);
        Task<bool> UpdatePost(Post meal);
        Task<bool> DeletePost(Post mealFounded);
    }
}
