using System.ComponentModel.DataAnnotations;

namespace DBI.Domain.Entities.Core
{
    public class DogBreed : BaseEntity<int>
    {
        [Required]
        public string ShowName { get; set; }
        public string ShortDescription { get; set; }

        public int? BreedTrainingPropsId { get; set; }
        public virtual BreedTrainingProps? BreedTrainingProps { get; set; }
        public virtual ICollection<SearchHistoryEntity> SearchHistories { get; set; } = new List<SearchHistoryEntity>();
    }
}
