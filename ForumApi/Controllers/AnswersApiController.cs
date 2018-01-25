using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ForumApi.Models;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    [Route("api/categories/{CategoryId}/posts/{postId}/answers")]
    public class AnswersApiController : Controller
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswersApiController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }
        
        [HttpGet]
        public Task<IEnumerable<Answer>> GetAnswers(int categoryId, int postId)
        {
            return _answerRepository.GetAllAsync(postId, categoryId);
        }

        [Route("{AnswerId}")]
        [HttpGet]
        public async Task<Answer> GetSingleAnswer(int categoryId, int postId, int answerId)
        {
            return await _answerRepository.GetSingleAsyn(answerId, postId, categoryId);
        }
        
        [HttpPost]
        public async Task<Answer> AddAnswer(int postId, int categoryId, [FromBody] Answer answer)
        {
            answer.PostId = postId;
            answer.CategoryId = categoryId;
            await _answerRepository.AddAsyn(answer);
            await _answerRepository.SaveAsync();
            return answer;
        }

        [Route("{AnswerId}")]
        [HttpPut]
        public async Task<Answer> ReplaceAnswer([FromBody] Answer answer)
        {
            var updated = await _answerRepository.UpdateAsyn(answer, answer.AnswerId);
            return updated;
        }


        [Route("{AnswerId}")]
        [HttpPatch]
        public async Task<Answer> UpdateAnswer([FromBody] Answer answer)
        {
            var updated = await _answerRepository.UpdateAsyn(answer, answer.AnswerId);
            return updated;
        }

        [Route("{AnswerId}")]
        [HttpDelete]
        public string Delete(int categoryId, int postId, int answerId, [FromBody] Answer answer)
        {
            answer.CategoryId = categoryId;
            answer.PostId = postId;
            answer.AnswerId = answerId;
            _answerRepository.Delete(answer);
            return "Vote deleted successfully!";
        }

        protected override void Dispose(bool disposing)
        {
            _answerRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}

