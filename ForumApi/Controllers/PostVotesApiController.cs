using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    //[Route("api/post/{PostId}/votes")]
    public class PostVotesApiController : Controller
    {
        private readonly IVoteRepository _postVotesRepository;

        public PostVotesApiController(IVoteRepository postVotesRepository)
        {
            _postVotesRepository = postVotesRepository;
        }

        [Route("~/api/posts/{PostId}/votes")]
        [HttpGet]
        public async Task<IEnumerable<Models.Vote>> GetVotes()
        {
            return await _postVotesRepository.GetAllAsyn();
        }

        [Route("~/api/posts/{PostId}/votes/{VoteId}")]
        [HttpGet]
        public async Task<Models.Vote> GetSinglePost(int voteId)
        {
            return await _postVotesRepository.GetAsync(voteId);
        }

        [Route("~/api/posts/{PostId}/votes/")]
        [HttpPost]
        public async Task<Models.Vote> AddVote([FromBody] Models.Vote vote)
        {
            await _postVotesRepository.AddAsyn(vote);
            await _postVotesRepository.SaveAsync();
            return vote;
        }

        [Route("~/api/posts/{PostId}/votes/{VoteId}")]
        [HttpPut]
        public async Task<Models.Vote> ReplaceVote([FromBody] Models.Vote vote)
        {
            var updated = await _postVotesRepository.UpdateAsyn(vote, vote.VoteId);
            return updated;
        }

        [Route("~/api/posts/{PostId}/votes/{VoteId}")]
        [HttpPatch]
        public async Task<Models.Vote> UpdatePost([FromBody] Models.Vote vote)
        {
            var updated = await _postVotesRepository.UpdateAsyn(vote, vote.VoteId);
            return updated;
        }

        [Route("~/api/posts/{PostId}/votes/{VoteId}")]
        [HttpDelete]
        public string Delete(int id)
        {
            _postVotesRepository.Delete(_postVotesRepository.Get(id));
            return "Vote deleted successfully!";
        }

        protected override void Dispose(bool disposing)
        {
            _postVotesRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
