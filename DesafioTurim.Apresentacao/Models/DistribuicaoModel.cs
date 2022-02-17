using System;
using System.Collections.Generic;
using DesafioTurim.Dominio.Entidades;

namespace DesafioTurim.Apresentacao.Models
{
    public class DistribuicaoModel
    {
        public string DistribuicaoID { get; set; }
        public decimal ValorDistribuir { get; set; }
        public static DistribuicaoEntidade ModelToEntidade (DistribuicaoModel distribuicaoModel)
        {
            return new DistribuicaoEntidade(){
                DistribuicaoID = distribuicaoModel.DistribuicaoID,
                ValorDistribuir = Convert.ToString(distribuicaoModel.ValorDistribuir)
            };
        }
        public static DistribuicaoModel EntidadeToModel (DistribuicaoEntidade distribuicaoEntidade)
        {
            return new DistribuicaoModel(){
                DistribuicaoID = distribuicaoEntidade.DistribuicaoID,
                ValorDistribuir = Convert.ToDecimal(distribuicaoEntidade.ValorDistribuir)
            };
        }
        public static List<DistribuicaoModel> ModelToEntidadeList(List<DistribuicaoEntidade> distribuicaoEntidade)
        {
            var distribuicaoModel = new List<DistribuicaoModel>();
            if (distribuicaoModel != null)
            {
                foreach (var distribuicao in distribuicaoEntidade)
                {
                    distribuicaoModel.Add(DistribuicaoModel.EntidadeToModel(distribuicao));
                }
            }    
            return distribuicaoModel;
        }  
    }
}