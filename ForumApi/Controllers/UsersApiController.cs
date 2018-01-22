using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    //[Route("api/user")]
    public class UsersApiController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersApiController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        [Route("~/api/posts/{PostId}/users")]
        [HttpGet]
        public async Task<IEnumerable<Models.User>> GetUsers()
        {
            return await _userRepository.GetAllAsyn();
        }

        [Route("~/api/posts/{PostId}/users/{UserId}")]
        [HttpGet]
        public async Task<Models.User> GetSingleUser(int userId)
        {
            return await _userRepository.GetAsync(userId);
        }

        [Route("~/api/posts/{PostId}/users")]
        [HttpPost]
        public async Task<Models.User> AddUser([FromBody] Models.User user)
        {
            await _userRepository.AddAsyn(user);
            await _userRepository.SaveAsync();
            return user;
        }

        [Route("~/api/posts/{PostId}/users/{UserId}")]
        [HttpPut]
        public async Task<Models.User> ReplaceUser([FromBody] Models.User user)
        {
            var updated = await _userRepository.UpdateAsyn(user, user.UserId);
            return updated;
        }

        [Route("~/api/posts/{PostId}/users/{UserId}")]
        [HttpPatch]
        public async Task<Models.User> UpdateUser([FromBody] Models.User user)
        {
            var updated = await _userRepository.UpdateAsyn(user, user.UserId);
            return updated;
        }

        [Route("~/api/posts/{PostId}/users/{UserId}")]
        [HttpDelete]
        public string Delete(int id)
        {
            _userRepository.Delete(_userRepository.Get(id));
            return "User deleted successfully!";
        }



        protected override void Dispose(bool disposing)
        {
            _userRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}

