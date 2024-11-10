using Microsoft.AspNetCore.Mvc;

namespace PesoIdealAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesoIdealController : ControllerBase
    {
        // POST api/pesoideal
        [HttpPost]
        public ActionResult<double> CalcularPesoIdeal([FromBody] PesoRequest request)
        {
            if (request == null)
            {
                return BadRequest("Datos inválidos.");
            }

            double pesoIdeal = 0;

            // Fórmulas simplificadas de cálculo de peso ideal
            if (request.Sexo.ToLower() == "masculino")
            {
                pesoIdeal = 50 + 0.91 * (request.Estatura - 152);
            }
            else if (request.Sexo.ToLower() == "femenino")
            {
                pesoIdeal = 45.5 + 0.91 * (request.Estatura - 152);
            }

            return Ok(pesoIdeal);
        }
    }

    public class PesoRequest
    {
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public double Estatura { get; set; }
    }
}
