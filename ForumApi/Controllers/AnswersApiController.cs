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
        public Task<IEnumerable<Answer>> GetAnswers([FromRoute] int categoryId, int postId)
        {
            return _answerRepository.GetAllAsync(categoryId, postId);
        }

        [Route("{AnswerId}")]
        [HttpGet]
        public async Task<Answer> GetSingleAnswer([FromRoute] int categoryId, int postId, int answerId)
        {
            return await _answerRepository.GetSingleAsyn(categoryId, postId, answerId);
        }
        
        [HttpPost]
        public async Task<Answer> AddAnswer(int categoryId, int postId, [FromBody] Answer answer)
        {
            answer.CategoryId = categoryId;
            answer.PostId = postId;
            await _answerRepository.AddAsyn(answer);
            await _answerRepository.SaveAsync();
            return answer;
        }

        [Route("{AnswerId}")]
        [HttpPut]
        public async Task<Answer> ReplaceAnswer(int categoryId, int postId, int answerId, [FromBody] Answer answer)
        {
            answer.CategoryId = categoryId;
            answer.PostId = postId;
            answer.AnswerId = answerId;
            
            var updated = await _answerRepository.UpdateAsyn(answer);
            return updated;
        }

        //[Route("{AnswerId}")]
        //[HttpPatch]
        //public async Task<Answer> UpdateAnswer([FromBody] Answer answer)
        //{
        //    var updated = await _answerRepository.UpdateAsyn(answer);
        //    return updated;
        //}

        [Route("{AnswerId}")]
        [HttpDelete]
        public string Delete(int categoryId, int postId, int answerId, [FromBody] Answer answer)
        {
            answer.CategoryId = categoryId;
            answer.PostId = postId;
            answer.AnswerId = answerId;
            _answerRepository.Delete(answer);
            return "Answer deleted successfully!";
        }

        protected override void Dispose(bool disposing)
        {
            _answerRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}

