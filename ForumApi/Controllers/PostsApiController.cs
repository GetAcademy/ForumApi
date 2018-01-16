using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Post")]
    public class PostsApiController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostsApiController(IPostRepository postRepository)
        {
            _postRepository = postRepository;

        }

        [Route("~/api/GetPosts")]
        [HttpGet]
        public async Task<IEnumerable<Models.Post>> Index()
        {
            return await _postRepository.GetAllAsyn();
        }

        [Route("~/api/AddPost")]
        [HttpPost]
        public async Task<Models.Post> AddPost([FromBody] Models.Post post)
        {
            await _postRepository.AddAsyn(post);
            await _postRepository.SaveAsync();
            return post;
        }

        [Route("~/api/UpdatePost")]
        [HttpPut]
        //public Post UpdatePost([FromBody] Post post)
        //{
        //  var updated = _postRepository.Update(post, post.PostId);
        //  return updated;
        //}
        public async Task<Models.Post> UpdatePost([FromBody] Models.Post post)
        {
            var updated = await _postRepository.UpdateAsyn(post, post.PostId);
            return updated;
        }

        [Route("~/api/DeletePost/{id}")]
        [HttpDelete]
        public string Delete(int id)
        {
            _postRepository.Delete(_postRepository.Get(id));
            return "Post deleted successfully!";
        }

        protected override void Dispose(bool disposing)
        {
            _postRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
