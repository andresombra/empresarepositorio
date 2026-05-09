using Empresa.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Empresa.Api.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas a empresas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Controlador Auth.
        /// </summary>
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// GenerateToken.
        /// </summary>
        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] UserLogin userLogin)
        {
            if (userLogin.Username == "admin" && userLogin.Password == "password") // Exemplo simples
            {
                var jwtSettings = _configuration.GetSection("Jwt");
                var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

                if (key.Length < 32)
                    throw new Exception("A chave JWT deve ter pelo menos 32 bytes (256 bits).");

                var claims = new[]
                   {
                   new Claim(JwtRegisteredClaimNames.Sub, userLogin.Username),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
               };

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"]!)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }
    }
}
