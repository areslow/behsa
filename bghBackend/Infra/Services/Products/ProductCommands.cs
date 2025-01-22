using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.Products;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;

namespace bghBackend.Infra.Services.Products
{
    public class ProductCommands : IProductCommands
    {
        private readonly ApplicationDbContext _db;

        public ProductCommands(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse> CreateProduct(ProductDTO product)
        {
            try
            {
                string imageName="";
                string imagePath;
                if (product.Image == null || product.Image.Length == 0)
                {
                    imagePath = "";//path to demo image
                }
                else
                {
                    imageName = $"{Guid.NewGuid()}{Path.GetExtension(product.Image.FileName)}";
                    imagePath = Path.Combine(SD.ProductImageDirectory, imageName);
                    using Stream fs = new FileStream(imagePath, FileMode.Create);
                        await product.Image.CopyToAsync(fs);
                }
                Product productToCreate = new()
                {
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    ImageName = imageName,
                    Name = product.Name,
                    IsDeleted = false,
                };
                await _db.Products.AddAsync(productToCreate);
                int res = await _db.SaveChangesAsync();
                if (res == 0) return ApiResponse.FailureResponse(SD.CREATION_FAILED);
                
                    return ApiResponse.SuccessResponse(SD.CREATION_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message + " " + ex.InnerException ?? "");
            }
        }

        public async Task<ApiResponse> DeleteProduct(string productId)
        {
            try
            {
                Product? productToDelete = await _db.Products.FindAsync(productId);
                if (productToDelete == null) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                productToDelete.IsDeleted = true;
                int res = await _db.SaveChangesAsync();
                if(res == 0) return ApiResponse.FailureResponse(SD.ACTION_FAILED);
                return ApiResponse.SuccessResponse(SD.ITEM_REMOVED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message + " " + ex.InnerException ?? "");
            }
        }

        public async Task<ApiResponse> UpdateProduct(ProductDTO product)
        {
            try
            {
                
                Product? productFromDb = await _db.Products.FindAsync(product.Id);
                if (productFromDb == null || productFromDb.IsDeleted == true) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);

                string? imageName = productFromDb.ImageName;
                string imagePath;
                
                if(product.Image != null && product.Image.Length != 0)
                {
                    if (File.Exists(Path.Combine(SD.ProductImageDirectory, imageName)))
                        File.Delete(Path.Combine(SD.ProductImageDirectory, imageName));
                    imageName = $"{Path.GetFileNameWithoutExtension(imageName)}{Path.GetExtension(product.Image.FileName)}";
                    productFromDb.ImageName = imageName;
                    imagePath = Path.Combine(SD.ProductImageDirectory, imageName);
                    using Stream fs = new FileStream(imagePath, FileMode.Create);
                        await product.Image.CopyToAsync(fs);
                }
                
                productFromDb.Name = product.Name;
                productFromDb.Description = product.Description;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.Price = product.Price;
                int res = await _db.SaveChangesAsync();
                if (res == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_UPDATED);
            }
            catch (Exception ex)
            {
                return ApiResponse.SuccessResponse(ex.Message + " " + ex.InnerException ?? "");
            }
        }
    }
}
