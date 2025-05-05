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
        public IGenericRepository<Role> CreateRoleData()
        {
            var logger = _loggerFactory.CreateLogger<Role>();
            return new RoleData(_context, logger);
        }
        
    }
}
