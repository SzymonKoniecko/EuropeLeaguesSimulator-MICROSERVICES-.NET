namespace DataHub.API.Entities
{
    public class Stadium
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int Capacity { get; set; }
        public string ImageUrl { get; set; }
        public Guid ClubId { get; set; }
        public virtual Club Club { get; set; }
    }
}
