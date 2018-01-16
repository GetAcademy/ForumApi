using ForumApi.Models;

namespace ForumApi.DataAccess
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        // If you need to customize your entity actions you can put here 
        new Post Get(int postId);
    }
}
