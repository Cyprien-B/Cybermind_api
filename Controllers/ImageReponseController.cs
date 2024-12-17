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
    public class ImageReponseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImageReponseController(AppDbContext context)
        {
            _context = context;
        }

        // Create a new ImageReponse
        [HttpPost]
        public IActionResult CreateImageReponse([FromBody] ImageReponse newImageReponse)
        {
            if (newImageReponse == null)
            {
                return BadRequest("ImageReponse is null.");
            }

            _context.ImageReponses.Add(newImageReponse);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetImageReponseById), new { id = newImageReponse.Id }, newImageReponse);
        }

        // Get an ImageReponse by Id
        [HttpGet("{id}")]
        public IActionResult GetImageReponseById(uint id)
        {
            var imageReponse = _context.ImageReponses.FirstOrDefault(e => e.Id == id);
            if (imageReponse == null)
            {
                return NotFound();
            }

            var imageReponseDTO = new ImageReponseDTO
            {
                Id = imageReponse.Id,
                PathImage = imageReponse.PathImage
            };

            return Ok(imageReponseDTO);
        }

        // Update an ImageReponse by Id
        [HttpPut("{id}")]
        public IActionResult UpdateImageReponse(uint id, [FromBody] ImageReponseDTO updatedImageReponseDTO)
        {
            if (updatedImageReponseDTO == null || updatedImageReponseDTO.Id != id)
            {
                return BadRequest("ImageReponseDTO is null or Id mismatch.");
            }

            var imageReponse = _context.ImageReponses.FirstOrDefault(e => e.Id == id);
            if (imageReponse == null)
            {
                return NotFound();
            }

            imageReponse.PathImage = updatedImageReponseDTO.PathImage;

            _context.SaveChanges();

            return NoContent();
        }

        // Delete an ImageReponse by Id
        [HttpDelete("{id}")]
        public IActionResult DeleteImageReponse(uint id)
        {
            var imageReponse = _context.ImageReponses.FirstOrDefault(e => e.Id == id);
            if (imageReponse == null)
            {
                return NotFound();
            }

            _context.ImageReponses.Remove(imageReponse);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
