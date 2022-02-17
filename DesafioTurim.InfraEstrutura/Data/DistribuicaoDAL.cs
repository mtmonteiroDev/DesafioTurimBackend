using DesafioTurim.InfraEstrutura.DAL;
using DesafioTurim.Dominio.Repositorios;
using DesafioTurim.Dominio.Entidades;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace DesafioTurim.InfraEstrutura.Data
{
    public class DistribuicaoDAL : BaseDAL, IDistribuicaoRepositorio
    {
        public DistribuicaoDAL()
        {
             AbrirConexao();
        }
        public async Task<List<DistribuicaoEntidade>> BuscarValorDistribuicao()
        {
            try
            {
                Query distribuicaoQuery = fireStoreDb.Collection("Distribuicao");
                QuerySnapshot distribuicaoQuerySnapshot = await distribuicaoQuery.GetSnapshotAsync();
                List<DistribuicaoEntidade> listaDistribuicao = new List<DistribuicaoEntidade>();
                foreach (DocumentSnapshot documentSnapshot in distribuicaoQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> data = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(data);
                        DistribuicaoEntidade novoDistribuicao = JsonConvert.DeserializeObject<DistribuicaoEntidade>(json);
                        novoDistribuicao.DistribuicaoID = documentSnapshot.Id;
                        listaDistribuicao.Add(novoDistribuicao);
                    }
                }
                
                return listaDistribuicao;
            }
            catch(Exception ex)
            {
                var erro = ex.Message;
                throw;
            }
        }
         public async void AtualizaValorDistrinuicao(DistribuicaoEntidade distribuicaoEntidade)
         {
             try
            {
                 DocumentReference distribuicaoRef = fireStoreDb.Collection("Distribuicao").Document(distribuicaoEntidade.DistribuicaoID);
                await distribuicaoRef.SetAsync(distribuicaoEntidade, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
         }
         public async void AlterarFuncionario(FuncionarioEntidade funcionario)
        {
           try
            {
                DocumentReference funcionarioRef = fireStoreDb.Collection("Funcionarios").Document(funcionario.FuncionairoId);
                await funcionarioRef.SetAsync(funcionario, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
    }
}