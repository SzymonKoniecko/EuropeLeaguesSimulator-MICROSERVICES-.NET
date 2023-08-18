namespace WebScrapingIntegration.API.Entities
{
    public class WebScrapingProces
    {
        public Guid Id { get; set; }
        public string NameOfMethod { get; set; }
        public string GivenQuery { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
