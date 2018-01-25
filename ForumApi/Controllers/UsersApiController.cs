using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UsersApiController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersApiController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        [HttpGet]
        public async Task<IEnumerable<Models.User>> GetUsers()
        {
            return await _userRepository.GetAllAsyn();
        }

        [Route("{UserId}")]
        [HttpGet]
        public async Task<Models.User> GetSingleUser(int userId)
        {
            return await _userRepository.GetAsync(userId);
        }

        [HttpPost]
        public async Task<Models.User> AddUser([FromBody] Models.User user)
        {
            await _userRepository.AddAsyn(user);
            await _userRepository.SaveAsync();
            return user;
        }

        [Route("{UserId}")]
        [HttpPut]
        public async Task<Models.User> ReplaceUser([FromBody] Models.User user)
        {
            var updated = await _userRepository.UpdateAsyn(user, user.UserId);
            return updated;
        }

        [Route("{UserId}")]
        [HttpPatch]
        public async Task<Models.User> UpdateUser([FromBody] Models.User user)
        {
            var updated = await _userRepository.UpdateAsyn(user, user.UserId);
            return updated;
        }

        [Route("{UserId}")]
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

