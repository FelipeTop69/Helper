using AutoMapper;
using Data.Factories;
using Entity.DTOs.UserDTOs;
using Entity.Models;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services
{
    public class UserBusiness : GenericServices<User, UserDTO>
    {
        public UserBusiness(IDataFactoryGlobal factory, ILogger<User> logger, IMapper mapper) : base(factory.CreateUserData(), logger, mapper)
        {

        }

        public async Task<UserOptionsDTO> CreateAsyncNew(UserOptionsDTO dtoExp)
        {
            ValidateOptions(dtoExp);
            await ValidateOptionsCreate(dtoExp);
            try
            {
                var entity = _mapper.Map<User>(dtoExp);
                var created = await _data.CreateAsync(entity);
                return _mapper.Map<UserOptionsDTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear el UserRol {dtoExp.Id}");
                throw new ExternalServiceException("Base de datos", $"Error al crear {dtoExp.Id}", ex);
            }
        }

        public async Task<UserOptionsDTO> UpdateAsyncNew(UserOptionsDTO dtoExp)
        {
            var idProp = dtoExp.Id;
            if (idProp <= 0)
                throw new ValidationException("Id", "El ID debe ser mayor que cero");

            ValidateOptions(dtoExp);

            var existingEntity = await _data.GetByIdAsync(idProp);
            if (existingEntity == null)
                throw new EntityNotFoundException($"{dtoExp.Username}", idProp);
            try
            {

                _mapper.Map(dtoExp, existingEntity);  
                var updated = await _data.UpdateAsync(existingEntity);
                return _mapper.Map<UserOptionsDTO>(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar {dtoExp.Id}");
                throw new ExternalServiceException("Base de datos", $"Error al actualizar {dtoExp.Id}", ex);
            }
        }

        protected void ValidateOptions(UserOptionsDTO entity)
        {
            if (entity == null)
            {
                throw new ValidationException("El objeto Role no puede ser nulo.");
            }

            if (entity.PersonId <= 0)
            {
                _logger.LogWarning("Intento de crear/actualizar un RolUser con UserId inválido.");
                throw new ValidationException("UserId", "El ID del usuario debe ser mayor que cero.");
            }

            if (string.IsNullOrWhiteSpace(entity.Username))
            {
                _logger.LogWarning("Intento de crear/actualizar un Role con Name vacío.");
                throw new ValidationException("Name", "El nombre del Role es obligatorio.");
            }
        }

        protected async Task ValidateOptionsCreate(UserOptionsDTO entity)
        {
            var normalizedNewName = Normalize(entity.Username);

            var allForms = await GetAllAsync();

            var exists = allForms.Any(f =>
                Normalize(f.Username) == normalizedNewName
            );

            if (exists)
                throw new ValidationException("Ya existe un User con ese nombre.");
        }

        private string Normalize(string input)
        {
            return input.Trim().ToLower().Replace(" ", "");
        }


        protected override void Validate(UserDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
