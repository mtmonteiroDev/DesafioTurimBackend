using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioTurim.Dominio.Entidades;
using DesafioTurim.Dominio.Repositorios;

namespace DesafioTurim.Dominio.Dominio
{
    public class RelatorioDominio
    {
        IRelatorioRepositorio _relatorioRepositorio;
        public RelatorioDominio(IRelatorioRepositorio relatorioRepositorio)
        {
            _relatorioRepositorio = relatorioRepositorio;
        }
        public Task<List<DistribuicaoLucrosFuncionariosEntidade>> ListarDistribuicaoDeLucroPorFuncionario()
        {
            return _relatorioRepositorio.ListarDistribuicaoDeLucroPorFuncionario();
        }
        public Task<List<FuncionarioEntidade>> ListarFuncionariosCadastratos()
        {
            return _relatorioRepositorio.ListarFuncionariosCadastratos();
        }
        public Task<string> RetornaSaldoDisponibilizadoDistribuido()
        {
            return _relatorioRepositorio.RetornaSaldoDisponibilizadoDistribuido();
        }
        public Task<string> RetornaTotalDisPonibilizado()
        {
            return _relatorioRepositorio.RetornaTotalDisPonibilizado();
        }
        public Task<string> RetornaTotalDistribuido()
        {
            return _relatorioRepositorio.RetornaTotalDistribuido();
        }
    }
}