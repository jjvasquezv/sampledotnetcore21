using Newtonsoft.Json;  // Importar Newtonsoft.Json
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;  // Para hacer la solicitud HTTP
using System.Threading.Tasks;  // Para trabajar con métodos asíncronos
using System.Text;  // Para convertir a cadena JSON

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;

    // Inyectamos HttpClient en el constructor
    public HomeController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Acción para mostrar el formulario de consulta de peso ideal
    public IActionResult Index()
    {
        return View();  // Devolvemos la vista, que tiene el formulario
    }

    // Acción para procesar la consulta de peso ideal
    [HttpPost]
    public async Task<IActionResult> GetPesoIdeal(int edad, double estatura, string sexo)
    {
        // Datos de entrada que enviamos a la API
        var consulta = new
        {
            Edad = edad,
            Estatura = estatura,
            Sexo = sexo
        };

        // Convertimos el objeto a JSON
        var jsonContent = JsonConvert.SerializeObject(consulta);

        // Creamos un objeto StringContent para la solicitud POST
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // URL de la API que hemos creado
        var apiUrl = "https://localhost:5001/api/PesoIdealApi";  // URL de la API

        // Realizamos la solicitud POST a la API
        var response = await _httpClient.PostAsync(apiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            // Si la respuesta es exitosa, obtenemos los resultados
            var apiResponse = await response.Content.ReadAsStringAsync();

            // Retornamos la respuesta de la API como JSON
            return Content(apiResponse, "application/json");
        }
        else
        {
            // Si la solicitud falla, retornamos un mensaje de error
            return Content("{\"error\": \"Hubo un problema al consultar la API.\"}", "application/json");
        }
    }
}
