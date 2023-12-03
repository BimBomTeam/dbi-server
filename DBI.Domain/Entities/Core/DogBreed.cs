using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBI.Domain.Entities.Core
{
    public class DogBreed : BaseEntity<int>
    {
        [Required]
        public string ShowName { get; set; }
        public string ShortDescription { get; set; }

        public int DogBreedTrainingPropsId { get; set; }
        public virtual BreedTrainingProps? BreedTrainingProps { get; set; }
    }
}
