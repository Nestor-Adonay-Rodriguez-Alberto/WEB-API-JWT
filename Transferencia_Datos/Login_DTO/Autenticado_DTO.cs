using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transferencia_Datos.Login_DTO
{
    public class Autenticado_DTO
    {
        // ATRIBUTOS:
        public int IdAdmin { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public string Contraseña { get; set; }

        public string Rol { get; set; }

        public string Token { get; set; }
    }
}
