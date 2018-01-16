using System.Linq;
using System.Threading.Tasks;
using ForumApi.Models;

namespace ForumApi.DataAccess
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(DataContext context) : base(context){ }

        public Answer Get(int Id)
        {
            return GetAll().FirstOrDefault(b => b.AnswerId == Id);
        }

        public async Task<Answer> GetSingleAsyn(int id)
        {
            return await _context.Set<Answer>().FindAsync(id);
        }
    }
}
