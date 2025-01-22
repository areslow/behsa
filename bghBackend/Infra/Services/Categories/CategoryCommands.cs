using bghBackend.Application.Categories;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;
using Microsoft.EntityFrameworkCore;

namespace bghBackend.Infra.Services.Categories
{
    public class CategoryCommands : ICategoryCommands
    {
        private readonly ApplicationDbContext _db;

        public CategoryCommands(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// create new category in database
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<ApiResponse> CreateCategory(CategoryDTO category)
        {
            try
            {
                var catgFromDb = await _db.Categories.FirstOrDefaultAsync(c => c.Name == category.Name && c.IsDeleted == false);
                if (catgFromDb != null) return ApiResponse.FailureResponse("Item already exists");
                Category categoryToCreate = new()
                {
                    ImagePath = category.ImagePath,
                    Name = category.Name,
                    IsDeleted = false,
                };
                await _db.Categories.AddAsync(categoryToCreate);
                int res = await _db.SaveChangesAsync();
                if (res == 0) return ApiResponse.FailureResponse(SD.CREATION_FAILED);
                return ApiResponse.SuccessResponse(SD.CREATION_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                return ApiResponse.SuccessResponse(ex.Message + " " + ex.InnerException ?? "");
            }
        }

        public async Task<ApiResponse> DeleteCategory(long categoryId)
        {
            try
            {
                Category? catgFromDb = await _db.Categories.FindAsync(categoryId);
                if (catgFromDb == null || catgFromDb.IsDeleted == true) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                catgFromDb.IsDeleted = true;
                int res = await _db.SaveChangesAsync();
                if (res == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_REMOVED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message + " " + ex.InnerException ?? "");
            }
        }

        public async Task<ApiResponse> UpdateCategory(CategoryDTO category)
        {
            try
            {
                Category? catgFromDb = await _db.Categories.FindAsync(category.Id);
                if (catgFromDb == null || catgFromDb.IsDeleted == true) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                catgFromDb.ImagePath = category.ImagePath;
                catgFromDb.Name = category.Name;
                int res = await _db.SaveChangesAsync();
                if (res == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_UPDATED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message + " " + ex.InnerException ?? "");
            }
        }
    }
}
