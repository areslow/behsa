using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace bghBackend.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        //[MaxLength(20,ErrorMessage ="Name must have less than 20 char length")]
        public string? FirstName { get; set; }
        //[MaxLength(40,ErrorMessage ="LastName must have less than 40 char length")]
        public string? LastName { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Reply>? Replies { get; set; }
        public ICollection<Product>? RegisteredCourse { get; set; }
    }
}
