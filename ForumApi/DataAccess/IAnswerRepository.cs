using ForumApi.Models;

namespace ForumApi.DataAccess
{
    public interface IAnswerRepository : IGenericRepository<Answer>
    {
        // If you need to customize your entity actions you can put here 
        Answer Get(int blogId);
    }
}
