using System.Linq;
using System.Threading.Tasks;
using ForumApi.Models;

namespace ForumApi.DataAccess
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(DataContext context) : base(context)
        {
        }

        public Answer Get(int answerId)
        {
            var query = GetAll().FirstOrDefault(p => p.AnswerId == answerId);
            return query;
        }

        public async Task<Answer> GetSingleAsyn(int answerId)
        {
            return await _context.Set<Answer>().FindAsync(answerId);
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
