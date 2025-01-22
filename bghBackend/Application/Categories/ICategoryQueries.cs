using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Categories
{
    public interface ICategoryQueries
    {
        public Task<ApiResponse> GetAllCategories();
        public Task<ApiResponse> GetCategoryById(long catgId);
    }
}
