namespace DataHub.API.Models
{
    public class StadiumDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int Capacity { get; set; }
        public string ImageUrl { get; set; }
    }
}
