using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers;

[ApiController]
[Route("api/Servicios")]
public class MobiliarioController : ControllerBase
{
    // Lista estática para que los datos persistan en memoria
    private static readonly List<MobiliarioDto> _inventario = new()
    {
        new MobiliarioDto { Id = 1, Codigo = "SI-001", Tipo = "Silla Tiffany", Descripcion = "Color Blanco (Resina)", Ubicacion = "Bodega Principal", Estado = "Excelente" },
        new MobiliarioDto { Id = 2, Codigo = "ME-010", Tipo = "Mesa Redonda", Descripcion = "1.50m para 10 personas", Ubicacion = "Bodega Principal", Estado = "Bueno" },
        new MobiliarioDto { Id = 3, Codigo = "MA-005", Tipo = "Mantel Blanco", Descripcion = "Redondo Liso", Ubicacion = "Lavandería", Estado = "En Limpieza" },
        new MobiliarioDto { Id = 4, Codigo = "SI-P-002", Tipo = "Silla Plegable Negra", Descripcion = "Acojinada", Ubicacion = "Evento (Salida)", Estado = "Regular" },
        new MobiliarioDto { Id = 5, Codigo = "CA-3X3-01", Tipo = "Carpa Toldo 3x3m", Descripcion = "Impermeable", Ubicacion = "Bodega Externa", Estado = "Mantenimiento" }
    };

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_inventario);
    }

    // Estructura para recibir datos nuevos (POST)
    public class CrearMobiliarioRequest
    {
        public string? Codigo { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion { get; set; }
        public string? Ubicacion { get; set; }
        public string? Estado { get; set; }
    }

    [HttpPost]
    public IActionResult Post([FromBody] CrearMobiliarioRequest req)
    {
        // Validación de campos obligatorios
        if (req == null || string.IsNullOrWhiteSpace(req.Codigo) || string.IsNullOrWhiteSpace(req.Tipo))
            return BadRequest("El código y el tipo son obligatorios.");

        // Generación automática del ID
        var nuevoId = _inventario.Count == 0 ? 1 : _inventario.Max(m => m.Id) + 1;

        var nuevo = new MobiliarioDto
        {
            Id = nuevoId,
            Codigo = req.Codigo.Trim(),
            Tipo = req.Tipo.Trim(),
            Descripcion = req.Descripcion?.Trim() ?? "",
            Ubicacion = req.Ubicacion?.Trim() ?? "",
            Estado = req.Estado?.Trim() ?? "Excelente"
        };

        _inventario.Add(nuevo);
        return Ok(nuevo);
    }

    // Estructura para actualizar (PUT)
    public class ActualizarMobiliarioRequest
    {
        public string? Codigo { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion { get; set; }
        public string? Ubicacion { get; set; }
        public string? Estado { get; set; }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ActualizarMobiliarioRequest req)
    {
        var existente = _inventario.FirstOrDefault(m => m.Id == id);
        if (existente == null) return NotFound("Mobiliario no encontrado.");

        if (req == null) return BadRequest("Datos insuficientes.");

        existente.Codigo = req.Codigo?.Trim() ?? existente.Codigo;
        existente.Tipo = req.Tipo?.Trim() ?? existente.Tipo;
        existente.Descripcion = req.Descripcion?.Trim() ?? existente.Descripcion;
        existente.Ubicacion = req.Ubicacion?.Trim() ?? existente.Ubicacion;
        existente.Estado = req.Estado?.Trim() ?? existente.Estado;

        return Ok(existente);
    }

    // Clase DTO para representar el objeto
    public class MobiliarioDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = "";
        public string Tipo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Ubicacion { get; set; } = "";
        public string Estado { get; set; } = "";
    }
}