using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioTurim.Dominio.Entidades;
using DesafioTurim.Dominio.Excepition;
using DesafioTurim.Dominio.Repositorios;

namespace DesafioTurim.Dominio.Dominio
{
    public class DistribuicaoDominio
    {
        IDistribuicaoRepositorio _distribuicaoRepositorio;
        public DistribuicaoDominio(IDistribuicaoRepositorio distribuicaoRepositorio)
        {
            _distribuicaoRepositorio = distribuicaoRepositorio;
        }
        public Task<List<DistribuicaoEntidade>> BuscarValorDistribuicao()
        {
            var retorno =  _distribuicaoRepositorio.BuscarValorDistribuicao();
            if(retorno.Result==null) throw new DominioException("Não existe valor para ser distrinuido para os funcionários");
            return retorno;
        }
        public void AtualizaValorDistrinuicao(DistribuicaoEntidade distribuicaoEntidade)
        {
           if(distribuicaoEntidade.ValorDistribuir=="" || distribuicaoEntidade.ValorDistribuir==null)
              throw new DominioException("O valor de distribução informado não pode ser vazio");
           if(Convert.ToDecimal(distribuicaoEntidade.ValorDistribuir)<=0)
              throw new DominioException("O valor de distribução informado não pode menor ou igual a zero");   
           _distribuicaoRepositorio.AtualizaValorDistrinuicao(distribuicaoEntidade);
        }
    }
}