using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S3_210028110_NET8.Modelos.DTOs;
using S3_210028110_NET8.Servicios.Interfaces;
using System.Security.Claims;

namespace S3_210028110_NET8.Controladores
{
    [ApiController]
    [Route("api/tareas")]
    [Authorize]
    public class TareasControlador : ControllerBase
    {
        private readonly ITareaServicio _servicio;

        public TareasControlador(ITareaServicio servicio)
        {
            _servicio = servicio;
        }

        private int ObtenerUsuarioId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] TareaCreadaDTO dto)
        {
            await _servicio.CrearTareaAsync(dto, ObtenerUsuarioId());
            return Ok(new { mensaje = "Tarea creada" });
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var tareas = await _servicio.ListarTareasAsync(ObtenerUsuarioId());
            return Ok(tareas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var tarea = await _servicio.ObtenerTareaAsync(id, ObtenerUsuarioId());
            return tarea == null ? NotFound() : Ok(tarea);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] TareaCreadaDTO dto)
        {
            await _servicio.ActualizarTareaAsync(id, dto, ObtenerUsuarioId());
            return Ok(new { mensaje = "Tarea actualizada" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _servicio.EliminarTareaAsync(id, ObtenerUsuarioId());
            return Ok(new { mensaje = "Tarea eliminada" });
        }
    }
}
