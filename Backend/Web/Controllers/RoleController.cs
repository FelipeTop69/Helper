using Business.Services;
using Entity.Context;
using Entity.DTOs;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Produces("application/json")]
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly RoleBusiness _RoleBusiness;
        private readonly ILogger<RoleController> _logger;

        public RoleController(AppDbContext context, RoleBusiness RoleBusiness, ILogger<RoleController> logger)
        {
            _context = context;
            _RoleBusiness = RoleBusiness;
            _logger = logger;
        }

        [HttpGet("GetAll/")]
        [ProducesResponseType(typeof(IEnumerable<RoleDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var Rols = await _RoleBusiness.GetAllAsync();
                return Ok(Rols);
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener Roles");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(RoleDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRolById(int id)
        {
            try
            {
                var Rol = await _RoleBusiness.GetByIdAsync(id);
                return Ok(Rol);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida para obtener el Role con ID: {RolId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Role no encontrado con ID: {RolId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener Role con ID: {RolId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("Create/")]
        [ProducesResponseType(typeof(RoleDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRol([FromBody] RoleDTO RolDto)
        {
            try
            {
                var createdRol = await _RoleBusiness.CreateAsync(RolDto);
                return CreatedAtAction(nameof(GetRolById), new { id = createdRol.Id }, createdRol);
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
        public async Task<IActionResult> UpdateRol([FromBody] RoleDTO RolDto)
        {
            try
            {
                var update = await _RoleBusiness.UpdateAsync(RolDto);
                return Ok(update);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar Role con ID: {RolId}", RolDto.Id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Role no encontrado con ID: {RolId}", RolDto.Id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar el Role con ID: {RolId}", RolDto.Id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        [HttpDelete("Persistence/{id}/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteRol(int id)
        {
            try
            {
                var response = await _RoleBusiness.DeleteAsync(id);
                return Ok(response); 
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al eliminar Role con ID: {RolId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Role no encontrado con ID: {RolId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar el Role con ID: {RolId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPatch("Logical/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PatchLogicalRol(int id)
        {
            try
            {
                var response = await _RoleBusiness.DeleteLogicalAsync(id);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al desactivar el rol con ID: {RolId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Role no encontrado con ID: {RolId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al desactivar el rol con ID: {RolId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}