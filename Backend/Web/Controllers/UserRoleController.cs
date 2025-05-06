using Business.Services;
using Entity.DTOs.UserRoleDTOs;
using Microsoft.AspNetCore.Mvc;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Produces("application/json")]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleBusiness _userRoleBusiness;
        private readonly ILogger<UserRoleController> _logger;

        public UserRoleController(UserRoleBusiness userRoleBusiness, ILogger<UserRoleController> logger)
        {
            _userRoleBusiness = userRoleBusiness;
            _logger = logger;
        }

        [HttpGet("GetAll/")]
        [ProducesResponseType(typeof(IEnumerable<UserRoleDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllUserRoles()
        {
            try
            {
                var UserRoles = await _userRoleBusiness.GetAllAsync();
                return Ok(UserRoles);
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener UserRoles");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(UserRoleDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserRoleById(int id)
        {
            try
            {
                var UserRole = await _userRoleBusiness.GetByIdAsync(id);
                return Ok(UserRole);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida para obtener el UserRole con ID: {UserRoleId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "UserRole no encontrado con ID: {RolId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener UserRole con ID: {RolId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("Create/")]
        [ProducesResponseType(typeof(UserRoleDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRol([FromBody] UserRoleOptionsDTO RolDto)
        {
            try
            {
                var createdRol = await _userRoleBusiness.CreateAsyncNew(RolDto);
                return CreatedAtAction(nameof(GetUserRoleById), new { id = createdRol.Id }, createdRol);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al crear Role");
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al crear Role");
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpPut("Update/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUserRole([FromBody] UserRoleOptionsDTO UserRoleDto)
        {
            try
            {
                var update = await _userRoleBusiness.UpdateAsyncNew(UserRoleDto);
                return Ok(update);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar UserRole con ID: {UserRoleId}", UserRoleDto.Id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "UserRole no encontrado con ID: {UserRoleId}", UserRoleDto.Id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar el  con ID: {UserRoleId}", UserRoleDto.Id);
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpDelete("Persistence/{id}/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            try
            {
                var response = await _userRoleBusiness.DeleteAsync(id);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al eliminar UserRole con ID: {UserRoleId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "UserRole no encontrado con ID: {UserRoleId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar el UserRole con ID: {UserRoleId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPatch("Logical/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteLogicalUserRole(int id)
        {
            try
            {
                var response = await _userRoleBusiness.DeleteLogicalAsync(id);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al eliminar UserRole con ID: {UserRoleId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "UserRole no encontrado con ID: {UserRoleId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar el UserRole con ID: {UserRoleId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

