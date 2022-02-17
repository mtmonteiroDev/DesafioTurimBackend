using Google.Cloud.Firestore;

namespace DesafioTurim.Dominio.Entidades
{
    [FirestoreData]
    public class DistribuicaoEntidade
    {
        
        public string DistribuicaoID { get; set; }
        [FirestoreProperty]        
        public string ValorDistribuir { get; set; }

        
    }
}