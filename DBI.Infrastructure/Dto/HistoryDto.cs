namespace DBI.Infrastructure.Dto
{
    public class HistoryDto
    {
        public int Id { get; set; }
        public int DogBreedName { get; set; }
        public int DogBreedId { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedDate { get; set; } = default(DateTime?);
        public string AvatarLink { get; set; }
    }
}
