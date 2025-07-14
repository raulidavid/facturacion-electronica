using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacturacionController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<FacturacionController> _logger;

        public FacturacionController(ILogger<FacturacionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // Dummy endpoint for electronic invoicing
        [HttpPost("facturacion-electronica")]
        public IActionResult FacturacionElectronica([FromBody] FacturaRequest request)
        {
            var response = new FacturaResponse
            {
                Folio = Guid.NewGuid().ToString(),
                Estatus = "Procesado",
                Mensaje = "Factura electrónica generada exitosamente (dummy)."
            };
            return Ok(response);
        }

        // Dummy endpoint: consulta factura
        [HttpGet("consulta-factura/{folio}")]
        public IActionResult ConsultaFactura(string folio)
        {
            var response = new FacturaResponse
            {
                Folio = folio,
                Estatus = "Encontrada",
                Mensaje = "Factura encontrada (dummy)."
            };
            return Ok(response);
        }

        // Dummy endpoint: cancela factura
        [HttpPost("cancela-factura")]
        public IActionResult CancelaFactura([FromBody] CancelacionRequest request)
        {
            var response = new CancelacionResponse
            {
                Folio = request.Folio,
                Estatus = "Cancelada",
                Mensaje = "Factura cancelada exitosamente (dummy)."
            };
            return Ok(response);
        }

        // Dummy endpoint: lista facturas
        [HttpGet("lista-facturas")]
        public IActionResult ListaFacturas()
        {
            var facturas = Enumerable.Range(1, 3).Select(i => new FacturaResponse
            {
                Folio = Guid.NewGuid().ToString(),
                Estatus = "Procesado",
                Mensaje = $"Factura {i} (dummy)."
            }).ToList();
            return Ok(facturas);
        }
    }

    public class FacturaRequest
    {
        public string RfcEmisor { get; set; }
        public string RfcReceptor { get; set; }
        public decimal Importe { get; set; }
        public string Concepto { get; set; }
    }

    public class FacturaResponse
    {
        public string Folio { get; set; }
        public string Estatus { get; set; }
        public string Mensaje { get; set; }
    }

    public class CancelacionRequest
    {
        public string Folio { get; set; }
    }

    public class CancelacionResponse
    {
        public string Folio { get; set; }
        public string Estatus { get; set; }
        public string Mensaje { get; set; }
    }
}
