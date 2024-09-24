using System.ComponentModel.DataAnnotations;


namespace Transferencia_Datos.Producto_DTO
{
    public class Crear_Producto_DTO
    {
        // ATRIBUTOS:
        [Required(ErrorMessage ="Nombre del Producto Obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Precio del Producto Obligatorio")]
        public decimal Precio { get; set; }

    }
}
