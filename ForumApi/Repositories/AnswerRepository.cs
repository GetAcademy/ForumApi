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

        public async Task<IEnumerable<Answer>> GetAllAsync(int categoryId, int postId)
        {
            return (await GetAllAsyn()).Where(p => p.CategoryId == categoryId && p.PostId == postId).ToList();
        }

        public async Task<Answer> GetSingleAsyn(int categoryId, int postId, int answerId)
        {
            return await _context.Set<Answer>().FindAsync(categoryId, postId, answerId);
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

        protected override async Task<Answer> Find(Answer t)
        {
            return await GetSingleAsyn(t.CategoryId, t.PostId, t.AnswerId);
        }
    }
}
