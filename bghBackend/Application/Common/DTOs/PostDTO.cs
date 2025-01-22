using bghBackend.Infra.Utilities;

namespace bghBackend.Application.Common.DTOs
{
    public class PostDTO
    {
        public long? Id { get; set; }
        public string? DateCreated { get; set; } = PersianDateTime.Now();
        public string Title { get; set; }
        public string? Abstract { get; set; }// if it was an article it should have an abstract
        public string Content { get; set; }
        public string? References { get; set; }
        public string? AuthorId { get; set; }
        public bool IsOpen { get; set; } = true; // determine if it is possible to put comment on this post or not
    }
}
