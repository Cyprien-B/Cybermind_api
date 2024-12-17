using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CyberMind_API.Modeles;
using CyberMind_API.DTOs;
using CyberMind_API.dbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberMind_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReponseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReponseController(AppDbContext context)
        {
            _context = context;
        }

        // Create a new Reponse
        [HttpPost]
        public IActionResult CreateReponse([FromBody] Reponse newReponse)
        {
            if (newReponse == null)
            {
                return BadRequest("Reponse is null.");
            }

            var challenge = _context.Challenge.FirstOrDefault(c => c.Id == newReponse.Challenge.Id);
            if (challenge == null)
            {
                return BadRequest("Invalid ChallengeId.");
            }

            newReponse.Challenge = challenge;

            _context.Reponses.Add(newReponse);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetReponseById), new { id = newReponse.Id }, newReponse);
        }


        // Get a Reponse by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ReponseDTO>> GetReponseById(int id)
        {
            var reponse = await _context.Reponses
                .Include(r => r.Challenge)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (reponse == null)
            {
                return NotFound();
            }

            var reponseDTO = new ReponseDTO
            {
                Id = reponse.Id,
                Score = reponse.Score,
                Answer = reponse.Answer,
                Challenge = reponse.Challenge,
                User = reponse.User
            };

            return Ok(reponseDTO);
        }

        // Update a Reponse by Id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReponse(uint id, [FromBody] ReponseDTO updatedReponseDTO)
        {
            if (updatedReponseDTO == null || updatedReponseDTO.Id != id)
            {
                return BadRequest("ReponseDTO is null or Id mismatch.");
            }

            var reponse = await _context.Reponses.FirstOrDefaultAsync(e => e.Id == id);
            if (reponse == null)
            {
                return NotFound();
            }

            reponse.Score = updatedReponseDTO.Score;
            reponse.Answer = updatedReponseDTO.Answer;

            var challenge = await _context.Challenge.FirstOrDefaultAsync(c => c.Id == updatedReponseDTO.Challenge.Id);
            if (challenge == null)
            {
                return BadRequest("Invalid Challenge.");
            }
            reponse.Challenge = challenge;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedReponseDTO.User.Id);
            if (user == null)
            {
                return BadRequest("Invalid User.");
            }
            reponse.User = user;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // Delete a Reponse by Id
        [HttpDelete("{id}")]
        public IActionResult DeleteReponse(uint id)
        {
            var reponse = _context.Reponses.FirstOrDefault(e => e.Id == id);
            if (reponse == null)
            {
                return NotFound();
            }

            _context.Reponses.Remove(reponse);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
