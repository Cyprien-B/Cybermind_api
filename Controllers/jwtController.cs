using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CyberMind_API.Modeles;
using CyberMind_API.dbContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtOptions _jwtOptions;
    private readonly AppDbContext _context;

    public AuthController(IOptions<JwtOptions> jwtOptions, AppDbContext context)
    {
        _jwtOptions = jwtOptions.Value;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login([FromBody] LoginRequest login)
    {
        var account = await _context.Users.FirstOrDefaultAsync(e => e.Mail == login.Mail);
        if (account == null)
        {
            return BadRequest("false account or wrong password");
        }

        bool verified = BCrypt.Net.BCrypt.Verify(login.Password, account.Password);

        if (verified)
        {
            var token = GenerateJwtToken(login.Mail);
            return Ok(new { Token = token });
        }
        else
        {
            return BadRequest("false account or wrong password");
        }
    }

    private string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public class LoginRequest
{
    public required string Mail { get; set; }
    public required string Password { get; set; }
}
