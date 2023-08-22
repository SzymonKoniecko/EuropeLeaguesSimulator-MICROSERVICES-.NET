using System.Diagnostics.CodeAnalysis;

namespace DataHub.API.Entities
{
    public class Stadium
    {
        public Guid Id { get; set; }
        [AllowNull]
        public string FullName { get; set; }
        [AllowNull]
        public int Capacity { get; set; }
        [AllowNull]
        public string ImageUrl { get; set; }
        public virtual Club Club { get; set; }
    }
}
