using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repository
{
    public class UserData : GenericRepository<User>
    {
        private readonly AppDbContext _context;
        public UserData(AppDbContext context, ILogger<User> logger) : base(context, logger)
        {
            _context = context;
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await _context.User
                    .Include(u => u.Person)
                    .Where(u => u.Active)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.User
                    .Include(u => u.Person)
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
