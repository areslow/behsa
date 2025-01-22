using System.ComponentModel.DataAnnotations.Schema;

namespace bghBackend.Domain.Entities
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public float Price { get; set; }
        //public string status {  get; set; } // the status of the product : active, enrollment started (ended) and ...
        public bool IsDeleted { get; set; }
        public string? ImageName { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<AppUser>? RegisteredUsers { get; set; }

    }
}
