using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    //[Route("api/posts/{PostId}/answers/{AnswerId}/votes")]
    public class AnswerVotesApiController : Controller
    {
        private readonly IVoteRepository _answerVotesRepository;

        public AnswerVotesApiController(IVoteRepository answerVotesRepository)
        {
            _answerVotesRepository = answerVotesRepository;
        }

        [Route("api/posts/{PostId}/answers/{AnswerId}/votes")]
        [HttpGet]
        public async Task<IEnumerable<Models.Vote>> GetVotes()
        {
            return await _answerVotesRepository.GetAllAsyn();
        }

        [Route("api/posts/{PostId}/answers/{AnswerId}/votes/{VoteId}")]
        [HttpGet]
        public async Task<Models.Vote> GetSinglePost(int voteId)
        {
            return await _answerVotesRepository.GetAsync(voteId);
        }

        [Route("api/posts/{PostId}/answers/{AnswerId}/votes")]
        [HttpPost]
        public async Task<Models.Vote> AddVote([FromBody] Models.Vote vote)
        {
            await _answerVotesRepository.AddAsyn(vote);
            await _answerVotesRepository.SaveAsync();
            return vote;
        }

        [Route("api/posts/{PostId}/answers/{AnswerId}/votes/{VoteId}")]
        [HttpPut]
        public async Task<Models.Vote> ReplaceVote([FromBody] Models.Vote vote)
        {
            var updated = await _answerVotesRepository.UpdateAsyn(vote, vote.VoteId);
            return updated;
        }

        [Route("api/posts/{PostId}/answers/{AnswerId}/votes/{VoteId}")]
        [HttpPatch]
        public async Task<Models.Vote> UpdatePost([FromBody] Models.Vote vote)
        {
            var updated = await _answerVotesRepository.UpdateAsyn(vote, vote.VoteId);
            return updated;
        }

        [Route("api/posts/{PostId}/answers/{AnswerId}/votes/{VoteId}")]
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
