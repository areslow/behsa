using System.ComponentModel.DataAnnotations.Schema;

namespace bghBackend.Domain.Entities
{
    public class Post
    {
        public long Id { get; set; }
        public string DateCreated { get; set; }
        public string Title { get; set; }
        public string? Abstract { get; set; }// in case the author wants to make a brief introduction
        public string Content { get; set; }
        public string? References { get; set; }
        
        public string? AuthorId { get; set; }
        public bool IsOpen { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("AuthorId")]
        public AppUser? Author { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
