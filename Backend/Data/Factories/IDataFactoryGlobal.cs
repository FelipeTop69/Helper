using Data.Interfaces;
using Entity.Models;

namespace Data.Factories
{
    public interface IDataFactoryGlobal
    {
        IGenericRepository<Person> CreatePersonData();
        IGenericRepository<Role> CreateRoleData();
        IGenericRepository<UserRole> CreateUserRoleData();
        IGenericRepository<User> CreateUserData();
    }
}
