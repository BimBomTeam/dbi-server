using System.ComponentModel.DataAnnotations;

namespace DBI.Domain.Entities.Core
{
    public class BreedTrainingProps : BaseEntity<int>
    {
        public int IdInTrainingDataset { get; set; }
        public virtual string NameInTrainingDataset { get; set; }

        [Required]
        public int DogBreedId { get; set; }
        public DogBreed DogBreed { get; set; } = null!;
    }
}
