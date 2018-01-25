using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    [Route("api/categories/{CategoryId}/posts/{PostId}/answers/{AnswerId}/votes")]
    public class AnswerVotesApiController : Controller
    {
        private readonly IVoteRepository _answerVotesRepository;

        public AnswerVotesApiController(IVoteRepository answerVotesRepository)
        {
            _answerVotesRepository = answerVotesRepository;
        }

        [HttpGet]
        public Task<IEnumerable<Vote>> GetVotes(int answerId, int postId, int categoryId)
        {
            return _answerVotesRepository.GetAllAnswerVotesAsync(answerId, postId, categoryId);
        }

        [Route("{VoteId}")]
        [HttpGet]
        public async Task<IEnumerable<Vote>> GetSingleAnswerVote(int categoryId, int postId, int answerId, int voteId)
        {
            return await _answerVotesRepository.GetSingleAnswerVoteAsync(categoryId, postId, answerId, voteId);
        }

        [HttpPost]
        public async Task<Vote> AddVote(int categoryId,int answerId, int postId, [FromBody] Vote vote)
        {
            vote.CategoryId = categoryId;
            vote.PostId = postId;
            vote.AnswerId = answerId;
            await _answerVotesRepository.AddAsyn(vote);
            await _answerVotesRepository.SaveAsync();
            return vote;
        }

        [Route("{VoteId}")]
        [HttpPut]
        public async Task<Vote> ReplaceVote([FromBody] Vote vote)
        {
            var updated = await _answerVotesRepository.UpdateAsyn(vote, vote.VoteId);
            return updated;
        }

        [Route("{VoteId}")]
        [HttpPatch]
        public async Task<Vote> UpdatePost([FromBody] Vote vote)
        {
            var updated = await _answerVotesRepository.UpdateAsyn(vote, vote.VoteId);
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
            _answerVotesRepository.Delete(vote);
            return "Vote deleted successfully!";
        }

        protected override void Dispose(bool disposing)
        {
            _answerVotesRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
