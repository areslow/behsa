namespace bghBackend.Application.Common.OtherModerls
{
    public class PaginationModel
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; } // totall page count
        public int CurrentPage { get; set; }
    }
}
