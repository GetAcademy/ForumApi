using System.Linq;
using System.Threading.Tasks;
using ForumApi.Models;
using ForumApi.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Vote>> GetAllAnswerVotesAsync(int answerId, int postId, int categoryId)
        {
            return (await GetAllAsyn())
                .Where(p => p.CategoryId == categoryId && p.PostId == postId && p.AnswerId == answerId)
                .ToList();
        }
        public async Task<IEnumerable<Vote>> GetAllVotesAsync(int answerId, int postId, int categoryId)
        {
            return (await GetAllAsyn())
                .Where(p => p.CategoryId == categoryId && p.PostId == postId && p.AnswerId == answerId)
                .ToList();
        }

        public async Task<IEnumerable<Vote>> GetSingleAsync(int categoryId, int postId, int answerId, int voteId)
        {
            return (await GetAllAsyn())
                .Where(p => p.CategoryId == categoryId && p.PostId == postId && p.AnswerId == answerId && p.VoteId == voteId)
                .ToList();
        }

        public async Task<Vote> GetSingleAsyncs(int categoryId, int postId, int answerId, int voteId)
        {
            return await _context.Set<Vote>().SingleOrDefaultAsync(v => v.AnswerId == answerId && 
            v.PostId == postId && v.CategoryId == categoryId && v.VoteId == voteId);
        }

        protected override async Task<Vote> Find(Vote t)
        {
            return await GetSingleAsyncs(t.CategoryId, t.PostId, t.AnswerId, t.VoteId);
        }
    }
}