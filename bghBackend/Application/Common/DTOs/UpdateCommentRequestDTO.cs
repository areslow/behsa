namespace bghBackend.Application.Common.DTOs
{
    public class UpdateCommentRequestDTO
    {
        public string NewComment {  get; set; }
        public UserDTO AppUser { get; set; }
    }
}
