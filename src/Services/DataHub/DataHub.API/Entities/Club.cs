using System.Diagnostics.CodeAnalysis;

namespace DataHub.API.Entities
{
    public class Club
    {
        public Guid Id { get; set; }
        [AllowNull]
        public string FullName { get; set; }
        [AllowNull]
        public string NickName { get; set; }
        [AllowNull]
        public string Logo { get; set; }
        [AllowNull]
        public string Manager { get; set; }
        [AllowNull]
        public string League { get; set; }
        [AllowNull]
        public Guid StadiumId { get; set; }
        public virtual Stadium Stadium { get; set; }
    }
}
