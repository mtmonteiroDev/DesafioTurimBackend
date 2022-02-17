using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioTurim.Dominio.Entidades;
namespace DesafioTurim.Dominio.Repositorios
{
    public interface IFuncionarioRepositorio
    {
        public Task<List<FuncionarioEntidade>> ListarFuncionariosRepositorio();
        public Task<FuncionarioEntidade> BuscarFuncionarioPorId(string id);
        public void InserirFuncionario(FuncionarioEntidade funcionario);
        public void AlterarFuncionario(FuncionarioEntidade funcionario);
        public void ExcluirFuncionario(string id);
        public Task AtualizaDistribuicaoDeLucrosParaOsFuncionarios(DistribuicaoLucrosFuncionariosEntidade distribuicaoLucrosFuncionariosEntidade);
        public Task zerarDistribuicaoDeLucrosParaOsFuncionarios();
    }
}