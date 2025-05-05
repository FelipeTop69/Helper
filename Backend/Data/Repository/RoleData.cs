using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.Models;
using Microsoft.Extensions.Logging;

namespace Data.Repository
{
    public class RoleData : GenericRepository<Role>
    {
        public RoleData(AppDbContext context, ILogger<Role> logger)
            :base(context, logger)
        {
            
        }
    }
}