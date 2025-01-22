using bghBackend.Application.Categories;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Infra.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bghBackend.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryCommands _categoryCommands;
        private readonly ICategoryQueries _categoryQueries;

        public CategoryController(ICategoryQueries categoryQueries, ICategoryCommands categoryCommands)
        {
            _categoryQueries = categoryQueries;
            _categoryCommands = categoryCommands;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllCategories()
        {
            return await _categoryQueries.GetAllCategories();
        }

        [HttpGet("{catgId:long}")]
        public async Task<ActionResult<ApiResponse>> GetCategoryById(long catgId)
        {
            return await _categoryQueries.GetCategoryById(catgId);
        }


        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateCategory([FromBody]CategoryDTO category)
        {
            return await _categoryCommands.CreateCategory(category);
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpPut]
        public async Task<ActionResult<ApiResponse>> UpdateCategory([FromBody]CategoryDTO category)
        {
            return await _categoryCommands.UpdateCategory(category);
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpDelete("{categoryId:long}")]
        public async Task<ActionResult<ApiResponse>> DeleteCategory(long categoryId)
        {
            return await _categoryCommands.DeleteCategory(categoryId);
        }

    }
}
