using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioTurim.InfraEstrutura.DAL;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using DesafioTurim.Dominio.Entidades;
using DesafioTurim.Dominio.Repositorios;

namespace DesafioTurim.InfraEstrutura.Data
{
    public class AreaAtuacaoDAL : BaseDAL, IAreaAtuacaoRepositorio
    {
        public AreaAtuacaoDAL()
        {
             AbrirConexao();
        }
        public async Task<List<AreaAtuacaoEntidade>> BuscaAerasdeAtuacaoPorDescricao(string desscricao)
        {
            try
            {
                Query areaAtuacaoQuery = fireStoreDb.Collection("Areas").WhereEqualTo("AreaAtuacao", desscricao);
                QuerySnapshot areaAtuacaoQuerySnapshot = await areaAtuacaoQuery.GetSnapshotAsync();
                List<AreaAtuacaoEntidade> listaAreaAtuacao = new List<AreaAtuacaoEntidade>();
                foreach (DocumentSnapshot documentSnapshot in areaAtuacaoQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> data = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(data);
                        AreaAtuacaoEntidade novaAreaAtuacao = JsonConvert.DeserializeObject<AreaAtuacaoEntidade>(json);
                        novaAreaAtuacao.AreaAtuacaoId = documentSnapshot.Id;
                        listaAreaAtuacao.Add(novaAreaAtuacao);
                    }
                }
                List<AreaAtuacaoEntidade> listaAreaAtuacaoOrdenada = listaAreaAtuacao.OrderBy(x => x.AreaAtuacao).ToList();
                return listaAreaAtuacaoOrdenada;
            }
            catch
            {
                throw;
            }
        }
        public async Task<AreaAtuacaoEntidade> BuscaAerasdeAtuacaoPorId(string id)
        {
            try
            {
                DocumentReference docRef = fireStoreDb.Collection("Areas").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    AreaAtuacaoEntidade areaAtuacao = snapshot.ConvertTo<AreaAtuacaoEntidade>();
                    areaAtuacao.AreaAtuacaoId = snapshot.Id;
                    return areaAtuacao;
                }
                else
                {
                    return new AreaAtuacaoEntidade();
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<AreaAtuacaoEntidade>> ListarAreasdeAtuacao()
        {
           try
            {
                Query areaAtuacaoQuery = fireStoreDb.Collection("Areas");
                QuerySnapshot areaAtuacaoQuerySnapshot = await areaAtuacaoQuery.GetSnapshotAsync();
                List<AreaAtuacaoEntidade> listaAreaAtuacao = new List<AreaAtuacaoEntidade>();
                foreach (DocumentSnapshot documentSnapshot in areaAtuacaoQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> data = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(data);
                        AreaAtuacaoEntidade novaAreaAtuacao = JsonConvert.DeserializeObject<AreaAtuacaoEntidade>(json);
                        novaAreaAtuacao.AreaAtuacaoId = documentSnapshot.Id;
                        listaAreaAtuacao.Add(novaAreaAtuacao);
                    }
                }
                List<AreaAtuacaoEntidade> listaAreaAtuacaoOrdenada = listaAreaAtuacao.OrderBy(x => x.AreaAtuacao).ToList();
                return listaAreaAtuacaoOrdenada;
            }
            catch(Exception ex)
            {
                var erro = ex.Message;
                throw;
            }
        }
    }
}