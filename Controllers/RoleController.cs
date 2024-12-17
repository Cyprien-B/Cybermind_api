using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CyberMind_API.Modeles;
using CyberMind_API.DTOs;
using CyberMind_API.dbContext;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace CyberMind_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoleController(AppDbContext context)
        {
            _context = context;
        }

        // list all Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAllRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            var roleDTOs = roles.Select(role => new RoleDTO
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();

            return Ok(roleDTOs);
        }

        // Create a new Role
        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRole), new { id = role.Id }, role);
        }


        // PUT: api/role/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(uint id, RoleDTO roleDTO)
        {
            if (id != roleDTO.Id)
            {
                return BadRequest();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = roleDTO.Name;

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Roles.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/role/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(uint id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

