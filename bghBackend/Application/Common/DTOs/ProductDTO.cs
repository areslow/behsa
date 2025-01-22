namespace bghBackend.Application.Common.DTOs
{
    public class ProductDTO
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public float Price { get; set; }
        public string? CategoryName { get; set; }
        //public bool IsDeleted { get; set; }
        public string? ImageName { get; set; }

        public IFormFile? Image {  get; set; }
    }
}
