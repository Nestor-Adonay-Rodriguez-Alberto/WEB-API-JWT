using System.ComponentModel.DataAnnotations;


namespace Transferencia_Datos.Administrador_DTO
{
    public class Editar_Administrador_DTO
    {
        // ATRIBUTOS:
        [Required]
        public int IdAdmin { get; set; }


        [Required(ErrorMessage = "Ingrese El Nombre Del Administrador")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Ingrese Un Email")]
        [EmailAddress(ErrorMessage = "Esto no es un Email Valido.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Ingrese Una Contraseña")]
        public string Contraseña { get; set; }


        [Required(ErrorMessage = "Ingrese El Tipo De Administrador")]
        public string Rol { get; set; }

    }
}
