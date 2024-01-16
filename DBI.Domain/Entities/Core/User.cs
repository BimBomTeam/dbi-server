using System.ComponentModel.DataAnnotations;

namespace DBI.Domain.Entities.Core
{
    public class User : BaseEntity<string>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Identifier { get; set; }
        public string? Role { get; set; } = "user";

        public ICollection<SearchHistoryEntity> Histories{ get; set; } =  new List<SearchHistoryEntity>();
    }
}
