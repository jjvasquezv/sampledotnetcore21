using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PesoIdealWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController()
        {
            _httpClient = new HttpClient();
        }

        // Vista principal
        public IActionResult Index()
        {
            return View();
        }

        // Acción para enviar los datos a la API
        [HttpPost]
        public async Task<IActionResult> CalcularPesoIdeal(string sexo, int edad, double estatura)
        {
            var request = new
            {
                Sexo = sexo,
                Edad = edad,
                Estatura = estatura
            };

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5000/api/pesoideal", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var pesoIdeal = await response.Content.ReadAsStringAsync();
                ViewBag.PesoIdeal = pesoIdeal;
            }
            else
            {
                ViewBag.PesoIdeal = "Error al calcular el peso ideal";
            }

            return View("Index");
        }
    }
}
