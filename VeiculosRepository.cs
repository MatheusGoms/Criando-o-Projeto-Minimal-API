using VeiculosApi.Models;
using System.Collections.Concurrent;
namespace VeiculosApi.Data
{
    public class VeiculosRepository
    {
        private readonly ConcurrentDictionary<string, Veiculo> _veiculos = new();
        public IEnumerable<Veiculo> Listar() => _veiculos.Values;
        public void Adicionar(Veiculo veiculo)
        {
            _veiculos[veiculo.Placa] = veiculo;
        }
    }
}
