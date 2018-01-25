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

        public async Task<IEnumerable<Vote>> GetAllPostVotesAsync(int postId, int categoryId)
        {
            return (await GetAllAsyn()).Where(p => p.PostId == postId && p.CategoryId == categoryId && p.AnswerId == 0).ToList();
        }

        //public async Task<IEnumerable<Vote>> GetAllAnswerVotesAsync(int postId, int categoryId)
        //{
        //    return await GetAll().Where(p => p.CategoryId == categoryId && p.PostId == postId && p.AnswerId != null).ToListAsync();
        //}
        public async Task<IEnumerable<Vote>> GetAllAnswerVotesAsync(int answerId, int postId, int categoryId)
        {
            return (await GetAllAsyn())
                .Where(p => p.CategoryId == categoryId && p.PostId == postId && p.AnswerId == answerId)
                .ToList();
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