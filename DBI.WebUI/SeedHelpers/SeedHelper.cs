using DBI.Application;
using DBI.Domain.Entities.Core;
using Newtonsoft.Json;
using System;

namespace DBI.Domain.Helpers
{
    public static class SeedHelper
    {
        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                try
                {
                    if (!context.DogBreeds.Any())
                    {
                        var seedDogBreeds = FetchJson();

                        foreach (var seedDogBreed in seedDogBreeds)
                        {
                            var dogBreed = new DogBreed
                            {
                                ShowName = seedDogBreed.Name,
                                ShortDescription = seedDogBreed.Description,
                                BreedTrainingProps = new BreedTrainingProps
                                {
                                    IdInTrainingDataset = seedDogBreed.TrainingIndex
                                }
                            };

                            context.DogBreeds.Add(dogBreed);
                        }

                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return app;
            }
        }

        public static IEnumerable<SeedDogBreedEntity> FetchJson()
        {
            string filePath = @"E:\Codes\DogBreedIdentification\Server\DBI.WebUI\SeedHelpers\result.json";

            string json = File.ReadAllText(filePath);
            IEnumerable<SeedDogBreedEntity> breeds = JsonConvert.DeserializeObject<IEnumerable<SeedDogBreedEntity>>(json);

            return breeds;
        }
    }
}
