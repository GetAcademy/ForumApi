using ForumApi.Models;

namespace ForumApi.DataAccess
{
    public interface IVoteRepository : IGenericRepository<Vote>
    {
        // If you need to customize your entity actions you can put here 
        new Vote Get(string voteParent, int voteParentId, int voteId);
    }
}
