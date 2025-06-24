using S3_210028110_NET8.Modelos.DTOs;

namespace S3_210028110_NET8.Servicios.Interfaces
{
    public interface IUsuarioServicio
    {
        Task<string> Registrar(UsuarioRegistroDTO dto);
        Task<string?> Login(UsuarioLoginDTO dto);
    }
}
