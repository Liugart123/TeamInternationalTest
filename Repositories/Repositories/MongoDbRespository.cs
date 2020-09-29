using Data.Interfaces;
using Data.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MongoDbRespository : IDataRepository
    {
        private readonly string _connectionString = "mongodb+srv://test-user-ti:0CfqQoHsAl4l6bbp@cluster0.uhkcm.azure.mongodb.net/BlogPostDb";
        private readonly string _dataBaseName = "BlogPostDb";
        private readonly string _postCollection = "Post";

        private readonly IMongoCollection<Post> _post;

        public MongoDbRespository()
        {
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase(_dataBaseName);
            _post = database.GetCollection<Post>(_postCollection);
        }

        public async Task<string> IsDataRepositoryOk(string value)
        {
            return string.Format("All data is Ok {0}", value);
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _post.Find(s => true).ToListAsync();
        }

        public async Task<bool> CreateNewPost(Post post)
        {
            await _post.InsertOneAsync(post);
            if (string.IsNullOrEmpty(post.Id))
                return false;
            return true;
        }

        public async Task<Post> GetPostById(string postId)
        {
            return await _post.Find<Post>(c => c.Id == postId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            await _post.ReplaceOneAsync(c => c.Id == post.Id, post);
            return true;
        }

        public async Task<bool> DeletePost(Post postFounded)
        {
            await _post.DeleteOneAsync(c => c.Id == postFounded.Id);
            return true;
        }
    }
}
