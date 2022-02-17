using System.Collections.Generic;
using DesafioTurim.Dominio.Entidades;
using Google.Cloud.Firestore;

namespace DesafioTurim.Apresentacao.Models
{
    [FirestoreData]
    public class AreaAtuacaoModel
    {
        public string AreaAtuacaoId { get; set; }
        [FirestoreProperty]        
        public string AreaAtuacao { get; set; }
        public static AreaAtuacaoEntidade ModelToEntidade (AreaAtuacaoModel areaAtuacaoModel)
        {
            return new AreaAtuacaoEntidade(){
                AreaAtuacaoId = areaAtuacaoModel.AreaAtuacaoId,
                AreaAtuacao = areaAtuacaoModel.AreaAtuacao,
            };
        }
        public static AreaAtuacaoModel EntidadeToModel (AreaAtuacaoEntidade areaAtuacaoEntidade)
        {
            return new AreaAtuacaoModel(){
                AreaAtuacaoId = areaAtuacaoEntidade.AreaAtuacaoId,
                AreaAtuacao = areaAtuacaoEntidade.AreaAtuacao,
            };
        }
        public static List<AreaAtuacaoModel> ModelToEntidadeList(List<AreaAtuacaoEntidade> areaAtuacaoEntidade)
        {
            var areaAtuacaoModel = new List<AreaAtuacaoModel>();
            if (areaAtuacaoEntidade != null)
            {
                foreach (var area in areaAtuacaoEntidade)
                {
                    areaAtuacaoModel.Add(AreaAtuacaoModel.EntidadeToModel(area));
                }
            }    
            return areaAtuacaoModel;
        }    
    }



    
}