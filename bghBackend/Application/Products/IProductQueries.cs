using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Products
{
    public interface IProductQueries
    {
        public Task<ApiResponse> GetAllProduct();
        public Task<ApiResponse> GetProductById(string productId);
        public Task<ApiResponse> GetProductByName(string productName);
        public Task<ApiResponse> GetProductsByCategory(long catgId);
    }
}
