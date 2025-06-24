using System.ComponentModel.DataAnnotations;

public class UsuarioRegistroDTO
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Correo electrónico inválido")]
    public string CorreoElectronico { get; set; } = string.Empty;

    [Required]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string Contrasena { get; set; } = string.Empty;
}
