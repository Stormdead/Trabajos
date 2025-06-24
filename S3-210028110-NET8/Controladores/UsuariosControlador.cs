using Microsoft.AspNetCore.Mvc;
using S3_210028110_NET8.Modelos.DTOs;
using S3_210028110_NET8.Servicios.Interfaces;

namespace S3_210028110_NET8.Controladores;

[ApiController]
[Route("api/usuarios")]
public class UsuariosControlador : ControllerBase
{
    private readonly IUsuarioServicio _usuarioServicio;

    public UsuariosControlador(IUsuarioServicio usuarioServicio)
    {
        _usuarioServicio = usuarioServicio;
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDTO dto)
    {   
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
        try
        {
            var resultado = await _usuarioServicio.Registrar(dto);
            return Ok(new { mensaje = resultado });
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
    {
        var token = await _usuarioServicio.Login(dto);
        if (token == null)
            return Unauthorized(new { mensaje = "Credenciales inválidas" });

        return Ok(new { token });
    }
}
