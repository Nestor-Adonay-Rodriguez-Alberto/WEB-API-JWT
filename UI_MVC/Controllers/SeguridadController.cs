using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Transferencia_Datos.Login_DTO;

namespace UI_MVC.Controllers
{
    public class SeguridadController : Controller
    {
        // Para Hacer Solicitudes Al Servidor:
        private readonly HttpClient _HttpClient;


        // Constructor:
        public SeguridadController(IHttpClientFactory httpClientFactory)
        {
            _HttpClient = httpClientFactory.CreateClient("API_RESTful");
        }

        // Me Manda A La Vista
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(IniciarSesion_DTO iniciarSesion_DTO, string ReturnUrl)
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenidos = await _HttpClient.PostAsJsonAsync("/api/Seguridad/Autenticacion", iniciarSesion_DTO);

            // OBJETO:
            Autenticado_DTO? Objeto_Obtenido = new Autenticado_DTO();

            // True=200-299
            if (JSON_Obtenidos.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Objeto_Obtenido = await JSON_Obtenidos.Content.ReadFromJsonAsync<Autenticado_DTO>();
            }
            else
            {
                ViewBag.Error = "¡Error!... Credenciales Incorrectas.";
                return View(iniciarSesion_DTO);

            }



            if (Objeto_Obtenido != null)
            {
                var claims = new[] {
                    new Claim(ClaimTypes.Name, Objeto_Obtenido.Nombre),
                    new Claim(ClaimTypes.Role, Objeto_Obtenido.Rol),
                    new Claim("TokenSeguro", Objeto_Obtenido.Token)
                    };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true }); ;

                var result = User.Identity.IsAuthenticated;

                if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    return Redirect(ReturnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "¡Error!... Credenciales Incorrectas.";
            }

            ViewBag.pReturnUrl = ReturnUrl;

            return View(Objeto_Obtenido);
        }

    }
}
