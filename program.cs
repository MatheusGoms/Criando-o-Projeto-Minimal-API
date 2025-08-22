var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Dados simulados
var veiculos = new List<Veiculo>();
app.MapGet("/veiculos", () => veiculos);
app.MapPost("/veiculos", (Veiculo veiculo) =>
{
    veiculos.Add(veiculo);
    return Results.Created($"/veiculos/{veiculo.Placa}", veiculo);
});
app.Run();
record Veiculo(string Placa, string Modelo, int Ano);


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var key = "chave-super-secreta-para-jwt"; // Use uma chave mais segura em produção

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
​ ​ ​ ​ .AddJwtBearer(options =>
​ ​ ​ ​ {
​ ​ ​ ​ ​ ​ ​ ​ options.TokenValidationParameters = new TokenValidationParameters
​ ​ ​ ​ ​ ​ ​ ​ {
​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ValidateIssuer = false,
​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ValidateAudience = false,
​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ValidateIssuerSigningKey = true,
​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
​ ​ ​ ​ ​ ​ ​ ​ };
​ ​ ​ ​ });

app.UseAuthentication();
app.UseAuthorization();

// Endpoint para login e geração do token
app.MapPost("/login", (Usuario usuario) =>
{
​ ​ ​ ​ if (usuario.Nome == "admin" && usuario.Senha == "123456")
​ ​ ​ ​ {
​ ​ ​ ​ ​ ​ ​ ​ var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
​ ​ ​ ​ ​ ​ ​ ​ var tokenDescriptor = new SecurityTokenDescriptor
​ ​ ​ ​ ​ ​ ​ ​ {
​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ SigningCredentials = new SigningCredentials(
​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ SecurityAlgorithms.HmacSha256Signature
​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ ​ )
​ ​ ​ ​ ​ ​ ​ ​ };
​ ​ ​ ​ ​ ​ ​ ​ var token = tokenHandler.CreateToken(tokenDescriptor);
​ ​ ​ ​ ​ ​ ​ ​ var tokenString = tokenHandler.WriteToken(token);
​ ​ ​ ​ ​ ​ ​ ​ return Results.Ok(new { token = tokenString });
​ ​ ​ ​ }
​ ​ ​ ​ return Results.Unauthorized();
});

record Usuario(string Nome, string Senha);
