using DesafioTurim.InfraEstrutura.DAL;
using DesafioTurim.Dominio.Repositorios;
using System.Threading.Tasks;
using System.Collections.Generic;
using DesafioTurim.Dominio.Entidades;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
namespace DesafioTurim.InfraEstrutura.Data
{
    public class RelatorioDAL : BaseDAL, IRelatorioRepositorio
    {
        public RelatorioDAL()
        {
             AbrirConexao();
        }
        public async Task<List<DistribuicaoLucrosFuncionariosEntidade>> ListarDistribuicaoDeLucroPorFuncionario()
        {
            try
            {
                Query distribuicaoQuery = fireStoreDb.Collection("DistribuicaoLucrosFuncionarios");
                QuerySnapshot distribuicaoQuerySnapshot = await distribuicaoQuery.GetSnapshotAsync();
                List<DistribuicaoLucrosFuncionariosEntidade> listaDistribuicao = new List<DistribuicaoLucrosFuncionariosEntidade>();
                foreach (DocumentSnapshot documentSnapshot in distribuicaoQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> data = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(data);
                        DistribuicaoLucrosFuncionariosEntidade novoDistribuicao = JsonConvert.DeserializeObject<DistribuicaoLucrosFuncionariosEntidade>(json);
                        novoDistribuicao.DistribuicaoLucroID = documentSnapshot.Id;
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
        public async Task<List<FuncionarioEntidade>> ListarFuncionariosCadastratos()
        {
            try
            {
                Query funcionarioQuery = fireStoreDb.Collection("Funcionarios");
                QuerySnapshot funcionarioQuerySnapshot = await funcionarioQuery.GetSnapshotAsync();
                List<FuncionarioEntidade> listaFuncionario = new List<FuncionarioEntidade>();
                foreach (DocumentSnapshot documentSnapshot in funcionarioQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> data = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(data);
                        FuncionarioEntidade novoFuncionario = JsonConvert.DeserializeObject<FuncionarioEntidade>(json);
                        novoFuncionario.FuncionairoId = documentSnapshot.Id;
                        listaFuncionario.Add(novoFuncionario);
                    }
                }
                return listaFuncionario;
            }
            catch(Exception ex)
            {
                var erro = ex.Message;
                throw;
            }
        }
        public async Task<string> RetornaSaldoDisponibilizadoDistribuido()
        {
             var totalDisPonibilizado = await RetornaTotalDisPonibilizado();
             var totalDistribuido = await RetornaTotalDistribuido();
            try
            {
                var saldoDisponibilizadoDistribuido = Convert.ToDecimal(totalDisPonibilizado) - Convert.ToDecimal(totalDistribuido);
                return Convert.ToString(saldoDisponibilizadoDistribuido);
            }
            catch(Exception ex)
            {
                var erro = ex.Message;
                throw;
            }
        }
        public async Task<string> RetornaTotalDisPonibilizado()
        {
            try
            {
                Query distribuicaoQuery = fireStoreDb.Collection("Distribuicao");
                QuerySnapshot distribuicaoQuerySnapshot = await distribuicaoQuery.GetSnapshotAsync();
                decimal valorTotalDisponibilizado=0;
                foreach (DocumentSnapshot documentSnapshot in distribuicaoQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> data = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(data);
                        DistribuicaoEntidade novoDistribuicao = JsonConvert.DeserializeObject<DistribuicaoEntidade>(json);
                        valorTotalDisponibilizado += Convert.ToDecimal(novoDistribuicao.ValorDistribuir);

                    }
                }
                
                return Convert.ToString(valorTotalDisponibilizado);
            }
            catch(Exception ex)
            {
                var erro = ex.Message;
                throw;
            }
        }
        public async Task<string> RetornaTotalDistribuido()
        {
            try
            {
                Query distribuicaoQuery = fireStoreDb.Collection("DistribuicaoLucrosFuncionarios");
                QuerySnapshot distribuicaoQuerySnapshot = await distribuicaoQuery.GetSnapshotAsync();
                decimal valorTotalDistibuido=0;
                foreach (DocumentSnapshot documentSnapshot in distribuicaoQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> data = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(data);
                        DistribuicaoLucrosFuncionariosEntidade novoDistribuicao = JsonConvert.DeserializeObject<DistribuicaoLucrosFuncionariosEntidade>(json);
                        valorTotalDistibuido += Convert.ToDecimal(novoDistribuicao.ValorDistribuido);

                    }
                }
                 return Convert.ToString(valorTotalDistibuido);
            }
            catch(Exception ex)
            {
                var erro = ex.Message;
                throw;
            }
        }
    }
}