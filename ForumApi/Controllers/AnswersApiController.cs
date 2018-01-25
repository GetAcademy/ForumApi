using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public Task<IEnumerable<Models.Answer>> GetAnswers(int categoryId, int postId)
        {
            return _answerRepository.GetAllAsync(postId, categoryId);
        }

        [Route("{AnswerId}")]
        [HttpGet]
        public async Task<Models.Answer> GetSingleAnswer(int categoryId, int postId, int answerId)
        {
            return await _answerRepository.GetSingleAsyn(answerId, postId, categoryId);
        }
        
        [HttpPost]
        public async Task<Models.Answer> AddAnswer(int postId, int categoryId, [FromBody] Models.Answer answer)
        {
            answer.PostId = postId;
            answer.CategoryId = categoryId;
            await _answerRepository.AddAsyn(answer);
            await _answerRepository.SaveAsync();
            return answer;
        }

        [Route("{AnswerId}")]
        [HttpPut]
        public async Task<Models.Answer> ReplaceAnswer([FromBody] Models.Answer answer)
        {
            var updated = await _answerRepository.UpdateAsyn(answer, answer.AnswerId);
            return updated;
        }


        [Route("{AnswerId}")]
        [HttpPatch]
        public async Task<Models.Answer> UpdateAnswer([FromBody] Models.Answer answer)
        {
            var updated = await _answerRepository.UpdateAsyn(answer, answer.AnswerId);
            return updated;
        }

        [Route("{AnswerId}")]
        [HttpDelete]
        public string Delete(int id)
        {
            _answerRepository.Delete(_answerRepository.Get(id));
            return "Answer deleted successfully!";
        }

        protected override void Dispose(bool disposing)
        {
            _answerRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}

