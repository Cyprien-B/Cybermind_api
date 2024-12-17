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
    public class ChallengeController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;


        // GET: api/Challenge
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChallengeDTO>>> GetAllChallenges()
        {
            var challenges = await _context.Challenge
                .Include(c => c.Etablissement)
                .Include(c => c.ImageEnonces)
                .Include(c => c.Reponse)
                .ToListAsync();

            var challengeDTOs = challenges.Select(c => new ChallengeDTO
            {
                Id = c.Id,
                Titre = c.Titre,
                Enonce = c.Enonce,
                EtablissementId = c.EtablissementId,
                Etablissement = c.Etablissement,
                ImageEnonces = c.ImageEnonces,
                Categories = c.Categories,
                Reponse = c.Reponse
            });

            return Ok(challengeDTOs);
        }

        // GET: api/Challenge/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChallengeDTO>> GetChallenge(int id)
        {
            var challenge = await _context.Challenge
                .Include(c => c.Etablissement)
                .Include(c => c.ImageEnonces)
                .Include(c => c.Reponse)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (challenge == null)
            {
                return NotFound();
            }

            var challengeDTO = new ChallengeDTO
            {
                Id = challenge.Id,
                Titre = challenge.Titre,
                Enonce = challenge.Enonce,
                EtablissementId = challenge.EtablissementId,
                Etablissement = challenge.Etablissement,
                ImageEnonces = challenge.ImageEnonces,
                Categories = challenge.Categories,
                Reponse = challenge.Reponse
            };

            return Ok(challengeDTO);
        }

        // POST: api/Challenge
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Challenge>> CreateChallenge([FromBody] Challenge challenge)
        {
            if (challenge == null)
            {
                return BadRequest("Challenge is null.");
            }

            _context.Challenge.Add(challenge);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChallenge), new { id = challenge.Id }, challenge);
        }


        // PUT: api/Challenge/5
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditChallenge(int id, [FromBody] ChallengeDTO challengeDTO)
        {
            if (id != challengeDTO.Id)
            {
                return BadRequest();
            }

            var challenge = await _context.Challenge.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }

            challenge.Titre = challengeDTO.Titre;
            challenge.Enonce = challengeDTO.Enonce;
            challenge.EtablissementId = challengeDTO.EtablissementId;
            challenge.Etablissement = challengeDTO.Etablissement;
            challenge.ImageEnonces = challengeDTO.ImageEnonces;
            challenge.Categories = challengeDTO.Categories;
            challenge.Reponse = challengeDTO.Reponse;

            _context.Entry(challenge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Challenge.Any(e => e.Id == id))
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

        // DELETE: api/Challenge/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteChallenge(int id)
        {
            var challenge = await _context.Challenge.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }

            _context.Challenge.Remove(challenge);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
