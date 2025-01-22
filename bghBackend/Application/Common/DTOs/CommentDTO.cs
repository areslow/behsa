namespace bghBackend.Application.Common.DTOs
{
    public class CommentDTO
    {
        public long Id { get; set; }
        public string? DateCreated { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public long PostId { get; set; }
        public bool IsOpen { get; set; }
        public ICollection<ReplyDTO>? Replies { get; set; }
    }
}
