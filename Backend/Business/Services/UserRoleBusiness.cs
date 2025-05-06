using AutoMapper;
using Data.Factories;
using Entity.DTOs.UserRoleDTOs;
using Entity.Models;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services
{
    public class UserRoleBusiness : GenericServices<UserRole, UserRoleDTO>
    {
        public UserRoleBusiness(IDataFactoryGlobal factory, ILogger<UserRole> logger, IMapper mapper) : base(factory.CreateUserRoleData(), logger, mapper)
        {

        }

        public async Task<UserRoleOptionsDTO> CreateAsyncNew(UserRoleOptionsDTO dtoExp)
        {
            ValidateOptions(dtoExp);

            try
            {
                var entity = _mapper.Map<UserRole>(dtoExp);
                var created = await _data.CreateAsync(entity);
                return _mapper.Map<UserRoleOptionsDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear el UserRole {dtoExp.Id}");
                throw new ExternalServiceException("Base de datos", $"Error al crear {dtoExp.Id}", ex);
            }
        }

        public async Task<UserRoleOptionsDTO> UpdateAsyncNew(UserRoleOptionsDTO dtoExp)
        {
            var idProp = dtoExp.Id;
            if (idProp <= 0)
                throw new ValidationException("Id", "El ID debe ser mayor que cero");

            ValidateOptions(dtoExp);

            var existingEntity = await _data.GetByIdAsync(idProp);
            if (existingEntity == null)
                throw new EntityNotFoundException("UserRole", idProp);
            try
            {

                _mapper.Map<UserRole>(dtoExp);
                var updated = await _data.UpdateAsync(existingEntity);
                return _mapper.Map<UserRoleOptionsDTO>(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar {dtoExp.Id}");
                throw new ExternalServiceException("Base de datos", $"Error al actualizar {dtoExp.Id}", ex);
            }
        }

        protected void ValidateOptions(UserRoleOptionsDTO entity)
        {
            if (entity == null)
            {
                throw new ValidationException("El objeto Role no puede ser nulo.");
            }

            if (entity.UserId <= 0)
            {
                _logger.LogWarning("Intento de crear/actualizar un RolUser con UserId inválido.");
                throw new ValidationException("UserId", "El ID del usuario debe ser mayor que cero.");
            }

            if (entity.RoleId <= 0)
            {
                _logger.LogWarning("Intento de crear/actualizar un RolUser con RoleId inválido.");
                throw new ValidationException("RoleId", "El ID del rol debe ser mayor que cero.");
            }
        }


        protected override void Validate(UserRoleDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
