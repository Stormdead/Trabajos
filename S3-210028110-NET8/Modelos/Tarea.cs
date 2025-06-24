namespace S3_210028110_NET8.Modelos
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Completada { get; set; }
        public int UsuarioId { get; set; }
    }
}
