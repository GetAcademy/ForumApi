using System.Collections.Generic;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Produces("application/json")]
    [Route("api/categories")]
    public class CategoriesApiController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesApiController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }

        [HttpGet]
        public async Task<IEnumerable<Models.Category>> GetCategories()
        {
            return await _categoryRepository.GetAllAsyn();
        }

        [Route("{CategoryId}")]
        [HttpGet]
        public async Task<Models.Category> GetSingleCategory(int categoryId)
        {
            return await _categoryRepository.GetAsync(categoryId);
        }

        [HttpPost]
        public async Task<Models.Category> AddCategory([FromBody] Models.Category category)
        {
            await _categoryRepository.AddAsyn(category);
            await _categoryRepository.SaveAsync();
            return category;
        }

        [Route("{CategoryId}")]
        [HttpPut]
        public async Task<Models.Category> ReplaceCategory(int categoryId, [FromBody] Models.Category category)
        {
            category.CategoryId = categoryId;
            var updated = await _categoryRepository.UpdateAsyn(category);
            return updated;
        }

        [Route("{CategoryId}")]
        [HttpPatch]
        public async Task<Models.Category> UpdateCategory([FromBody] Models.Category category)
        {
            var updated = await _categoryRepository.UpdateAsyn(category);
            return updated;
        }

        [Route("{CategoryId}")]
        [HttpDelete]
        public string Delete(int categoryId, [FromBody] Category category)
        {
            category.CategoryId = categoryId;
            _categoryRepository.Delete(category);
            return "Category deleted successfully!";
        }

        protected override void Dispose(bool disposing)
        {
            _categoryRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}

