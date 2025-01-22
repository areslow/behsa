namespace bghBackend.Application.Common.OtherModerls
{
    public class SearchModelForContents
    {
        // if any of these parameters are true the requests goes through the corresponding table(s)
        public bool AuthorName { get; set; }
        public bool Content { get; set; }
        public bool Abstract {  get; set; }
        public bool Refrences {  get; set; }
        // for specific date
        public string? Date { get; set; }

        // between two dates
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}
