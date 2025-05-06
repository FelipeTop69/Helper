using Entity.Context;
using Entity.Models;
using Microsoft.Extensions.Logging;

namespace Data.Repository
{
    public class PersonData : GenericRepository<Person>
    {
        public PersonData(AppDbContext context, ILogger<Person> logger) : base(context, logger) { }
    }
}
