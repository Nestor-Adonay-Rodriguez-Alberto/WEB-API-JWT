using System.ComponentModel.DataAnnotations;


namespace Transferencia_Datos.Login_DTO
{
    public class IniciarSesion_DTO
    {
        // ATRIBUTOS:
        [Required(ErrorMessage = "Ingrese Un Email")]
        [EmailAddress(ErrorMessage = "Esto no es un Email Valido.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Ingrese Una Contraseña")]
        public string Contraseña { get; set; }

    }
}
