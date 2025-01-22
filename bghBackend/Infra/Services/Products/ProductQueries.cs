using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.Products;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;
using Microsoft.EntityFrameworkCore;

namespace bghBackend.Infra.Services.Products
{
    public class ProductQueries : IProductQueries
    {
        private readonly ApplicationDbContext _db;

        public ProductQueries(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse> GetAllProduct()
        {
            try
            {
                List<ProductDTO> response = await _db.Products.Include(p => p.Category).Where(p => p.IsDeleted == false).Select(p => new ProductDTO
                {
                    CategoryId = p.CategoryId,
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Id = p.Id,
                    ImageName = p.ImageName,
                }).ToListAsync();
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, response);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message + " " + ex.InnerException ?? "");
            }

        }

        public async Task<ApiResponse> GetProductsByCategory(long catgId)
        {
            try
            {
                List<ProductDTO> response = await _db.Products.Include(p => p.Category).Where(p => p.IsDeleted == false && p.CategoryId == catgId)
                    .Select(p => new ProductDTO
                    {
                        CategoryId = p.CategoryId,
                        Name = p.Name,
                        CategoryName = p.Category.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Id = p.Id,
                        ImageName = p.ImageName,
                    }).ToListAsync();
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, response);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message + " " + ex.InnerException ?? "");
            }
        }

        public async Task<ApiResponse> GetProductById(string productId)
        {
            try
            {
                ProductDTO? response = await _db.Products.Include(p => p.Category).Where(p => p.IsDeleted == false)
                    .Select(p => new ProductDTO
                {
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Id = p.Id,
                        ImageName = p.ImageName,
                    Name = p.Name,
                }).FirstOrDefaultAsync(p => p.Id == productId);

                if (response == null) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, response);

            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message + " " + ex.InnerException ?? "");
            }
        }

        public async Task<ApiResponse> GetProductByName(string productName)
        {
            try
            {
                Product? prFromDb = await _db.Products.Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.Name == productName && p.IsDeleted == false);
                if (prFromDb == null) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                ProductDTO response = new()
                {
                    CategoryId = prFromDb.CategoryId,
                    CategoryName = prFromDb.Category.Name,
                    Description = prFromDb.Description,
                    Price = prFromDb.Price,
                    Id = prFromDb.Id,
                    ImageName = prFromDb.ImageName,
                    Name = prFromDb.Name
                };
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, response);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message + " " + ex.InnerException ?? "");
            }
        }
    }
}
