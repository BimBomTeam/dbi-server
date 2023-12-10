using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Commands;

namespace DBI.Application.Commands
{
    public class DogBreedCommand : BaseCommand<DogBreed, int>, IDogBreedCommand
    {
        public DogBreedCommand(ApplicationDbContext context) : base(context)
        {
        }
    }
}
