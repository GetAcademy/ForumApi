using System.Linq;
using System.Threading.Tasks;
using ForumApi.Models;

namespace ForumApi.DataAccess
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }

        public Category Get(int categoryId)
        {
            var query = GetAll().FirstOrDefault(p => p.CategoryId == categoryId);
            return query;
        }

        public async Task<Category> GetSingleAsyn(int categoryId)
        {
            return await _context.Set<Category>().FindAsync(categoryId);
        }

        public override Category Update(Category t, object key)
        {
            Category exist = _context.Set<Category>().Find(key);
            if (exist != null)
            {
            }
            return base.Update(t, key);
        }

        public async override Task<Category> UpdateAsyn(Category t, object key)
        {
            Category exist = await _context.Set<Category>().FindAsync(key);
            if (exist != null)
            {
            }
            return await base.UpdateAsyn(t, key);
        }
    }
}
