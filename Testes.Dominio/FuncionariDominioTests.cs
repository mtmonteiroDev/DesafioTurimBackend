using System;
using DesafioTurim.Dominio.Dominio;
using DesafioTurim.Dominio.Entidades;
using DesafioTurim.Dominio.Excepition;
using DesafioTurim.Dominio.Repositorios;
using Moq;
using Xunit;

namespace Testes.Dominio
{
    public class FuncionariDominioTests
    {
       private FuncionarioDominio funcionarioDominio;

       public FuncionariDominioTests()
       {
           funcionarioDominio = new FuncionarioDominio(new Mock<IFuncionarioRepositorio>().Object);
       } 
       [Fact]
       public void InserirFuncionarioSemMatricula()
       {
                var exception = Assert.Throws<DominioException>(()=> funcionarioDominio.InserirFuncionario(new FuncionarioEntidade()
                {
                        FuncionairoId="x",
                        Matricula="",
                        Nome ="Marcos",
                        Area="Diretoria",
                        Cargo="Funcionário",
                        Salario="1001",
                        DataAdmissao="10/10/2021"
                }));

                Assert.Equal("Favor preencher a matricula do funcionario.", exception.Message);

       }
       [Fact]
       public void InserirFuncionarioSemNome()
       {
              var exception = Assert.Throws<DominioException>(()=> funcionarioDominio.InserirFuncionario(new FuncionarioEntidade()
                {
                        FuncionairoId="x",
                        Matricula="1",
                        Nome ="",
                        Area="Diretoria",
                        Cargo="Funcionário",
                        Salario="1001",
                        DataAdmissao="10/10/2021"
                }));

                Assert.Equal("Favor preencher o nome do funcionario.", exception.Message);
       }
       [Fact]
       public void InserirFuncionarioSemArea()
       {
              var exception = Assert.Throws<DominioException>(()=> funcionarioDominio.InserirFuncionario(new FuncionarioEntidade()
                {
                        FuncionairoId="x",
                        Matricula="1",
                        Nome ="Marcos",
                        Area="",
                        Cargo="Funcionário",
                        Salario="1001",
                        DataAdmissao="10/10/2021"
                }));

                Assert.Equal("Favor preencher a área do funcionario.", exception.Message);
       }
       [Fact]
       public void InserirFuncionarioSemCargo()
       {
              var exception = Assert.Throws<DominioException>(()=> funcionarioDominio.InserirFuncionario(new FuncionarioEntidade()
                {
                        FuncionairoId="x",
                        Matricula="1",
                        Nome ="Marcos",
                        Area="Diretoria",
                        Cargo="",
                        Salario="1001",
                        DataAdmissao="10/10/2021"
                }));

                Assert.Equal("Favor preencher o cargo do funcionario.", exception.Message);
       }
       [Fact]
       public void InserirFuncionarioCargoForadePadrao()
       {
              var exception = Assert.Throws<DominioException>(()=> funcionarioDominio.InserirFuncionario(new FuncionarioEntidade()
                {
                        FuncionairoId="x",
                        Matricula="1",
                        Nome ="Marcos",
                        Area="Diretoria",
                        Cargo="Cozinheiro",
                        Salario="1001",
                        DataAdmissao="10/10/2021"
                }));

                Assert.Equal("Favor preencher o cargo do funcionario como Estagiário ou Funcionário.", exception.Message);
       }


        [Fact]
       public void InserirFuncionarioSemSalario()
       {
              var exception = Assert.Throws<DominioException>(()=> funcionarioDominio.InserirFuncionario(new FuncionarioEntidade()
                {
                        FuncionairoId="x",
                        Matricula="1",
                        Nome ="Marcos",
                        Area="Diretoria",
                        Cargo="Funcionário",
                        Salario="",
                        DataAdmissao="10/10/2021"
                }));

                Assert.Equal("Favor preencher salario do funcionario.", exception.Message);
       }
        [Fact]
       public void InserirFuncionarioSalarioAbaixoDe1000()
       {
              var exception = Assert.Throws<DominioException>(()=> funcionarioDominio.InserirFuncionario(new FuncionarioEntidade()
                {
                        FuncionairoId="x",
                        Matricula="1",
                        Nome ="Marcos",
                        Area="Diretoria",
                        Cargo="Funcionário",
                        Salario="999",
                        DataAdmissao="10/10/2021"
                }));

                Assert.Equal("O salário do funcionario não pode ser menor que R$ 1000.", exception.Message);
       }
        [Fact]
       public void InserirFuncionarioSalarioInvalido()
       {
              var exception = Assert.Throws<DominioException>(()=> funcionarioDominio.InserirFuncionario(new FuncionarioEntidade()
                {
                        FuncionairoId="x",
                        Matricula="1",
                        Nome ="Marcos",
                        Area="Diretoria",
                        Cargo="Funcionário",
                        Salario="1001x.",
                        DataAdmissao="10/10/2021"
                }));

                Assert.Equal("Salário inválido.", exception.Message);
       }
       
        [Fact]
       public void InserirFuncionarioSemDataAdimissao()
       {
              var exception = Assert.Throws<DominioException>(()=> funcionarioDominio.InserirFuncionario(new FuncionarioEntidade()
                {
                        FuncionairoId="x",
                        Matricula="1",
                        Nome ="Marcos",
                        Area="Diretoria",
                        Cargo="Funcionário",
                        Salario="1001",
                        DataAdmissao=""
                }));

                Assert.Equal("Favor preencher a data de admissão do funcionario.", exception.Message);
       }

        [Fact]
       public void InserirFuncionarioDataAdimissaoInvalida()
       {
              var exception = Assert.Throws<DominioException>(()=> funcionarioDominio.InserirFuncionario(new FuncionarioEntidade()
                {
                        FuncionairoId="x",
                        Matricula="1",
                        Nome ="Marcos",
                        Area="Diretoria",
                        Cargo="Funcionário",
                        Salario="1001",
                        DataAdmissao="30/02/2021"
                }));

                Assert.Equal("Data de admissão inválida.", exception.Message);
       }
       
       [Fact]
       public void InserirFuncionarioOk()
       {
               Assert.True(funcionarioDominio.InserirFuncionario(new FuncionarioEntidade()
               {
                FuncionairoId="x",
                Matricula="1" ,
                Nome ="2",
                Area="Diretoria",
                Cargo="Funcionário",
                Salario="1001",
                DataAdmissao="10/10/2021"
               }));
       }
       [Fact]
       public void AlterarFuncionario_ok()
       {
               Assert.True(funcionarioDominio.AlterarFuncionario(new FuncionarioEntidade()
               {
                FuncionairoId="x",
                Matricula="1" ,
                Nome ="2",
                Area="Diretoria",
                Cargo="Funcionário",
                Salario="1001",
                DataAdmissao="10/10/2021"
               }));
       }
       [Fact]
       public void ExcluirFuncionario_ok()
       {
          Assert.True(funcionarioDominio.ExcluirFuncionario("X"));
       }
    }
}
