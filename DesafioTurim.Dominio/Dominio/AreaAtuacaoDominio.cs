using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioTurim.Dominio.Entidades;
using DesafioTurim.Dominio.Excepition;
using DesafioTurim.Dominio.Repositorios;

namespace DesafioTurim.Dominio.Dominio
{
    public class AreaAtuacaoDominio
    {
        IAreaAtuacaoRepositorio _areaAtuacaoRepositorio;
        public AreaAtuacaoDominio(IAreaAtuacaoRepositorio areaAtuacaoRepositorio)
        {
            _areaAtuacaoRepositorio = areaAtuacaoRepositorio;
        }
        public  Task<AreaAtuacaoEntidade> BuscaAerasdeAtuacaoPorId(string id)
        {
            if(id=="" || id ==null) throw new DominioException("O Id da área é obrigatório.");
            var retorno = _areaAtuacaoRepositorio.BuscaAerasdeAtuacaoPorId(id);
            if(retorno.Result.AreaAtuacao==null) throw new DominioException("Não foram encontrados registros para o Id Informado");
            return retorno;
        }
        public Task<List<AreaAtuacaoEntidade>> ListarAreasdeAtuacao()
        {
            return _areaAtuacaoRepositorio.ListarAreasdeAtuacao();
        }
    }
}