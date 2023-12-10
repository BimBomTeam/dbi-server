namespace DBI.Domain.Entities.Core
{
    public class SearchHistoryEntity : BaseEntity<int>
    {
        public DateTime Date { get; set; }
        public int DogBreedId { get; set; }
        public virtual DogBreed DogBreed { get; set; }
    }
}