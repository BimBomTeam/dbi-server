using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Queries;

namespace DBI.Application.Queries
{
    public class DogBreedQuery : BaseQuery<DogBreed, int>, IDogBreedQuery
    {
        public DogBreedQuery(ApplicationDbContext context) : base(context)
        {
        }
    }
}
