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
using Microsoft.AspNetCore.Authorization;

namespace CyberMind_API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les utilisateurs et les retourne sous forme de DTO (Data Transfer Object).
        /// Cette méthode effectue une requête dans la base de données pour obtenir tous les utilisateurs,
        /// puis mappe les données nécessaires (nom, établissement, rôle, points, etc.) vers un DTO.
        /// </summary>
        /// <returns>Liste d'objets UserDTO représentant les utilisateurs avec des informations simplifiées.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var usersDTO = await _context.Users
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Etablissement = user.Etablissement,
                    ChallengeDones = user.ChallengeDones,
                    Role = user.role.Name,
                    Point = user.Point // Pour la propreté du code, on pourrait aussi calculer les points via une somme de challenges.
                })
                .ToListAsync();

            return Ok(usersDTO);
        }

        /// <summary>
        /// Récupère un utilisateur spécifique par son identifiant (ID) et le retourne sous forme de DTO.
        /// Cette méthode permet de rechercher un utilisateur dans la base de données à l'aide de son identifiant.
        /// Si l'utilisateur est trouvé, il est retourné sous forme d'un DTO. Sinon, un message d'erreur est renvoyé.
        /// </summary>
        /// <param name="id">L'identifiant de l'utilisateur à récupérer.</param>
        /// <returns>Un objet UserDTO contenant les informations de l'utilisateur ou un message d'erreur si l'utilisateur n'est pas trouvé.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var userDTO = await _context.Users
                .Where(user => user.Id == id)
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

        /// <summary>
        /// Crée un nouvel utilisateur avec les informations fournies.
        /// Cette méthode permet de créer un utilisateur en spécifiant son nom, son mail, son mot de passe
        /// et son code d'inscription. Les informations sont validées avant la création, et le rôle par défaut
        /// est attribué à l'utilisateur.
        /// </summary>
        /// <param name="name">Nom de l'utilisateur.</param>
        /// <param name="mail">Adresse mail de l'utilisateur.</param>
        /// <param name="password">Mot de passe de l'utilisateur.</param>
        /// <param name="codeInscription">Code d'inscription de l'établissement auquel l'utilisateur est rattaché.</param>
        /// <returns>Un code HTTP 201 si l'utilisateur a été créé avec succès, ou un code d'erreur en cas de problème.</returns>

        // A authoriser pour tous
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(string name, string mail, string password, string codeInscription)
        {
            if (name == null)
            {
                return BadRequest("Merci de rentrer un nom");
            }
            if (mail == null)
            {
                return BadRequest("mail manquant");
            }
            if (password == null)
            {
                return BadRequest("merci d'indiquer votre password");
            }
            if (codeInscription == null)
            {
                return BadRequest("il manque le code d'etablisement");
            }

            var unetablissement = await _context.Etablissements
                .Where(e => e.CodeInscription == codeInscription)
                .FirstOrDefaultAsync();

            if (unetablissement == null)
            {
                return BadRequest("Il n'existe pas d'etablissement avec ce code.");
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

        /// <summary>
        /// Met à jour les informations d'un utilisateur existant.
        /// Cette méthode permet de modifier les informations d'un utilisateur en fonction de son ID.
        /// Si l'utilisateur existe, les modifications sont appliquées et sauvegardées dans la base de données.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à mettre à jour.</param>
        /// <param name="unuser">Objet User contenant les nouvelles données de l'utilisateur.</param>
        /// <returns>Un code HTTP 200 si l'utilisateur a été mis à jour, ou un code d'erreur en cas de problème.</returns>
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

        /// <summary>
        /// Supprime un utilisateur existant.
        /// Cette méthode permet de supprimer un utilisateur de la base de données en fonction de son ID.
        /// Si l'utilisateur existe, il est supprimé, sinon une erreur est retournée.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à supprimer.</param>
        /// <returns>Un code HTTP 200 si l'utilisateur a été supprimé, ou un code d'erreur si l'utilisateur n'est pas trouvé.</returns>
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

        /// <summary>
        /// Ajoute des points à un utilisateur existant.
        /// Cette méthode permet d'ajouter un certain nombre de points à l'utilisateur en fonction de son ID.
        /// Les points sont ajoutés à l'utilisateur et la base de données est mise à jour.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur auquel les points doivent être ajoutés.</param>
        /// <param name="pointsToAdd">Nombre de points à ajouter à l'utilisateur.</param>
        /// <returns>Un code HTTP 200 si les points ont été ajoutés, ou un code d'erreur si l'utilisateur n'est pas trouvé.</returns>
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
