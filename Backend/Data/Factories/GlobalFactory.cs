using Data.Interfaces;
using Data.Repository;
using Entity.Context;
using Entity.Models;
using Microsoft.Extensions.Logging;

namespace Data.Factories
{
    public class GlobalFactory : IDataFactoryGlobal
    {
        private readonly AppDbContext _context;
        private readonly ILoggerFactory _loggerFactory;
        public GlobalFactory(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _loggerFactory = loggerFactory;
        }

        public IGenericRepository<Person> CreatePersonData()
        {
            var logger = _loggerFactory.CreateLogger<Person>();
            return new PersonData(_context, logger);
        }

        public IGenericRepository<Role> CreateRoleData()
        {
            var logger = _loggerFactory.CreateLogger<Role>();
            return new RoleData(_context, logger);
        }

        public IGenericRepository<UserRole> CreateUserRoleData()
        {
            var logger = _loggerFactory.CreateLogger<UserRole>();
            return new UserRoleData(_context, logger);
        }

        public IGenericRepository<User> CreateUserData()
        {
            var logger = _loggerFactory.CreateLogger<User>();
            return new UserData(_context, logger);
        }
    }
}
