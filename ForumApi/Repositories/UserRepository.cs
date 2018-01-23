using System.Linq;
using System.Threading.Tasks;
using ForumApi.Models;
using ForumApi.Interfaces;

namespace ForumApi.Repositories
{
        public class UserRepository : GenericRepository<User>, IUserRepository
        {
            public UserRepository(DataContext context) : base(context)
            {
            }

            public User Get(int userId)
            {
                var query = GetAll().FirstOrDefault(b => b.UserId == userId);
                return query;
            }

            public async Task<User> GetSingleAsyn(int userId)
            {
                return await _context.Set<User>().FindAsync(userId);
            }

            public override User Update(User t, object key)
            {
                User exist = _context.Set<User>().Find(key);
                if (exist != null)
                {
                }
                return base.Update(t, key);
            }

            public async override Task<User> UpdateAsyn(User t, object key)
            {
                User exist = await _context.Set<User>().FindAsync(key);
                if (exist != null)
                {
                }
                return await base.UpdateAsyn(t, key);
            }
        }
    }
