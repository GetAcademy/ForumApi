using System.Linq;
using System.Threading.Tasks;
using ForumApi.Models;
using ForumApi.Interfaces;
using System.Collections.Generic;

namespace ForumApi.Repositories
{
    public class VotesRepository : GenericRepository<Vote>, IVoteRepository
    {
        public VotesRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Vote>> GetAllPostVotesAsync(int postId)
        {
            return (await GetAllAsyn()).Where(p => p.VoteParent == postId).ToList();
        }

        public Task<IEnumerable<Vote>> GetAllAnswerVotesAsync(int answerId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Vote>> GetAllAnswerVotesAsync(int answerId, int postId)
        {
            return (await GetAllAsyn()).Where(a => a.VoteParent == answerId).ToList();
        }

        public async Task<Vote> GetSingleAsyn(int voteParent, int voteId)
        {
            return await _context.Set<Vote>().FindAsync(voteParent, voteId);
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