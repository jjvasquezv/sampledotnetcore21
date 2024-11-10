using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PesoIdealWeb.Models;

namespace PesoIdealWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        // Inyección de dependencias de HttpClient
        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CalcularPesoIdeal(string sexo, int edad, int estatura)
        {
            // Construimos la URL de la API para llamar a PesoIdealApi
            var apiUrl = $"http://localhost:5000/api/pesoideal?sexo={sexo}&edad={edad}&estatura={estatura}";

            try
            {
                // Realizar la solicitud HTTP GET a la API de PesoIdeal
                var response = await _httpClient.GetStringAsync(apiUrl);

                // Deserializar la respuesta JSON a un objeto (por ejemplo, PesoIdealResult)
                var pesoIdealResult = JsonConvert.DeserializeObject<PesoIdealResult>(response);

                // Pasar el resultado a la vista
                return View("Resultado", pesoIdealResult);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y mostrar un mensaje de error
                ViewBag.Error = $"Hubo un error al intentar calcular el peso ideal: {ex.Message}";
                return View("Index");
            }
        }
    }
}
