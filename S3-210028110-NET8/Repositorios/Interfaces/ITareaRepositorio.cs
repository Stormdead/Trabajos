using S3_210028110_NET8.Modelos;

namespace S3_210028110_NET8.Repositorios.Interfaces
{
    public interface ITareaRepositorio
    {
        Task CrearTareaAsync(Tarea tarea);
        Task<IEnumerable<Tarea>> ObtenerTareasPorUsuarioAsync(int usuarioId);
        Task<Tarea?> ObtenerPorIdAsync(int id, int usuarioId);
        Task ActualizarTareaAsync(Tarea tarea);
        Task EliminarTareaAsync(int id, int usuarioId);
    }
}
