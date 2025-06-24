using S3_210028110_NET8.Modelos;
using S3_210028110_NET8.Modelos.DTOs;
using S3_210028110_NET8.Repositorios.Interfaces;
using S3_210028110_NET8.Servicios.Interfaces;

namespace S3_210028110_NET8.Servicios.Implementaciones
{
    public class TareaServicio : ITareaServicio
    {
        private readonly ITareaRepositorio _repositorio;

        public TareaServicio(ITareaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task CrearTareaAsync(TareaCreadaDTO dto, int usuarioId)
        {
            var tarea = new Tarea
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                FechaVencimiento = dto.FechaVencimiento,
                UsuarioId = usuarioId
            };

            await _repositorio.CrearTareaAsync(tarea);
        }

        public async Task<IEnumerable<TareaRespuestaDTO>> ListarTareasAsync(int usuarioId)
        {
            var tareas = await _repositorio.ObtenerTareasPorUsuarioAsync(usuarioId);

            return tareas.Select(t => new TareaRespuestaDTO
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                FechaVencimiento = t.FechaVencimiento,
                Completada = t.Completada
            });
        }

        public async Task<TareaRespuestaDTO?> ObtenerTareaAsync(int id, int usuarioId)
        {
            var tarea = await _repositorio.ObtenerPorIdAsync(id, usuarioId);
            if (tarea == null) return null;

            return new TareaRespuestaDTO
            {
                Id = tarea.Id,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                FechaVencimiento = tarea.FechaVencimiento,
                Completada = tarea.Completada
            };
        }

        public async Task ActualizarTareaAsync(int id, TareaCreadaDTO dto, int usuarioId)
        {
            var tarea = await _repositorio.ObtenerPorIdAsync(id, usuarioId);
            if (tarea == null) throw new Exception("Tarea no encontrada");

            tarea.Titulo = dto.Titulo;
            tarea.Descripcion = dto.Descripcion;
            tarea.FechaVencimiento = dto.FechaVencimiento;

            await _repositorio.ActualizarTareaAsync(tarea);
        }

        public async Task EliminarTareaAsync(int id, int usuarioId)
        {
            await _repositorio.EliminarTareaAsync(id, usuarioId);
        }
    }
}
