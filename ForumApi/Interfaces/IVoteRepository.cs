using ForumApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApi.Interfaces
{
    public interface IVoteRepository : IGenericRepository<Vote>
    {
        // If you need to customize your entity actions you can put here 
        Task<IEnumerable<Vote>> GetAllPostVotesAsync(int categoryId, int postId);
        Task<IEnumerable<Vote>> GetAllAnswerVotesAsync(int categoryId, int postId, int answerId);
        Task<IEnumerable<Vote>> GetSingleAsync(int categoryId, int postId, int answerId, int voteId);
    }
}
