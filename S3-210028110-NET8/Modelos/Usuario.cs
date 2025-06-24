namespace S3_210028110_NET8.Modelos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public string ContrasenaHash { get; set; } = null!;
        public string Rol { get; set; } = "Usuario";
    }
}
