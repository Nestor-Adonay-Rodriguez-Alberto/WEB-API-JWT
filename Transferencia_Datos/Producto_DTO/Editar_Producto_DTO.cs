using System.ComponentModel.DataAnnotations;


namespace Transferencia_Datos.Producto_DTO
{
    public class Editar_Producto_DTO
    {
        // ATRIBUTOS:
        [Required]
        public int IdProducto { get; set; }


        [Required(ErrorMessage = "Nombre del Producto Obligatorio")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Precio del Producto Obligatorio")]
        public decimal Precio { get; set; }

    }
}
