using System;
using Google.Cloud.Firestore;

namespace DesafioTurim.InfraEstrutura.DAL
{
    public abstract class BaseDAL
    {
         // objetos FirestoreDb
        public FirestoreDb fireStoreDb;
        public void FactoryConnection()
        {
             string arquivoApiKey = @"C:\Projetos\Desafio_Turim\BackEnd\desafioturim-5d4810dd93d1.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", arquivoApiKey );
            fireStoreDb = FirestoreDb.Create("desafioturim");
        }
        public void AbrirConexao()
        {
            if (fireStoreDb == null)
                FactoryConnection();
        }
    }
}