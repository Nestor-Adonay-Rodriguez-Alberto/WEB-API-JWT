using API_RESTful.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transferencia_Datos.Producto_DTO;


namespace API_RESTful.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        // Representa La DB:
        private readonly MyDBcontext _MyDBcontext;

        // Constructor:
        public ProductoController(MyDBcontext myDBcontext)
        {
            _MyDBcontext = myDBcontext;
        }



        // OBTIENE TODOS LOS REGISTROS DE LA DB:
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Producto> Objetos_Obtenidos = await _MyDBcontext.Productos.ToListAsync();

            List<ObtenerPorId_DTO> Lista_Productos = new List<ObtenerPorId_DTO>();

            // Agregamos los registros obtenidos a la lista que mandaremos:
            foreach(Producto producto in Objetos_Obtenidos)
            {
                Lista_Productos.Add(new ObtenerPorId_DTO 
                {
                    IdProducto= producto.IdProducto,
                    Nombre=producto.Nombre,
                    Precio=producto.Precio
                });
            }

            return Ok(Lista_Productos);
        }


        // RECIBE UN OBJETO Y LO GUARDA EN LA DB:
        [Authorize(Policy = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Crear_Producto_DTO crear_Producto_DTO)
        {
            // Objeto a Guardar:
            Producto producto = new Producto 
            {
                Nombre= crear_Producto_DTO.Nombre,
                Precio=crear_Producto_DTO.Precio
            };

            _MyDBcontext.Add(producto);
            await _MyDBcontext.SaveChangesAsync();

            return Ok("Guardado Correctamente");
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Producto Objeto_Obtenido = await _MyDBcontext.Productos.FirstOrDefaultAsync(x => x.IdProducto == id);

            if(Objeto_Obtenido!=null)
            {
                _MyDBcontext.Remove(Objeto_Obtenido);
                await _MyDBcontext.SaveChangesAsync();

                return Ok("Producto Eliminado Corectamente.");
            }
            else
            {
                return NotFound("No Se Encontro El Registro.");
            }

        }


    }
}
