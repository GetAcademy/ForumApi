using ForumApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApi.Interfaces
{
    public interface IVoteRepository : IGenericRepository<Vote>
    {
        // If you need to customize your entity actions you can put here 
        Task<IEnumerable<Vote>> GetAllPostVotesAsync(int postId, int categoryId);
        Task<IEnumerable<Vote>> GetAllAnswerVotesAsync(int answerId, int postId, int categoryId);
    }
}
