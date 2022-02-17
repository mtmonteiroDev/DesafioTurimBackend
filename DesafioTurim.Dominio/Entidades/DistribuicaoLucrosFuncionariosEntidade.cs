
using Google.Cloud.Firestore;

namespace DesafioTurim.Dominio.Entidades
{
    [FirestoreData]
    public class DistribuicaoLucrosFuncionariosEntidade
    {
        public string DistribuicaoLucroID { get; set; }
        [FirestoreProperty]        
        public string Matricula { get; set; }
        [FirestoreProperty]        
        public string Nome { get; set; }
        [FirestoreProperty]        
        public string ValorDistribuido { get; set; }
    }
}