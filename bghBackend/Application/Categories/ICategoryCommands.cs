using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Categories
{
    public interface ICategoryCommands
    {
        public Task<ApiResponse> CreateCategory(CategoryDTO category);
        public Task<ApiResponse> UpdateCategory(CategoryDTO category);
        public Task<ApiResponse> DeleteCategory(long categoryId);
    }
}
