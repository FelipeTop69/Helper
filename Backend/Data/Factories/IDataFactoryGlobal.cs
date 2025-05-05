using Data.Interfaces;
using Entity.Models;

namespace Data.Factories
{
    public interface IDataFactoryGlobal
    {
        IGenericRepository<Role> CreateRoleData();
    }
}
