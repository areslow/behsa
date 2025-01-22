using System.ComponentModel.DataAnnotations;

namespace bghBackend.Application.Common.DTOs
{
    public class RegisterRequestDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
