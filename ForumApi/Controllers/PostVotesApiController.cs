using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ForumApi.Authorization;
using ForumApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        public Task<IEnumerable<Models.Vote>> GetVotes(int postId)
        {
            
            return _postVotesRepository.GetAllPostVotesAsync(postId);
        }

        [Route("{VoteId}")]
        [HttpGet]
        public async Task<Models.Vote> GetSinglePost(int voteId)
        {
            return await _postVotesRepository.GetAsync(voteId);
        }

        public string UserId => ((TfsoIdentity) User.Identity).TfsoUserId;
        [HttpPost]
        public async Task<Models.Vote> AddVote(int postId, [FromBody] Models.Vote vote)
        {
            //vote.UserId = UserId;
            vote.VoteParent = postId;
            await _postVotesRepository.AddAsyn(vote);
            await _postVotesRepository.SaveAsync();
            return vote;
        }

        [Route("{VoteId}")]
        [HttpPut]
        public async Task<Models.Vote> ReplaceVote([FromBody] Models.Vote vote)
        {
            //vote.UserId = UserId;
            var updated = await _postVotesRepository.UpdateAsyn(vote, vote.VoteId);
            return updated;
        }

        [Route("{VoteId}")]
        [HttpPatch]
        public async Task<Models.Vote> UpdatePost([FromBody] Models.Vote vote)
        {
            var updated = await _postVotesRepository.UpdateAsyn(vote, vote.VoteId);
            return updated;
        }

        [Route("{VoteId}")]
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
