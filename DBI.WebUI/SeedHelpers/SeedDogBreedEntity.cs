using Newtonsoft.Json;

namespace DBI.Domain.Helpers
{
    public class SeedDogBreedEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("training_index")]
        public int TrainingIndex { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("avatar")]
        public string AvatarLink { get; set; }
        [JsonProperty("normalizedName")]
        public string NormalizedName { get; set; }

        public SeedDogBreedEntity(string name, string description, int trainingIndex, string avatarLink, string normalizedName)
        {
            this.Name = name;
            this.Description = description;
            this.TrainingIndex = trainingIndex;
            this.AvatarLink = avatarLink;
            this.NormalizedName = normalizedName;
        }
    }
}