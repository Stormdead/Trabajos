using S3_210028110_NET8.Modelos.DTOs;

namespace S3_210028110_NET8.Servicios.Interfaces
{
    public interface ITareaServicio
    {
        Task CrearTareaAsync(TareaCreadaDTO dto, int usuarioId);
        Task<IEnumerable<TareaRespuestaDTO>> ListarTareasAsync(int usuarioId);
        Task<TareaRespuestaDTO?> ObtenerTareaAsync(int id, int usuarioId);
        Task ActualizarTareaAsync(int id, TareaCreadaDTO dto, int usuarioId);
        Task EliminarTareaAsync(int id, int usuarioId);
    }
}
