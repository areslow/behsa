using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bghBackend.Domain.Entities
{
    public class Reply
    {
        public long Id { get; set; }
        public string DateCreated { get; set; }

        [Required]
        [MinLength(1)]
        public string Content { get; set; }
        public string? AuthorId { get; set; }
        public long CommentId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("AuthorId")]
        public AppUser? Author { get; set; }
        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }
    }
}
