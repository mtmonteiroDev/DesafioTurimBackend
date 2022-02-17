using System;
using Google.Cloud.Firestore;

namespace DesafioTurim.Dominio.Entidades
{
    [FirestoreData]
    public class FuncionarioEntidade
    {
        public string FuncionairoId { get; set; }
        [FirestoreProperty]
        public string Matricula { get; set; }
        [FirestoreProperty]
        public string Nome { get; set; }
        [FirestoreProperty]
        public string Area { get; set; }
        [FirestoreProperty]
        public string Cargo { get; set; }
        [FirestoreProperty]
        public  string Salario { get; set; }
        [FirestoreProperty]
        public string DataAdmissao { get; set; }
    }
}