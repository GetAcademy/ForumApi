using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    [Route("api/categories/{CategoryId}/posts/{PostId}/votes")]
    public class PostVotesApiController : Controller
    {
        private readonly IVoteRepository _postVotesRepository;

        public PostVotesApiController(IVoteRepository postVotesRepository)
        {
            _postVotesRepository = postVotesRepository;
        }

        [HttpGet]
        public Task<IEnumerable<Vote>> GetVotes(int categoryId, int postId)
        {
            
            return _postVotesRepository.GetAllPostVotesAsync(categoryId, postId);
        }

        [Route("{VoteId}")]
        [HttpGet]
        public async Task<IEnumerable<Vote>> GetSinglePostVote(int categoryId, int postId, int answerId, int voteId)
        {
            return await _postVotesRepository.GetSingleAsync(categoryId, postId, answerId, voteId);
        }

        //public string UserId => ((TfsoIdentity) User.Identity).TfsoUserId;

        [HttpPost]
        public async Task<Vote> AddVote(int categoryId, int postId, [FromBody] Vote vote)
        {
            vote.CategoryId = categoryId;
            vote.PostId = postId;
            await _postVotesRepository.AddAsyn(vote);
            await _postVotesRepository.SaveAsync();
            return vote;
        }

        [Route("{VoteId}")]
        [HttpPut]
        public async Task<Vote> ReplaceVote(int categoryId, int postId, int answerId, int voteId, [FromBody] Vote vote)
        {
            vote.CategoryId = categoryId;
            vote.PostId = postId;
            vote.AnswerId = answerId;
            vote.VoteId = voteId;
            var updated = await _postVotesRepository.UpdateAsyn(vote);
            return updated;
        }

        [Route("{VoteId}")]
        [HttpPatch]
        public async Task<Vote> UpdatePost([FromBody] Vote vote)
        {
            var updated = await _postVotesRepository.UpdateAsyn(vote);
            return updated;
        }

        [Route("{VoteId}")]
        [HttpDelete]
        public string Delete(int categoryId, int postId, int answerId, int voteId, [FromBody] Vote vote)
        {
            vote.CategoryId = categoryId;
            vote.PostId = postId;
            vote.AnswerId = answerId;
            vote.VoteId = voteId;
            _postVotesRepository.Delete(vote);
            return "Vote deleted successfully!";
        }

        protected override void Dispose(bool disposing)
        {
            _postVotesRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
