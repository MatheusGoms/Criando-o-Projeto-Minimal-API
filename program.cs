using VeiculosApi.Models;
using VeiculosApi.Services;
using VeiculosApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);
// JWT Key
var key = builder.Configuration["JwtKey"] ?? "chave-super-secreta-para-jwt";
// Serviços
builder.Services.AddSingleton<VeiculosRepository>();
builder.Services.AddSingleton(new JwtService(key));
// Autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => {
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT como: Bearer {seu token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } }
    });
});
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
// Endpoints
// Login e geração do token
app.MapPost("/login", (Usuario usuario, JwtService jwtService) =>
{
    // Exemplo: validação simples de usuário
    if (usuario.Nome == "admin" && usuario.Senha == "123456")
    {
        var token = jwtService.GerarToken(usuario.Nome, "admin"); // perfil admin
        return Results.Ok(new { token });
    }
    return Results.Unauthorized();
});
// Listar veículos (não exige autenticação)
app.MapGet("/veiculos", (VeiculosRepository repo) => repo.Listar());
// Adicionar veículo (apenas autenticado)
app.MapPost("/veiculos", [Microsoft.AspNetCore.Authorization.Authorize] (Veiculo veiculo, VeiculosRepository repo) =>
{
    repo.Adicionar(veiculo);
    return Results.Created($"/veiculos/{veiculo.Placa}", veiculo);
});
app.Run();
