using System.Linq;
using System.Threading.Tasks;
using ForumApi.Models;
using ForumApi.Interfaces;
using System.Collections.Generic;

namespace ForumApi.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository   
    {
        public PostRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Post>> GetAllAsync(int categoryId)
        {
            return (await GetAllAsyn()).Where(p => p.PostParent == categoryId).ToList();
        }

        public async Task<Post> GetSingleAsyn(int postId, int postParent)
        {
            return await _context.Set<Post>().FindAsync(postId);
        }

        public override Post Update(Post t, object key)
        {
            Post exist = _context.Set<Post>().Find(key);
            if (exist != null)
            {
                t.CreatedBy = exist.CreatedBy;
                t.CreatedOn = exist.CreatedOn;
            }
            return base.Update(t, key);
        }

        public async override Task<Post> UpdateAsyn(Post t, object key)
        {
            Post exist = await _context.Set<Post>().FindAsync(key);
            if (exist != null)
            {
                t.CreatedBy = exist.CreatedBy;
                t.CreatedOn = exist.CreatedOn;
            }
            return await base.UpdateAsyn(t, key);
        }
    }
}
