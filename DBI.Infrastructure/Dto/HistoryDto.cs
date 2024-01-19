namespace DBI.Infrastructure.Dto
{
    public class HistoryDto
    {
        public int DogBreedId { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedDate { get; set; } = default(DateTime?);
    }
}
