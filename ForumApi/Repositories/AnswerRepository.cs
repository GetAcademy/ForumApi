using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumApi.Models;
using ForumApi.Interfaces;

namespace ForumApi.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Answer>> GetAllAsync(int postId, int categoryId)
        {
            return (await GetAllAsyn()).Where(p => p.CategoryId == categoryId && p.PostId == postId).ToList();
        }

        public async Task<Answer> GetSingleAsyn(int categoryId, int postId, int answerId)
        {
            return await _context.Set<Answer>().FindAsync(answerId, postId, categoryId);
        }

        public override Answer Update(Answer t, object key)
        {
            Answer exist = _context.Set<Answer>().Find(key);
            if (exist != null)
            {
                t.CreatedBy = exist.CreatedBy;
                t.CreatedOn = exist.CreatedOn;
            }
            return base.Update(t, key);
        }

        public async override Task<Answer> UpdateAsyn(Answer t, object key)
        {
            Answer exist = await _context.Set<Answer>().FindAsync(key);
            if (exist != null)
            {
                t.CreatedBy = exist.CreatedBy;
                t.CreatedOn = exist.CreatedOn;
            }
            return await base.UpdateAsyn(t, key);
        }
    }
}
