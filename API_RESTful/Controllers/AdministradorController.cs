using API_RESTful.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Transferencia_Datos.Administrador_DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_RESTful.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        // Representa La DB:
        private readonly MyDBcontext _MyDBcontext;

        // Constructor:
        public AdministradorController(MyDBcontext myDBcontext)
        {
            _MyDBcontext = myDBcontext;
        }



        // OBTIENE TODOS LOS REGISTROS DE LA DB:
        [Authorize(Policy = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Administrador> Objetos_Obtenidos = await _MyDBcontext.Administradores.ToListAsync();

            ObtenerPorId_DTO Administradores = new ObtenerPorId_DTO();

            // Agregamos los registros obtenidos a la lista que mandaremos:
            foreach (Administrador administrador in Objetos_Obtenidos)
            {
                Administradores.Lista_Administradores.Add(new ObtenerPorId_DTO.Administrador
                {
                    IdAdmin = administrador.IdAdmin,
                    Nombre = administrador.Nombre,
                    Email = administrador.Email,
                    Contraseña=administrador.Contraseña,
                    Rol=administrador.Rol
                });

            }

            return Ok(Administradores);
        }


        // RECIBE UN OBJETO Y LO GUARDA EN LA DB:
        [Authorize(Policy = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Crear_Administrador_DTO crear_Administrador_DTO)
        {
            // Objeto a Guardar:
            Administrador administrador = new Administrador
            {
                Nombre = crear_Administrador_DTO.Nombre,
                Email = crear_Administrador_DTO.Email,
                Contraseña = crear_Administrador_DTO.Contraseña,
                Rol = crear_Administrador_DTO.Rol
            };

            _MyDBcontext.Add(administrador);
            await _MyDBcontext.SaveChangesAsync();

            return Ok("Guardado Correctamente");
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Administrador Objeto_Obtenido = await _MyDBcontext.Administradores.FirstOrDefaultAsync(x => x.IdAdmin == id);

            if (Objeto_Obtenido != null)
            {
                _MyDBcontext.Remove(Objeto_Obtenido);
                await _MyDBcontext.SaveChangesAsync();

                return Ok("Administrador Eliminado Corectamente.");
            }
            else
            {
                return NotFound("No Se Encontro El Registro.");
            }

        }


    }
}
