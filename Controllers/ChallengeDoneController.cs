using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CyberMind_API.Modeles;
using CyberMind_API.DTOs;
using System.Collections.Generic;
using System.Linq;
using CyberMind_API.dbContext;
using Microsoft.EntityFrameworkCore;

namespace CyberMind_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengeDoneController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // list all ChallengeDone
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChallengeDoneDTO>>> GetAllChallengeDone()
        {
            var challengeDones = await _context.ChallengeDones
                .Include(cd => cd.User)
                .Include(cd => cd.ImageReponse)
                .Include(cd => cd.Challenge)
                .Include(cd => cd.Reponse)
                .ToListAsync();

            var challengeDoneDTOs = challengeDones.Select(cd => new ChallengeDoneDTO
            {
                Id = cd.Id,
                User = cd.User,
                ImageReponse = cd.ImageReponse,
                Challenge = cd.Challenge,
                Reponse = cd.Reponse
            });

            return Ok(challengeDoneDTOs);
        }

        // Create a new ChallengeDone
        [HttpPost]
        public async Task<ActionResult<ChallengeDone>> CreateChallengeDone([FromBody] ChallengeDone challengeDone)
        {
            if (challengeDone == null)
            {
                return BadRequest("Challenge done is null.");
            }

            _context.ChallengeDones.Add(challengeDone);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateChallengeDone), new { id = challengeDone.Id }, challengeDone);
        }



        // PUT: api/ChallengeDone/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChallengeDone(int id, ChallengeDoneDTO challengeDoneDTO)
        {
            if (id != challengeDoneDTO.Id)
            {
                return BadRequest();
            }

            var challengeDone = await _context.ChallengeDones.FindAsync(id);
            if (challengeDone == null)
            {
                return NotFound();
            }

            challengeDone.User = challengeDoneDTO.User;
            challengeDone.ImageReponse = challengeDoneDTO.ImageReponse;
            challengeDone.Challenge = challengeDoneDTO.Challenge;
            challengeDone.Reponse = challengeDoneDTO.Reponse;

            _context.Entry(challengeDone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ChallengeDones.Any(e => e.Id == id))
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

        // DELETE: api/ChallengeDones/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallengeDones(int id)
        {
            var challengeDone = await _context.ChallengeDones.FindAsync(id);
            if (challengeDone == null)
            {
                return NotFound();
            }

            _context.ChallengeDones.Remove(challengeDone);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
