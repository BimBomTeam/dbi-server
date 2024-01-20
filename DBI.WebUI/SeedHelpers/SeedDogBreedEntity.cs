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

        public SeedDogBreedEntity(string name, string description, int trainingIndex)
        {
            this.Name = name;
            this.Description = description;
            this.TrainingIndex = trainingIndex;
        }
    }
}