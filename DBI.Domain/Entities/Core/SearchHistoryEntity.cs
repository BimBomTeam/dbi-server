using System.ComponentModel.DataAnnotations;

namespace DBI.Domain.Entities.Core
{
    public class SearchHistoryEntity : BaseEntity<int>
    {
        public DateTime Date { get; set; }
        public int DogBreedId { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual DogBreed DogBreed { get; set; }
        public virtual User User { get; set; }
    }
}