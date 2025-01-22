namespace bghBackend.Application.Common.DTOs
{
    public class CategoryDTO
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string? ImagePath { get; set; }
        public bool IsDeleted { get; set; }
        public List<ProductDTO>? Products { get; set; }
    }
}
