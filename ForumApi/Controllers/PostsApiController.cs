using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    [Route("api/categories/{CategoryId}/posts")]
    public class PostsApiController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostsApiController(IPostRepository postRepository)
        {
            _postRepository = postRepository;

        }

        [HttpGet]
        public async Task<IEnumerable<Models.Post>> GetPosts(int categoryId)
        {
            return await _postRepository.GetAllAsync(categoryId);
        }

        [Route("{PostId}")]
        [HttpGet]
        public async Task<Models.Post> GetSinglePost(int postId)
        {
            return await _postRepository.GetAsync(postId);
        }

        [HttpPost]
        public async Task<Models.Post> AddPost(int categoryId, [FromBody] Models.Post post)
        {
            post.PostParent = categoryId;
            await _postRepository.AddAsyn(post);
            await _postRepository.SaveAsync();
            return post;
        }

        [Route("{PostId}")]
        [HttpPut]
        public async Task<Models.Post> ReplacePost([FromBody] Models.Post post)
        {
            var updated = await _postRepository.UpdateAsyn(post, post.PostId);
            return updated;
        }

        [Route("{PostId}")]
        [HttpPatch]
        public async Task<Models.Post> UpdatePost([FromBody] Models.Post post)
        {
            var updated = await _postRepository.UpdateAsyn(post, post.PostId);
            return updated;
        }

        [Route("{PostId}")]
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
