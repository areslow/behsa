namespace bghBackend.Domain.Entities
{
    public class CouncilRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string DatePosted { get; set; }
        public string Status { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsDeleted { get; set; }
    }
}
