namespace DataHub.API.Entities
{
    public class Club
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? NickName { get; set; }
        public string Logo { get; set; }
        public string Manager { get; set; }
        public string League { get; set; }
        public Guid StadiumId { get; set; }
        public virtual Stadium Stadium { get; set; }
    }
}
