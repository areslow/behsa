using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bghBackend.Application.Common.DTOs
{
    public class ReplyDTO
    {
        public long Id { get; set; }
        public string? DateCreated { get; set; }
        
        public string? Content { get; set; }
        public string? AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public long CommentId { get; set; }
    }
}
