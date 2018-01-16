using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ForumApi.DataAccess;

namespace ForumApi.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _postRepository.GetAllAsyn());
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var postDetail = _postRepository.Find(b => b.PostId == id);
            return View(postDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostTitle,PostContent")]Models.Post post)
        {
            if (ModelState.IsValid)
            {
                await _postRepository.AddAsyn(post);
                await _postRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Models.Post post = _postRepository.Get((int)id);
            return View(post);

        }

        [HttpPost]
        public ActionResult Edit([Bind("PostId,CreatedBy,CreatedOn,PostTitle,PostContent,UpdatedBy,UpdatedOn")]Models.Post post)
        {
            if (ModelState.IsValid)
            {
                _postRepository.Update(post, post.PostId);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Models.Post post = _postRepository.Get((int)id);
            if (post == null)
            {
                return RedirectToAction("Index");
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Models.Post post = _postRepository.Get(id);
            _postRepository.Delete(post);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _postRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
