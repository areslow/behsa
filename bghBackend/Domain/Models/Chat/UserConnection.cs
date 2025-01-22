using bghBackend.Application.Common.DTOs;

namespace bghBackend.Domain.Models.Chat
{
    public class UserConnection
    {
        public UserDTO User { get; set; }
        public string groupName { get; set; }
    }
}
