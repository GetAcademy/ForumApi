using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.Interfaces;
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
        public Task<IEnumerable<Models.Vote>> GetVotes(int answerId)
        {
            return _answerVotesRepository.GetAllAnswerVotesAsync(answerId);
        }

        [Route("{VoteId}")]
        [HttpGet]
        public async Task<Models.Vote> GetSinglePost(int voteId)
        {
            return await _answerVotesRepository.GetAsync(voteId);
        }

        [HttpPost]
        public async Task<Models.Vote> AddVote(int answerId, [FromBody] Models.Vote vote)
        {

            vote.VoteParent = answerId;
            await _answerVotesRepository.AddAsyn(vote);
            await _answerVotesRepository.SaveAsync();
            return vote;
        }

        [Route("{VoteId}")]
        [HttpPut]
        public async Task<Models.Vote> ReplaceVote([FromBody] Models.Vote vote)
        {
            var updated = await _answerVotesRepository.UpdateAsyn(vote, vote.VoteId);
            return updated;
        }

        [Route("{VoteId}")]
        [HttpPatch]
        public async Task<Models.Vote> UpdatePost([FromBody] Models.Vote vote)
        {
            var updated = await _answerVotesRepository.UpdateAsyn(vote, vote.VoteId);
            return updated;
        }

        [Route("{VoteId}")]
        [HttpDelete]
        public string Delete(int id)
        {
            _answerVotesRepository.Delete(_answerVotesRepository.Get(id));
            return "Vote deleted successfully!";
        }

        protected override void Dispose(bool disposing)
        {
            _answerVotesRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
