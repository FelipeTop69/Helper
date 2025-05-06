using AutoMapper;
using Data.Interfaces;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services
{
    public abstract class GenericServices<T, DTO> where T : class
    {
        protected readonly IGenericRepository<T> _data;
        protected readonly ILogger<T> _logger;
        protected readonly IMapper _mapper;

        protected GenericServices(IGenericRepository<T> data, ILogger<T> logger, IMapper mapper)
        {
            _data = data;
            _logger = logger;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<DTO>> GetAllAsync()
        {
            try
            {
                var list = await _data.GetAllAsync();
                return _mapper.Map<IEnumerable<DTO>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener todos los {typeof(T).Name}");
                throw new ExternalServiceException("Base de datos", $"Error al obtener todos los {typeof(T).Name}", ex);
            }
        }

        public virtual async Task<DTO> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "El ID debe ser mayor que cero");

            var entity = await _data.GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(T).Name, id);

            try
            {
                return _mapper.Map<DTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener {typeof(T).Name} con ID {id}");
                throw new ExternalServiceException("Base de datos", $"Error al obtener {typeof(T).Name}", ex);
            }
        }

        public virtual async Task<DTO> CreateAsync(DTO entityDto)
        {
            Validate(entityDto);
            await ValidateCreate(entityDto);

            try
            {
                var entity = _mapper.Map<T>(entityDto);
                var created = await _data.CreateAsync(entity);
                return _mapper.Map<DTO>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear {typeof(T).Name}");
                throw new ExternalServiceException("Base de datos", $"Error al crear {typeof(T).Name}", ex);
            }
        }

        public virtual async Task<DTO> UpdateAsync(DTO entityDto)
        {
            if (entityDto == null)
                throw new ValidationException("Entidad", $"{typeof(T).Name} no puede ser nulo");

            var idProp = typeof(DTO).GetProperty("Id")?.GetValue(entityDto);
            if (idProp == null || (int)idProp <= 0)
                throw new ValidationException("Id", "El ID debe ser mayor que cero");

            Validate(entityDto);

            var existingEntity = await _data.GetByIdAsync((int)idProp);
            if (existingEntity == null)
                throw new EntityNotFoundException($"{typeof(T).Name}", (int)idProp);

            try
            {
                _mapper.Map(entityDto, existingEntity); 
                var updated = await _data.UpdateAsync(existingEntity);
                return _mapper.Map<DTO>(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar {typeof(T).Name}");
                throw new ExternalServiceException("Base de datos", $"Error al actualizar {typeof(T).Name}", ex);
            }
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "El ID debe ser mayor que cero");

            var existingEntity = await _data.GetByIdAsync(id);
            if (existingEntity == null)
                throw new EntityNotFoundException($"{typeof(T).Name}", id);

            try
            {
                var deleted = await _data.DeletePersistenceAsync(id);
                if (deleted == false)
                    throw new ExternalServiceException("Base de datos", $"No se pudo eliminar {typeof(T).Name}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar {typeof(T).Name} con ID {id}");
                throw new ExternalServiceException("Base de datos", $"Error al eliminar {typeof(T).Name} con ID {id}", ex);
            }
        }

        public virtual async Task<bool> DeleteLogicalAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "El ID debe ser mayor que cero");

            var existingEntity = await _data.GetByIdAsync(id);
            if (existingEntity == null)
                throw new EntityNotFoundException($"{typeof(T).Name}", id);

            try
            {
                var deleted = await _data.DeleteLogicalAsync(id);
                if (deleted == false)
                    throw new ExternalServiceException("Base de datos", $"No se pudo eliminar de manera logica {typeof(T).Name}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar logicamente {typeof(T).Name} con ID {id}");
                throw new ExternalServiceException("Base de datos", $"Error al eliminar logicamente {typeof(T).Name} con ID {id}", ex);
            }
        }


        protected abstract void Validate(DTO entity);
        protected abstract Task ValidateCreate(DTO entity);
    }
}
