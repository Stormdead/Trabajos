namespace S3_210028110_NET8.Modelos.DTOs
{
    public class TareaCreadaDTO
    {
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
