using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CyberMind_API.Modeles;
using CyberMind_API.dbContext;
using CyberMind_API.DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace CyberMind_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageEnonceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImageEnonceController(AppDbContext context)
        {
            _context = context;
        }

        // Create a new ImageEnonce
        [HttpPost]
        public IActionResult CreateImageEnonce([FromBody] ImageEnonce newImageEnonce)
        {
            if (newImageEnonce == null)
            {
                return BadRequest("ImageEnonce is null.");
            }

            _context.ImageEnonces.Add(newImageEnonce);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetImageEnonceById), new { id = newImageEnonce.Id }, newImageEnonce);
        }

        // Get an ImageEnonce by Id
        [HttpGet("{id}")]
        public IActionResult GetImageEnonceById(uint id)
        {
            var imageEnonce = _context.ImageEnonces.FirstOrDefault(e => e.Id == id);
            if (imageEnonce == null)
            {
                return NotFound();
            }

            var imageEnonceDTO = new ImageEnonceDTO
            {
                Id = imageEnonce.Id,
                ChallengeId = imageEnonce.ChallengeId,
                Challenge = imageEnonce.Challenge,
                PathImage = imageEnonce.PathImage
            };

            return Ok(imageEnonceDTO);
        }

        // Update an ImageEnonce by Id
        [HttpPut("{id}")]
        public IActionResult UpdateImageEnonce(uint id, [FromBody] ImageEnonceDTO updatedImageEnonceDTO)
        {
            if (updatedImageEnonceDTO == null || updatedImageEnonceDTO.Id != id)
            {
                return BadRequest("ImageEnonceDTO is null or Id mismatch.");
            }

            var imageEnonce = _context.ImageEnonces.FirstOrDefault(e => e.Id == id);
            if (imageEnonce == null)
            {
                return NotFound();
            }

            imageEnonce.ChallengeId = updatedImageEnonceDTO.ChallengeId;
            imageEnonce.Challenge = updatedImageEnonceDTO.Challenge;
            imageEnonce.PathImage = updatedImageEnonceDTO.PathImage;

            _context.SaveChanges();

            return NoContent();
        }

        // Delete an ImageEnonce by Id
        [HttpDelete("{id}")]
        public IActionResult DeleteImageEnonce(uint id)
        {
            var imageEnonce = _context.ImageEnonces.FirstOrDefault(e => e.Id == id);
            if (imageEnonce == null)
            {
                return NotFound();
            }

            _context.ImageEnonces.Remove(imageEnonce);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
