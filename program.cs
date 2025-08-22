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
