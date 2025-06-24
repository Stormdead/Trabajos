using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using S3_210028110_NET8.Servicios.Interfaces;
using S3_210028110_NET8.Servicios.Implementaciones;
using S3_210028110_NET8.Repositorios.Interfaces;
using S3_210028110_NET8.Repositorios.Implementaciones;
using S3_210028110_NET8.Seguridad;

var builder = WebApplication.CreateBuilder(args);

// Configuración de JWT desde appsettings.json
var configuracion = builder.Configuration;
var claveJwt = Encoding.ASCII.GetBytes(configuracion["Jwt:ClaveSecreta"]!);

// Agregar controladores
builder.Services.AddControllers();

// Configuración de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias de servicios y repositorios
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ITareaServicio, TareaServicio>();
builder.Services.AddScoped<ITareaRepositorio, TareaRepositorio>();
builder.Services.AddSingleton<TokenServicio>();

// Configuración de autenticación JWT
builder.Services.AddAuthentication(opciones =>
{
    opciones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opciones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opciones =>
{
    opciones.RequireHttpsMetadata = false;
    opciones.SaveToken = true;
    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(claveJwt),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = configuracion["Jwt:Emisor"],
        ValidAudience = configuracion["Jwt:Audiencia"]
    };
});

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
