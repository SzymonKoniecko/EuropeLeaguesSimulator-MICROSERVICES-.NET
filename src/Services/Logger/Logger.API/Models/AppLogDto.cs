namespace Logger.API.Models
{
    public class AppLogDto
    {
        public Guid Id { get; set; }
        public string Application { get; set; }
        public DateTime LoggedTime { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
        public string Exception { get; set; }
    }
}
