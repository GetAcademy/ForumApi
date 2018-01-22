using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    //[Route("api/category")]
    public class CategoriesApiController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesApiController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }

        [Route("~/api/categories")]
        [HttpGet]
        public async Task<IEnumerable<Models.Category>> GetCategories()
        {
            return await _categoryRepository.GetAllAsyn();
        }

        [Route("~/api/categories/{CategoryId}")]
        [HttpGet]
        public async Task<Models.Category> GetSingleCategory(int categoryId)
        {
            return await _categoryRepository.GetAsync(categoryId);
        }

        [Route("~/api/categories")]
        [HttpPost]
        public async Task<Models.Category> AddCategory([FromBody] Models.Category category)
        {
            await _categoryRepository.AddAsyn(category);
            await _categoryRepository.SaveAsync();
            return category;
        }

        [Route("~/api/categories/{CategoryId}")]
        [HttpPut]
        public async Task<Models.Category> ReplaceCategory([FromBody] Models.Category category)
        {
            var updated = await _categoryRepository.UpdateAsyn(category, category.CategoryId);
            return updated;
        }

        [Route("~/api/categories/{CategoryId}")]
        [HttpPatch]
        public async Task<Models.Category> UpdateCategory([FromBody] Models.Category category)
        {
            var updated = await _categoryRepository.UpdateAsyn(category, category.CategoryId);
            return updated;
        }

        [Route("~/api/categories/{CategoryId}")]
        [HttpDelete]
        public string Delete(int id)
        {
            _categoryRepository.Delete(_categoryRepository.Get(id));
            return "Category deleted successfully!";
        }



        protected override void Dispose(bool disposing)
        {
            _categoryRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}

