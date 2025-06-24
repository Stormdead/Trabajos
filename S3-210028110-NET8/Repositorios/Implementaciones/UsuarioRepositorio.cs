using Npgsql;
using S3_210028110_NET8.Modelos;
using S3_210028110_NET8.Repositorios.Interfaces;

namespace S3_210028110_NET8.Repositorios.Implementaciones
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _conexion;

        public UsuarioRepositorio(IConfiguration configuracion)
        {
            _conexion = configuracion.GetConnectionString("PostgresConexion")!;
        }

        public async Task CrearUsuarioAsync(Usuario usuario)
        {
            using var conn = new NpgsqlConnection(_conexion);
            await conn.OpenAsync();

            var query = @"INSERT INTO Usuarios (Nombre, CorreoElectronico, ContrasenaHash, Rol)
                          VALUES (@nombre, @correo, @hash, @rol)";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("correo", usuario.CorreoElectronico);
            cmd.Parameters.AddWithValue("hash", usuario.ContrasenaHash);
            cmd.Parameters.AddWithValue("rol", usuario.Rol);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<Usuario?> ObtenerPorCorreoAsync(string correo)
        {
            using var conn = new NpgsqlConnection(_conexion);
            await conn.OpenAsync();

            var query = "SELECT * FROM Usuarios WHERE CorreoElectronico = @correo";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("correo", correo);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Usuario
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    CorreoElectronico = reader.GetString(2),
                    ContrasenaHash = reader.GetString(3),
                    Rol = reader.GetString(4)
                };
            }

            return null;
        }
    }
}
