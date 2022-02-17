using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioTurim.Dominio.Entidades;
namespace DesafioTurim.Dominio.Repositorios
{
    public interface IAreaAtuacaoRepositorio
    {
        public Task<List<AreaAtuacaoEntidade>> ListarAreasdeAtuacao();
        public Task<AreaAtuacaoEntidade> BuscaAerasdeAtuacaoPorId(string id);
        public Task<List<AreaAtuacaoEntidade>> BuscaAerasdeAtuacaoPorDescricao(string desscricao);

    }
}