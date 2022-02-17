using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioTurim.Dominio.Entidades;
using DesafioTurim.Dominio.Excepition;
using DesafioTurim.Dominio.Repositorios;


namespace DesafioTurim.Dominio.Dominio
{
    public class FuncionarioDominio
    {
        IFuncionarioRepositorio _funcionarioRepositorio;
        IAreaAtuacaoRepositorio _areaAtuacaoRepositorio;
        public FuncionarioDominio(IFuncionarioRepositorio funcionarioRepositorio, IAreaAtuacaoRepositorio areaAtuacaoRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _areaAtuacaoRepositorio = areaAtuacaoRepositorio;
        }
        public FuncionarioDominio(IFuncionarioRepositorio funcionarioRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }
        public Task<List<FuncionarioEntidade>> ListarFuncionariosRepositorio()
        {
            return _funcionarioRepositorio.ListarFuncionariosRepositorio();
        }
        public Task<FuncionarioEntidade> BuscarFuncionarioPorId(string id)
        {
            if(id=="" || id==null)  throw new DominioException("O Id do funcionario é obrigatório.");
            var retorno = _funcionarioRepositorio.BuscarFuncionarioPorId(id);
            if(retorno.Result.Nome==null) throw new DominioException("Não foram encontrados registros para o Id Informado");
            return retorno;
        }
        public bool InserirFuncionario(FuncionarioEntidade funcionario)
        {
            ValidaEntidade(funcionario);    
            _funcionarioRepositorio.InserirFuncionario(funcionario);

            return true;
        }
        public bool AlterarFuncionario(FuncionarioEntidade funcionario)
        {
             ValidaEntidade(funcionario);    
            _funcionarioRepositorio.AlterarFuncionario(funcionario);
            return true;
        }
        public bool ExcluirFuncionario(string id)
        {
            if(id=="" || id==null)  throw new DominioException("O Id do funcionario é obrigatório.");
               _funcionarioRepositorio.ExcluirFuncionario(id);
               
            return true;
        }
        private static void ValidaEntidade (FuncionarioEntidade funcionario)
        {
            decimal salario;
            DateTime dataAdminissao;
            if(funcionario==null) throw new DominioException("Favor preencher os dados do funcionário.");
            if(funcionario.Matricula=="") throw new DominioException("Favor preencher a matricula do funcionario.");
            if(funcionario.Nome=="") throw new DominioException("Favor preencher o nome do funcionario.");
            if(funcionario.Area=="") throw new DominioException("Favor preencher a área do funcionario.");
            if(funcionario.Cargo=="") throw new DominioException("Favor preencher o cargo do funcionario.");
            if(funcionario.Cargo!="Funcionário" && funcionario.Cargo!="Estagiário") throw new DominioException("Favor preencher o cargo do funcionario como Estagiário ou Funcionário.");
            if(funcionario.DataAdmissao=="") throw new DominioException("Favor preencher a data de admissão do funcionario.");
            if(funcionario.Salario=="") throw new DominioException("Favor preencher salario do funcionario.");
            var eSalario = decimal.TryParse(funcionario.Salario, out salario);
            if(! eSalario) throw new DominioException("Salário inválido.");
            if(Convert.ToDecimal(funcionario.Salario)<1000) throw new DominioException("O salário do funcionario não pode ser menor que R$ 1000.");            
            var eData = DateTime.TryParse(funcionario.DataAdmissao, out dataAdminissao);
            if(!eData) throw new DominioException("Data de admissão inválida.");
        }
        private void ValidaAreaAtuacao (string areaAtuacao)
        {
           var retornoareaAtuacao = _areaAtuacaoRepositorio.BuscaAerasdeAtuacaoPorDescricao(areaAtuacao);
           if(retornoareaAtuacao.Result.Count==0) throw new DominioException("Area de atuacação inválida, informe uma outra área de atuação");
        }
        public async void calculaDivisaoLucros()
        {
            try
            {
                var funcionarios = _funcionarioRepositorio.ListarFuncionariosRepositorio().Result;
                List<FuncionarioEntidade> listaDistribuicao = new List<FuncionarioEntidade>();
                // limpa a base para criar a nova distribuição de lucros.
                await _funcionarioRepositorio.zerarDistribuicaoDeLucrosParaOsFuncionarios();
                foreach(FuncionarioEntidade funcionario in funcionarios)
                {
                    //verifico se o funcionário é estagiário
                    var eEstagiario = funcionario.Cargo=="Estagiário" ?  true : false;
                    // retorno tempo de casa
                    var tempoDeCasa = CalculaTempoDeCasa(Convert.ToDateTime(funcionario.DataAdmissao));
                    // determino quantos salarios o funcionario ganha
                    var quantidadeDeSalarios = CalculaSalariosFuncionario(Convert.ToDecimal(funcionario.Salario));
                    // calcula a distribuição para o funcionario
                    var lucroDistribuidoParaoFuncionario = CalculaDistribuicaoDeLucros(Convert.ToDecimal(funcionario.Salario), 
                                                                                    funcionario.Area,
                                                                                    eEstagiario,
                                                                                    tempoDeCasa,
                                                                                    quantidadeDeSalarios);
                    DistribuicaoLucrosFuncionariosEntidade distribuicaoLucrosFuncionariosEntidade = new DistribuicaoLucrosFuncionariosEntidade()
                    {
                        Matricula = funcionario.Matricula,
                        Nome = funcionario.Nome,
                        ValorDistribuido = Convert.ToString(lucroDistribuidoParaoFuncionario)
                    };
                    // para cada funcionario, insere a nova distribuição de lucro
                    await _funcionarioRepositorio.AtualizaDistribuicaoDeLucrosParaOsFuncionarios(distribuicaoLucrosFuncionariosEntidade);
                }
            }
            catch (System.Exception)
            {
                throw;
            }      
        }
        private int CalculaTempoDeCasa (DateTime dataAdmnissao)
        {
            int tempo = DateTime.Now.Year - dataAdmnissao.Year;
            return tempo;
        }
        private int CalculaSalariosFuncionario (decimal salario)
        {
            var quantidadeSalarios = Math.Floor(salario / 1000);
            return Convert.ToInt32(quantidadeSalarios);
        }
        private decimal CalculaDistribuicaoDeLucros (decimal salario, string area, bool eEstagiario, int tempoDeCasa, int quantidadeDeSalarios)
        {
            //identifica o PAA: Peso por área de atuação
            int PAA  = 0;
            int PFS = 0;
            int PTA = 0;
            if (area == "Diretoria")
                PAA =1;
            if (area == "Contabilidade" || area == "Tecnologia" || area == "Financeiro")
                PAA =2;                
            if (area == "Serviços Gerais")
                PAA =3;
            if (area == "Relacionamento com o cliente")
                PAA =4;
           // Identifica o PFS: Peso por faixa salarial 
            if (quantidadeDeSalarios >=8)
                PFS = 5;
            if (quantidadeDeSalarios >5 && quantidadeDeSalarios < 8)
                PFS = 3;    
            if (quantidadeDeSalarios >3 && quantidadeDeSalarios < 5)
                PFS = 2;    
            if (quantidadeDeSalarios <3 || eEstagiario)
                PFS = 1;    
            // Identifica o PTA: Peso por tempo de admissão
            if (tempoDeCasa <=1)
                PTA = 1;
            if (tempoDeCasa >1 && tempoDeCasa <3)
                PTA = 2;    
            if (tempoDeCasa >=3 && tempoDeCasa < 8)
                PTA = 3;    
            if (tempoDeCasa >8)
                PTA = 4;        
            var resultadoCalculo = (((salario * PTA) + (salario * PAA))/(salario * PFS))*12;
            return resultadoCalculo;
        }
    }
}
