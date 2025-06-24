using S3_210028110_NET8.Modelos;

namespace S3_210028110_NET8.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task CrearUsuarioAsync(Usuario usuario);
        Task<Usuario?> ObtenerPorCorreoAsync(string correo);
    }
}
