using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    //[Route("api/answer")]
    public class AnswersApiController : Controller
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswersApiController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;

        }

        [Route("~/api/posts/{PostId}/answers")]
        [HttpGet]
        public async Task<IEnumerable<Models.Answer>> GetAnswers()
        {
            return await _answerRepository.GetAllAsyn();
        }

        [Route("~/api/posts/{PostId}/answers/{AnswerId}")]
        [HttpGet]
        public async Task<Models.Answer> GetSingleAnswer(int answerId)
        {
            return await _answerRepository.GetAsync(answerId);
        }

        [Route("~/api/posts/{PostId}/answers")]
        [HttpPost]
        public async Task<Models.Answer> AddAnswer([FromBody] Models.Answer answer)
        {
            await _answerRepository.AddAsyn(answer);
            await _answerRepository.SaveAsync();
            return answer;
        }

        [Route("~/api/posts/{PostId}/answers/{AnswerId}")]
        [HttpPut]
        public async Task<Models.Answer> ReplaceAnswer([FromBody] Models.Answer answer)
        {
            var updated = await _answerRepository.UpdateAsyn(answer, answer.AnswerId);
            return updated;
        }

        [Route("~/api/posts/{PostId}/answers/{AnswerId}")]
        [HttpPatch]
        public async Task<Models.Answer> UpdateAnswer([FromBody] Models.Answer answer)
        {
            var updated = await _answerRepository.UpdateAsyn(answer, answer.AnswerId);
            return updated;
        }

        [Route("~/api/posts/{PostId}/answers/{AnswerId}")]
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

