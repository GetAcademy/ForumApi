using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.Models;

namespace ForumApi.Interfaces
{
    public interface IAnswerRepository : IGenericRepository<Answer>
    {
        // If you need to customize your entity actions you can put here 
        //new Answer Get(string answerParent, int answerParentId, int answerId);
        Task<IEnumerable<Answer>> GetAllAsync(int postId, int categoryId);
        Task<Answer> GetSingleAsyn(int categoryId, int postId, int answerId);
    }
}
