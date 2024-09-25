

namespace Transferencia_Datos.Administrador_DTO
{
    public class ObtenerPorId_DTO
    {
        // ATRIBUTOS:
        public class Administrador
        {
            public int IdAdmin { get; set; }

            public string Nombre { get; set; }

            public string Email { get; set; }

            public string Contraseña { get; set; }

            public string Rol { get; set; }
        }

        public List<Administrador> Lista_Administradores { get; set; }

        public ObtenerPorId_DTO()
        {
            Lista_Administradores = new List<Administrador>();
        }
    }
}
