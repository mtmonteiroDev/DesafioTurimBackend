
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioTurim.Dominio.Repositorios;
using DesafioTurim.Dominio.Entidades;
using DesafioTurim.InfraEstrutura.DAL;
namespace DesafioTurim.InfraEstrutura.Data
{
    public class FuncionarioDAL : BaseDAL, IFuncionarioRepositorio
    {
        public FuncionarioDAL()
        {
             AbrirConexao();
        }
        public async Task<List<FuncionarioEntidade>> ListarFuncionariosRepositorio()
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
                List<FuncionarioEntidade> listaFuncionarioOrdenada = listaFuncionario.OrderBy(x => x.Nome).ToList();
                return listaFuncionarioOrdenada;
            }
            catch(Exception ex)
            {
                var erro = ex.Message;
                throw;
            }
        }
        public async Task<FuncionarioEntidade> BuscarFuncionarioPorId(string id)
        {
             try
            {
                DocumentReference docRef = fireStoreDb.Collection("Funcionarios").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    FuncionarioEntidade funcionario = snapshot.ConvertTo<FuncionarioEntidade>();
                    funcionario.FuncionairoId = snapshot.Id;
                    return funcionario;
                }
                else
                {
                    return new FuncionarioEntidade();
                }
            }
            catch
            {
                throw;
            }
        }
       public async void InserirFuncionario(FuncionarioEntidade funcionario)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("Funcionarios");
                await colRef.AddAsync(funcionario);
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
        public async void ExcluirFuncionario(string id)
        {
            try
            {
                DocumentReference alunoRef = fireStoreDb.Collection("Funcionarios").Document(id);
                await alunoRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
        public Task<FuncionarioEntidade> BuscarFuncionarioPorMatricula(string matricula)
        {
            throw new NotImplementedException();
        }
        public async Task zerarDistribuicaoDeLucrosParaOsFuncionarios()
        {
            try
            {
                // Apaga a coleção de distribuição de lucros
                CollectionReference colRef = fireStoreDb.Collection("DistribuicaoLucrosFuncionarios");
                QuerySnapshot snapshot = await colRef.Limit(1000).GetSnapshotAsync();
                IReadOnlyList<DocumentSnapshot> documents = snapshot.Documents;
                while (documents.Count > 0)
                {
                    foreach (DocumentSnapshot document in documents)
                    {
                        await document.Reference.DeleteAsync();
                    }
                    snapshot = await colRef.Limit(1000).GetSnapshotAsync();
                    documents = snapshot.Documents;
                }
            }
            catch(Exception ex)
            {
                var erro = ex.Message;
                throw;
            }
        }
        public async Task AtualizaDistribuicaoDeLucrosParaOsFuncionarios(DistribuicaoLucrosFuncionariosEntidade distribuicaoLucrosFuncionariosEntidade)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("DistribuicaoLucrosFuncionarios");
                await colRef.AddAsync(distribuicaoLucrosFuncionariosEntidade);
            }
            catch
            {
                throw;
            }
        }
    }
}