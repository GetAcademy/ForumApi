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
            return (await GetAllAsyn()).Where(p => p.CategoryId == categoryId).ToList();
        }

        public async Task<Post> GetSingleAsyn(int categoryId, int postId)
        {
            return await _context.Set<Post>().FindAsync(postId, categoryId);
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

        //public async Task<Post> UpdateAsync(int categoryId, Post post, int postId)
        //{
        //    var updateThis = _context.Set<Post>().FindAsync(postId, post, categoryId);
        //    //Kode for å oppdatere
        //    return await updateThis;
        //}
        protected override async Task<Post> Find(Post t)
        {
            return await GetSingleAsyn(t.CategoryId, t.PostId);
        }
    }
}
