using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ForumApi.DataAccess;

namespace ForumApi.Controllers
{
    public class AnswersController : Controller
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswersController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _answerRepository.GetAllAsyn());
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var answerDetail = _answerRepository.Find(b => b.AnswerId == id);
            return View(answerDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerBody,PostId")]Models.Answer answer)
        {
            if (ModelState.IsValid)
            {
                await _answerRepository.AddAsyn(answer);
                await _answerRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(answer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Models.Answer answer = _answerRepository.Get((int)id);
            return View(answer);

        }

        [HttpPost]
        public ActionResult Edit([Bind("AnswerId,CreatedBy,CreatedOn,AnswerBody,UpdatedBy,UpdatedOn")]Models.Answer answer)
        {
            if (ModelState.IsValid)
            {
                _answerRepository.Update(answer, answer.AnswerId);
                return RedirectToAction("Index");
            }
            return View(answer);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Models.Answer answer = _answerRepository.Get((int)id);
            if (answer == null)
            {
                return RedirectToAction("Index");
            }
            return View(answer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Models.Answer answer = _answerRepository.Get(id);
            _answerRepository.Delete(answer);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _answerRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}


