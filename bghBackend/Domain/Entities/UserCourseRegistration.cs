using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bghBackend.Domain.Entities
{
    public class UserCourseRegistration
    {
        [Key]
        public long Id { get; set; }
        public string UserId { get; set; }
        public string CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Product Product { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}
