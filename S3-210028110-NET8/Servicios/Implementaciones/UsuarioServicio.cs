using Microsoft.AspNetCore.Identity;
using S3_210028110_NET8.Modelos;
using S3_210028110_NET8.Modelos.DTOs;
using S3_210028110_NET8.Repositorios.Interfaces;
using S3_210028110_NET8.Servicios.Interfaces;
using S3_210028110_NET8.Seguridad;

namespace S3_210028110_NET8.Servicios.Implementaciones
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly TokenServicio _tokenServicio;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public UsuarioServicio(IUsuarioRepositorio repositorio, TokenServicio tokenServicio)
        {
            _repositorio = repositorio;
            _tokenServicio = tokenServicio;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public async Task<string> Registrar(UsuarioRegistroDTO dto)
        {
            var existente = await _repositorio.ObtenerPorCorreoAsync(dto.CorreoElectronico);
            if (existente != null)
                throw new ApplicationException("El correo electrónico ya está registrado.");

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                CorreoElectronico = dto.CorreoElectronico,
                Rol = "Usuario"
            };

            usuario.ContrasenaHash = _passwordHasher.HashPassword(usuario, dto.Contrasena);

            await _repositorio.CrearUsuarioAsync(usuario);

            return "Usuario registrado correctamente.";
        }

        public async Task<string?> Login(UsuarioLoginDTO dto)
        {
            var usuario = await _repositorio.ObtenerPorCorreoAsync(dto.CorreoElectronico);
            if (usuario == null)
                return null;

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.ContrasenaHash, dto.Contrasena);
            if (resultado == PasswordVerificationResult.Failed)
                return null;

            return _tokenServicio.GenerarToken(usuario);
        }
    }
}
