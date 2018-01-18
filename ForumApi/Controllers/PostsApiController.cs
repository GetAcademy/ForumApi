using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    //[Route("api/post")]
    public class PostsApiController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostsApiController(IPostRepository postRepository)
        {
            _postRepository = postRepository;

        }

        [Route("~/api/posts")]
        [HttpGet]
        public async Task<IEnumerable<Models.Post>> GetPosts()
        {
            return await _postRepository.GetAllAsyn();
        }

        [Route("~/api/posts/{PostId}")]
        [HttpGet]
        public async Task<Models.Post> GetSinglePost(int postId)
        {
            return await _postRepository.GetAsync(postId);
        }

        [Route("~/api/Posts")]
        [HttpPost]
        public async Task<Models.Post> AddPost([FromBody] Models.Post post)
        {
            await _postRepository.AddAsyn(post);
            await _postRepository.SaveAsync();
            return post;
        }

        [Route("~/api/posts/{PostId}")]
        [HttpPut]
        public async Task<Models.Post> ReplacePost([FromBody] Models.Post post)
        {
            var updated = await _postRepository.UpdateAsyn(post, post.PostId);
            return updated;
        }

        [Route("~/api/posts/{PostId}")]
        [HttpPatch]
        public async Task<Models.Post> UpdatePost([FromBody] Models.Post post)
        {
            var updated = await _postRepository.UpdateAsyn(post, post.PostId);
            return updated;
        }

        [Route("~/api/posts/{PostId}")]
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
