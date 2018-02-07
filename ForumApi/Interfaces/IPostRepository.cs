using ForumApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApi.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        // If you need to customize your entity actions you can put here 
        //new Post Get(int postId);

        Task<IEnumerable<Post>> GetAllAsync(int categoryId);
        Task<Post> GetSingleAsyn(int postId, int categoryId);
        //Task<Post> UpdateAsync(int categoryId, Post post, int postPostId);
    }
}
