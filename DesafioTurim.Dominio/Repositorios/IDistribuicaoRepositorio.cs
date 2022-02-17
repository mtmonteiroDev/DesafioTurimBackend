using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioTurim.Dominio.Entidades;
namespace DesafioTurim.Dominio.Repositorios
{
    public interface IDistribuicaoRepositorio
    {
         public Task<List<DistribuicaoEntidade>> BuscarValorDistribuicao();
         void AtualizaValorDistrinuicao(DistribuicaoEntidade distribuicaoEntidade);
    }
}