using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IOperationServices
    {
        Task<string> IsServiceOk(string value);
        Task<List<Post>> GetAllPosts();
        Task<bool> CreateNewPost(Post meal);
        Task<bool> UpdatePost(Post meal);
        Task<bool> DeletePost(string mealId);
        Task<Post> GetPostById(string mealId);
    }
}
