using System.ComponentModel.DataAnnotations.Schema;

namespace bghBackend.Domain.Entities
{
    public class Comment
    {
        public long Id { get; set; }
        public string DateCreated { get; set; }
        public string Content { get; set; }
        public string? AuthorId {  get; set; }
        public long PostId { get; set; }
        public bool IsOpen { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        [ForeignKey("AuthorId")]
        public AppUser? Author { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        public ICollection<Reply>? Replies { get; set; }
    }
}
