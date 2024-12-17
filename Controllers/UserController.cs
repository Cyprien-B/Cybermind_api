using CyberMind_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CyberMind_API.dbContext;
using CyberMind_API.Modeles;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using BC = BCrypt.Net.BCrypt;


namespace CyberMind_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // Lister tout les users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {

            //  return await _context.Users.ToListAsync();

            var usersDTO = await _context.Users
         .Select(user => new UserDTO
         {
             Id = user.Id,
             Name = user.Name,
             Etablissement = user.Etablissement,
             ChallengeDones = user.ChallengeDones,
             Role = user.role.Name,
             Point = user.Point // pour que ce soit plus propre on aurait pu recuperer les point avec la somme de challengedone
         })
         .ToListAsync();

            return Ok(usersDTO);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var userDTO = await _context.Users
                .Where(user => user.Id == id)  // Recherche l'utilisateur par ID
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Etablissement = user.Etablissement,
                    ChallengeDones = user.ChallengeDones,
                    Role = user.role.Name,
                    Point = user.Point
                })
                .FirstOrDefaultAsync();

            if (userDTO == null)
            {
                return NotFound("User not found.");
            }

            return Ok(userDTO);
        }

        // Create a new User
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(string name, string mail, string password, string codeInscription)
        {
            //on vérifie que chaque valeur n'est pas nul, le rôle de l'utilisateur sera ajouter en dur pour le moment
            if (name == null)
            {
                return BadRequest("Merci de rentrer un nom");
            }
            if (mail == null) { 
                return BadRequest("mail manquant"); 
            }
            if (password == null) { 
                return BadRequest("merci d'indiquer votre password"); 
            }
            if (codeInscription == null) {
                return BadRequest("il manque le code d'etablisement");
            }

            var unetablissement = await _context.Etablissements.Where(e => e.CodeInscription == codeInscription).FirstOrDefaultAsync();

            if (unetablissement == null)
            {
                return BadRequest("Il n'existe pas d'etablisement avec ce code."); ;
            }

            uint test = 1;
            var leroledebase = await _context.Roles.FindAsync(test);

            if (leroledebase == null)
            {
                return NotFound();
            }

            string passwordHash = BC.HashPassword(password);

            var UnUser = new User()
            {
                Name = name,
                Mail = mail,
                Password = passwordHash,
                Etablissement = unetablissement,
                role = leroledebase
            };
            _context.Users.Add(UnUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateUser), new { id = UnUser.Id }, UnUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User unuser)
        {
            if (id != unuser.Id)
            {
                return BadRequest();
            }

            _context.Entry(unuser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id}/add-points")]
        public async Task<IActionResult> AddPointsToUser(int id, [FromQuery] int pointsToAdd)
        {

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User inconnu");
            }

            user.Point += pointsToAdd;

            await _context.SaveChangesAsync();


            return Ok();
        }
    }
}