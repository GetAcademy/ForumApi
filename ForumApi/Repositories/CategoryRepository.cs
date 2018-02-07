using System.Linq;
using System.Threading.Tasks;
using ForumApi.Models;
using ForumApi.Interfaces;

namespace ForumApi.Repositories
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
        
        protected override async Task<Category> Find(Category t)
        {
            return await GetSingleAsyn(t.CategoryId);
        }

    }
}
