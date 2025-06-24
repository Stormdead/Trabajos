using Npgsql;
using S3_210028110_NET8.Modelos;
using S3_210028110_NET8.Repositorios.Interfaces;

namespace S3_210028110_NET8.Repositorios.Implementaciones
{
    public class TareaRepositorio : ITareaRepositorio
    {
        private readonly string _conexion;

        public TareaRepositorio(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("Postgres")!;
        }

        public async Task CrearTareaAsync(Tarea tarea)
        {
            using var conn = new NpgsqlConnection(_conexion);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT InsertarTarea(@titulo, @descripcion, @fecha, @usuarioId);", conn);
            cmd.Parameters.AddWithValue("titulo", tarea.Titulo);
            cmd.Parameters.AddWithValue("descripcion", (object?)tarea.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fecha", tarea.FechaVencimiento);
            cmd.Parameters.AddWithValue("usuarioId", tarea.UsuarioId);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Tarea>> ObtenerTareasPorUsuarioAsync(int usuarioId)
        {
            var tareas = new List<Tarea>();
            using var conn = new NpgsqlConnection(_conexion);
            await conn.OpenAsync();

            var query = "SELECT * FROM Tareas WHERE UsuarioId = @usuarioId";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("usuarioId", usuarioId);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                tareas.Add(new Tarea
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                    FechaVencimiento = reader.GetDateTime(3),
                    Completada = reader.GetBoolean(4),
                    UsuarioId = reader.GetInt32(5)
                });
            }

            return tareas;
        }

        public async Task<Tarea?> ObtenerPorIdAsync(int id, int usuarioId)
        {
            using var conn = new NpgsqlConnection(_conexion);
            await conn.OpenAsync();

            var query = "SELECT * FROM Tareas WHERE Id = @id AND UsuarioId = @usuarioId";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("usuarioId", usuarioId);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Tarea
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                    FechaVencimiento = reader.GetDateTime(3),
                    Completada = reader.GetBoolean(4),
                    UsuarioId = reader.GetInt32(5)
                };
            }

            return null;
        }

        public async Task ActualizarTareaAsync(Tarea tarea)
        {
            using var conn = new NpgsqlConnection(_conexion);
            await conn.OpenAsync();

            var query = @"UPDATE Tareas SET 
                            Titulo = @titulo, 
                            Descripcion = @descripcion, 
                            FechaVencimiento = @fecha,
                            Completada = @completada
                          WHERE Id = @id AND UsuarioId = @usuarioId";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("titulo", tarea.Titulo);
            cmd.Parameters.AddWithValue("descripcion", (object?)tarea.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("fecha", tarea.FechaVencimiento);
            cmd.Parameters.AddWithValue("completada", tarea.Completada);
            cmd.Parameters.AddWithValue("id", tarea.Id);
            cmd.Parameters.AddWithValue("usuarioId", tarea.UsuarioId);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EliminarTareaAsync(int id, int usuarioId)
        {
            using var conn = new NpgsqlConnection(_conexion);
            await conn.OpenAsync();

            var query = "DELETE FROM Tareas WHERE Id = @id AND UsuarioId = @usuarioId";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("usuarioId", usuarioId);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
