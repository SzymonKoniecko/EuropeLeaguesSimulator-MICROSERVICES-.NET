namespace DataHub.API.Models
{
    public class ClubDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? NickName { get; set; }
        public string Logo { get; set; }
        public string Manager { get; set; }
        public string League { get; set; }
        public StadiumDto StadiumDto { get; set; }
    }
}
