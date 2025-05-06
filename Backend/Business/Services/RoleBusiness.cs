using AutoMapper;
using Data.Factories;
using Entity.DTOs;
using Entity.Models;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services
{
    public class RoleBusiness : GenericServices<Role, RoleDTO>
    {
        public RoleBusiness(IDataFactoryGlobal factory, ILogger<Role> logger, IMapper mapper) 
            : base(factory.CreateRoleData(), logger, mapper)
        {
        }

        protected override  void Validate(RoleDTO role)
        {
            if (role == null)
            {
                throw new ValidationException("El objeto Role no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(role.Name))
            {
                _logger.LogWarning("Intento de crear/actualizar un Role con Name vacío.");
                throw new ValidationException("Name", "El nombre del Role es obligatorio.");
            }
        }

        protected override async Task ValidateCreate(RoleDTO role)
        {
            var normalizedNewName = Normalize(role.Name);

            var allForms = await GetAllAsync();

            var exists = allForms.Any(f =>
                Normalize(f.Name) == normalizedNewName
            );

            if (exists)
                throw new ValidationException("Ya existe un Role con ese nombre.");
        }

        private string Normalize(string input)
        {
            return input.Trim().ToLower().Replace(" ", "");
        }
    }
}