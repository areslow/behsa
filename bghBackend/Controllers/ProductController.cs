using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.Products;
using bghBackend.Infra.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bghBackend.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductCommands _productCommands;
        private readonly IProductQueries _productQueries;
        public ProductController(IProductCommands productCommands, IProductQueries productQueries)
        {
            _productQueries = productQueries;
            _productCommands = productCommands;
        }

        /// <summary>
        /// return all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllProducts()
        {
            return await _productQueries.GetAllProduct();
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ApiResponse>> GetProductById(string productId)
        {
            return await _productQueries.GetProductById(productId);
        }

        [HttpGet("{catgId:long}")]
        public async Task<ActionResult<ApiResponse>> GetProductsByCategory(long catgId)
        {
            return await _productQueries.GetProductsByCategory(catgId);
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateProduct([FromForm]ProductDTO product)
        {
            return await _productCommands.CreateProduct(product);
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpPut]
        public async Task<ActionResult<ApiResponse>> UpdateProduct([FromForm] ProductDTO product)
        {
            return await _productCommands.UpdateProduct(product);
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpDelete("{productId}")]
        public async Task<ActionResult<ApiResponse>> DeleteProduct(string productId)
        {
            return await _productCommands.DeleteProduct(productId);
        }
    }
}
