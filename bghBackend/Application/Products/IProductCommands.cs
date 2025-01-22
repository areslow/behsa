using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Products
{
    public interface IProductCommands
    {
        public Task<ApiResponse> CreateProduct(ProductDTO product);
        public Task<ApiResponse> UpdateProduct(ProductDTO product);
        public Task<ApiResponse> DeleteProduct(string productId);
    }
}
