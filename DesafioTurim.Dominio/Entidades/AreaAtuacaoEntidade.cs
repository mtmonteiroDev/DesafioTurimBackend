using Google.Cloud.Firestore;

namespace DesafioTurim.Dominio.Entidades
{
    [FirestoreData]
    public class AreaAtuacaoEntidade
    {
        public string AreaAtuacaoId { get; set; }
        [FirestoreProperty]        
        public string AreaAtuacao { get; set; }
    }
}