using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioTurim.Dominio.Entidades;
namespace DesafioTurim.Dominio.Repositorios
{
    public interface IRelatorioRepositorio
    {
        public Task<List<DistribuicaoLucrosFuncionariosEntidade>> ListarDistribuicaoDeLucroPorFuncionario();
        public Task<List<FuncionarioEntidade>> ListarFuncionariosCadastratos();
        public Task<string> RetornaTotalDistribuido();
        public Task<string> RetornaTotalDisPonibilizado();
        public Task<string> RetornaSaldoDisponibilizadoDistribuido();
    }
}