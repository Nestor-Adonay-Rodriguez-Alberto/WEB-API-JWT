using API_RESTful.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Transferencia_Datos.Login_DTO;

namespace API_RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        // Representa La DB:
        private readonly MyDBcontext _MyDBcontext;

        private IConfiguration config;

        // Constructor:
        public SeguridadController(MyDBcontext myDBcontext, IConfiguration config)
        {
            _MyDBcontext = myDBcontext;
            this.config = config;
        }


        // RECIBE UN OBJETO Y LO GUARDA EN LA DB:
        [HttpPost("Autenticacion")]
        public async Task<IActionResult> Login([FromBody] IniciarSesion_DTO iniciarSesion_DTO)
        {
            Administrador? Objeto_Obtenido = await _MyDBcontext.Administradores.FirstOrDefaultAsync(x => x.Email == iniciarSesion_DTO.Email && x.Contraseña == iniciarSesion_DTO.Contraseña);

            if (Objeto_Obtenido == null)
            {
                return NotFound("¡Error!... Credenciales Incorrectas.");
            }

            // Generamos el Token:
            string JwtToken = Generar_Token(Objeto_Obtenido);

            return Ok(new { Token = JwtToken });
        }


        // Metodo Para Generear El Token:
        private string Generar_Token(Administrador administrador)
        {

            // Datos Del Administrador
            var Claims = new[]
            {
                new Claim(ClaimTypes.Name, administrador.Nombre),
                new Claim(ClaimTypes.Email, administrador.Email),
                new Claim("AdminType", administrador.Rol),
            };

            // Obtengon en Bytes la Key:
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));

            // Encriptando La Key:
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            // Token:
            var SecurityToken = new JwtSecurityToken(
                claims: Claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: Creds
                );


            // Cadena Token:
            string Token = new JwtSecurityTokenHandler().WriteToken(SecurityToken);

            return Token; 
        }

    }
}
