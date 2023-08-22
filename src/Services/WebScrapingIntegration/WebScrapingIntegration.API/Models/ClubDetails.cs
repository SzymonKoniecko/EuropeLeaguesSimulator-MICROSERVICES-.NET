namespace WebScrapingIntegration.API.Models
{
    public class ClubDetails
    {
        public string FullName { get; set; }
        public string? NickName { get; set; }
        public string LogoUrl { get; set; }
        public string Manager { get; set; }
        public string League { get; set; }
        public string StadiumFullName { get; set; }
        public string StadiumCapacity { get; set; }
        public string StadiumImageUrl { get; set; }

        public void SetNoContentIfNull()
        {
            if (string.IsNullOrWhiteSpace(this.FullName)) { this.FullName = "NoContent"; }
            if (string.IsNullOrWhiteSpace(this.NickName)) { this.NickName = "NoContent"; }
            if (string.IsNullOrWhiteSpace(this.LogoUrl)) { this.LogoUrl = "NoContent"; }
            if (string.IsNullOrWhiteSpace(this.Manager)) { this.Manager = "NoContent"; }
            if (string.IsNullOrWhiteSpace(this.League)) { this.League = "NoContent"; }
            if (string.IsNullOrWhiteSpace(this.StadiumFullName)) { this.StadiumFullName = "NoContent"; }
            if (string.IsNullOrWhiteSpace(this.StadiumCapacity)) { this.StadiumCapacity = "NoContent"; }
            if (string.IsNullOrWhiteSpace(this.StadiumImageUrl)) { this.StadiumImageUrl = "NoContent"; }
        }
    }
}
