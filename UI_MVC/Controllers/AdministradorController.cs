using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using Transferencia_Datos.Administrador_DTO;

namespace UI_MVC.Controllers
{
    [Authorize]
    public class AdministradorController : Controller
    {
        // Para Hacer Solicitudes Al Servidor:
        private readonly HttpClient _HttpClient;


        // Constructor:
        public AdministradorController(IHttpClientFactory httpClientFactory)
        {
            _HttpClient = httpClientFactory.CreateClient("API_RESTful");
        }


        // GET: AdministradorController
        public async Task<IActionResult> Index()
        {
            // Token Obtenido al Iniciar Sesion:
            string TokenSeguro = User.FindFirstValue("TokenSeguro");

            // Agrego el Token a la Peticion:
            _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",TokenSeguro);

            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenidos = await _HttpClient.GetAsync("/api/Administrador");

            // OBJETO:
            ObtenerPorId_DTO Lista_Administradores = new ObtenerPorId_DTO();
            // True=200-299
            if (JSON_Obtenidos.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Lista_Administradores = await JSON_Obtenidos.Content.ReadFromJsonAsync<ObtenerPorId_DTO>();
            }


            return View(Lista_Administradores);
        }


        // GET: AdministradorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdministradorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: AdministradorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdministradorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
