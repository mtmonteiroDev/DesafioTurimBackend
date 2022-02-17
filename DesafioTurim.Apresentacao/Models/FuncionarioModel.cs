using System;
using System.Collections.Generic;
using DesafioTurim.Dominio.Entidades;

namespace DesafioTurim.Apresentacao.Models
{
    
    public class FuncionarioModel
    {

        public string FuncionairoId { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Area { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
        public string Dataadmissao { get; set; }
        public static FuncionarioEntidade ModelToEntidade (FuncionarioModel funcionarioModel)
        {
            return new FuncionarioEntidade(){
                FuncionairoId = funcionarioModel.FuncionairoId,
                Matricula = funcionarioModel.Matricula,
                Nome = funcionarioModel.Nome,
                Area = funcionarioModel.Area,
                Cargo = funcionarioModel.Cargo,
                Salario = Convert.ToString(funcionarioModel.Salario),
                DataAdmissao = funcionarioModel.Dataadmissao
            };
        }
        public static FuncionarioModel EntidadeToModel (FuncionarioEntidade funcionarioEntidade)
        {
            return new FuncionarioModel(){
                FuncionairoId = funcionarioEntidade.FuncionairoId,
                Matricula = funcionarioEntidade.Matricula,
                Nome = funcionarioEntidade.Nome,
                Area = funcionarioEntidade.Area,
                Cargo = funcionarioEntidade.Cargo,
                Salario = Convert.ToDecimal(funcionarioEntidade.Salario),
                Dataadmissao = funcionarioEntidade.DataAdmissao
            };
        }
        public static List<FuncionarioModel> ModelToEntidadeList(List<FuncionarioEntidade> funcionarioEntidade)
        {
            var funcionarioModel = new List<FuncionarioModel>();
            if (funcionarioModel != null)
            {
                foreach (var fucnionario in funcionarioEntidade)
                {
                    funcionarioModel.Add(FuncionarioModel.EntidadeToModel(fucnionario));
                }
            }    
            return funcionarioModel;
        }    
    }
}