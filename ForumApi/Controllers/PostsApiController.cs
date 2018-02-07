using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using ForumApi.Models;
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
        public async Task<IEnumerable<Post>> GetPosts([FromRoute] int categoryId)
        {
            return await _postRepository.GetAllAsync(categoryId);
        }

        [Route("{PostId}")]
        [HttpGet]
        public async Task<Post> GetSinglePost(int categoryId, int postId)
        {
            return await _postRepository.GetSingleAsyn(categoryId, postId);
        }


        [HttpPost]
        public async Task<Post> AddPost(int categoryId, [FromBody] Post post)
        {
            post.CategoryId = categoryId;
            await _postRepository.AddAsyn(post);
            await _postRepository.SaveAsync();
            return post;
        }

        [Route("{PostId}")]
        [HttpPut]
        public async Task<Post> ReplacePost(int categoryId, int postId, [FromBody] Post post)
        {
            post.CategoryId = categoryId;
            post.PostId = postId;
            var updated = await _postRepository.UpdateAsyn(post);
            return updated;
        }

        [Route("{PostId}")]
        [HttpPatch]
        public async Task<Post> UpdatePost(int categoryId, [FromBody] Post post)
        {
            var updated = await _postRepository.UpdateAsyn(post);
            return updated;
        }

        [Route("{PostId}")]
        [HttpDelete]
        public string Delete(int categoryId, int postId, [FromBody] Post post)
        {
            post.CategoryId = categoryId;
            post.PostId = postId;
            _postRepository.Delete(post);
            return "Post deleted successfully!";
        }

        protected override void Dispose(bool disposing)
        {
            _postRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
