using System.Linq;
using System.Threading.Tasks;
using ForumApi.Models;

namespace ForumApi.DataAccess
{
    public class VotesRepository : GenericRepository<Vote>, IVoteRepository
        {
            public VotesRepository(DataContext context) : base(context)
            {
            }

            public Vote Get(string voteParent, int voteParentId, int voteId)
            {
                var query = GetAll().FirstOrDefault(b => b.VoteId == voteId && b.VoteParent == voteParent && b.VoteParentId == voteParentId);
                return query;
            }

            public async Task<Vote> GetSingleAsyn(string voteParent, int voteParentId, int voteId)
            {
                return await _context.Set<Vote>().FindAsync(voteParent, voteParentId, voteId);
            }

            public override Vote Update(Vote t, object key)
            {
                Vote exist = _context.Set<Vote>().Find(key);
                if (exist != null)
                {
                }
                return base.Update(t, key);
            }

            public async override Task<Vote> UpdateAsyn(Vote t, object key)
            {
                Vote exist = await _context.Set<Vote>().FindAsync(key);
                if (exist != null)
                {
                }
                return await base.UpdateAsyn(t, key);
            }

        }
    }