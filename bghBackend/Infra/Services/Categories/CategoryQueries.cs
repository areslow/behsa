using bghBackend.Application.Categories;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;
using Microsoft.EntityFrameworkCore;

namespace bghBackend.Infra.Services.Categories
{
    public class CategoryQueries : ICategoryQueries
    {
        private readonly ApplicationDbContext _db;

        public CategoryQueries(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// get all categories and their corresponding products
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> GetAllCategories()
        {
            try
            {
                List<CategoryDTO> response = await _db.Categories.Include(c => c.Products)
                    .Where(c => c.IsDeleted == false).Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ImagePath = c.ImagePath,
                        Products = c.Products.Select(p => new ProductDTO
                        {
                            Name = p.Name,
                            ImageName = p.ImageName,
                            Description = p.Description,
                            Id = p.Id,
                            CategoryId = c.Id,
                            CategoryName = c.Name,
                        }).ToList(),

                    }).ToListAsync();
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, response);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message + " " + ex.InnerException ?? "");
            }
        }

        public async Task<ApiResponse> GetCategoryById(long catgId)
        {
            try
            {
                    Category? catg = await _db.Categories.FindAsync(catgId);
                if (catg == null || catg.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                List<ProductDTO> products = await _db.Products.Where(p => p.CategoryId == catgId && p.IsDeleted == false).Select(p => new ProductDTO
                {
                    Description = p.Description,
                    Id = p.Id,
                    ImageName = p.ImageName,
                    Name = p.Name,
                    CategoryId = catgId,
                    CategoryName = catg.Name,
                }).ToListAsync();
                CategoryDTO response = new()
                {
                    Id = catgId,
                    Name = catg.Name,
                    ImagePath=catg.ImagePath,
                    Products = products
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
