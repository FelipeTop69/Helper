using Entity.Context;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repository
{
    public class UserRoleData : GenericRepository<UserRole>
    {
        private readonly AppDbContext _context;
        public UserRoleData(AppDbContext context, ILogger<UserRole> logger) : base(context, logger)
        {
            _context = context;
        }

        public override async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            try
            {
                return await _context.UserRole
                    .Include(u => u.User)
                    .Include(u => u.Role)
                    .Where(u => u.Active)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override async Task<UserRole?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.UserRole
                    .Include(u => u.User)
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
