using AutoMapper;
using Data.Factories;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Models;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services
{
    public class PersonBusiness : GenericServices<Person, PersonDTO>
    {
        private readonly IDataFactoryGlobal _factory;

        public PersonBusiness(IDataFactoryGlobal factory, ILogger<Person> logger, IMapper mapper) : base(factory.CreatePersonData(), logger, mapper)
        {
            _factory = factory;

        }

        protected override void Validate(PersonDTO person)
        {
            if (person == null)
            {
                throw new ValidationException("El objeto Person no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(person.Name))
            {
                _logger.LogWarning("Intento de crear/actualizar un Peron con Name vacío.");
                throw new ValidationException("Name", "El nombre del Person es obligatorio.");
            }
        }

        protected override async Task ValidateCreate(PersonDTO person)
        {
            var normalizedNewName = Normalize(person.Name);

            var allForms = await GetAllAsync();

            var exists = allForms.Any(f =>
                Normalize(f.Name) == normalizedNewName
            );

            if (exists)
                throw new ValidationException("Ya existe un Person con ese nombre.");
        }

        public async Task<IEnumerable<PersonDTO>> GetAvailableAsync()
        {
            var allPersons = await _data.GetAllAsync();
            var allUsers = await _factory.CreateUserData().GetAllAsync();

            var usedPersonIds = allUsers.Select(u => u.PersonId).ToHashSet();

            var availablePersons = allPersons
                .Where(p => !usedPersonIds.Contains(p.Id))
                .ToList();

            return _mapper.Map<IEnumerable<PersonDTO>>(availablePersons);
        }

        private string Normalize(string input)
        {
            return input.Trim().ToLower().Replace(" ", "");
        }
    }
}
