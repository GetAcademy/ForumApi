using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    public class AnswerApiController
    {
        [Produces("application/json")]
        [Route("api/Answer")]
        public class AnswersApiController : Controller
        {
            private readonly IAnswerRepository _answerRepository;

            public AnswersApiController(IAnswerRepository answerRepository)
            {
                _answerRepository = answerRepository;

            }

            [Route("~/api/GetAnswers")]
            [HttpGet]
            public async Task<IEnumerable<Models.Answer>> Index()
            {
                return await _answerRepository.GetAllAsyn();
            }

            [Route("~/api/AddAnswer")]
            [HttpPost]
            public async Task<Models.Answer> AddAnswer([FromBody] Models.Answer answer)
            {
                await _answerRepository.AddAsyn(answer);
                await _answerRepository.SaveAsync();
                return answer;
            }

            [Route("~/api/UpdateAnswer")]
            [HttpPut]
            //public Answer UpdateAnswer([FromBody] Answer answer)
            //{
            //  var updated = _answerRepository.Update(answer, answer.AnswerId);
            //  return updated;
            //}
            public async Task<Models.Answer> UpdateAnswer([FromBody] Models.Answer answer)
            {
                var updated = await _answerRepository.UpdateAsyn(answer, answer.AnswerId);
                return updated;
            }

            [Route("~/api/DeleteAnswer/{id}")]
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
}

