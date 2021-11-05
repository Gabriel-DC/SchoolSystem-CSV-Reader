using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem_CSV_Reader.classes
{
    public class SyncAluno
    {
        public long ID { get; set; }
        public long? NumeroMatricula { get; set; }
        public long? EscolaID { get; set; }
        public string Inep { get; set; }
        public string NomeCompleto { get; set; }
        public string Nome { get; set; }
        public string NomeDoMeio { get; set; }
        public string Sobrenome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string EmailInstitucional { get; set; }
        public string EmailParticular { get; set; }
        public int CodigoSerie { get; set; }
        public string NomeSerie { get; set; }
        public int CodigoTurma { get; set; }
        public string NomeTurma { get; set; }
        public int CodigoTurno { get; set; }
        public string NomeTurno { get; set; }
        public int AnoLetivo { get; set; }
        public int Status { get; set; }

        public static SyncAluno FromCsv(string csvLine)
        {
            string[] values = FormataValores(csvLine.Split(',').ToList());

            SyncAluno syncAluno = new SyncAluno();
            syncAluno.ID = Convert.ToInt64(values[0]);
            syncAluno.NumeroMatricula = Convert.ToInt64(values[1]);
            syncAluno.EscolaID = Convert.ToInt64(values[2]);
            syncAluno.Inep = Convert.ToString(values[3]);

            syncAluno.NomeCompleto = Convert.ToString(values[4]);

            (syncAluno.Nome, syncAluno.NomeDoMeio, syncAluno.Sobrenome) = SepararNomes(syncAluno.NomeCompleto);

            //syncAluno.Nome = Convert.ToString(values[5]);
            //syncAluno.NomeDoMeio = Convert.ToString(values[6]);
            //syncAluno.Sobrenome = Convert.ToString(values[7]);
            syncAluno.DataNascimento = Convert.ToDateTime(values[8]);
            syncAluno.EmailInstitucional = Convert.ToString(values[9]);
            syncAluno.EmailParticular = Convert.ToString(values[10]);
            syncAluno.CodigoSerie = Convert.ToInt32(values[11]);
            syncAluno.NomeSerie = Convert.ToString(values[12]);
            syncAluno.CodigoTurma = Convert.ToInt32(values[13]);
            syncAluno.NomeTurma = Convert.ToString(values[14]);
            syncAluno.CodigoTurno = Convert.ToInt32(values[15]);
            syncAluno.NomeTurno = Convert.ToString(values[16]);
            syncAluno.AnoLetivo = Convert.ToInt32(values[17]);
            syncAluno.Status = Convert.ToInt32(values[18]);
            return syncAluno;
        }

        public static string[] FormataValores(List<string> valores)
        {
            valores.ForEach(valor =>
            {
                valor = valor == "NULL" || string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
            });

            return valores.ToArray();
        }

        public static (string nome, string nomeDoMeio, string sobrenome) SepararNomes(string nomeCompleto)
        {           
            if (string.IsNullOrEmpty(nomeCompleto))
                return (null, null, null);

            string[] conectivos = new string[] { "de", "da", "do", "dos", "das" };

            var nomes = nomeCompleto.Split(' ').Where((nome, posicao) => !conectivos.Contains(nome)).ToList();

            if (nomes.Count >= 3)
                return (
                    nomes.FirstOrDefault(),
                    string.Join(' ', nomes.Where((nome, posicao) => posicao > 0 && posicao < nomes.Count - 1)),
                    nomes.LastOrDefault()
                );
            else if (nomes.Count == 2)
                return (
                    nomes.FirstOrDefault(),
                    null,
                    nomes.LastOrDefault()
                );
            else
                return (
                    nomes.FirstOrDefault(),
                    null,
                    null
                );            
        }
    }



    //ID,
    //NumeroMatricula,
    //EscolaID,
    //Inep,
    //NomeCompleto,
    //Nome,
    //NomeDoMeio,
    //SobreNome,
    //DataNascimento,
    //EmailInstitucional,
    //EmailParticular,
    //CodigoSerie,
    //NomeSerie,
    //CodigoTurma,
    //NomeTurma,
    //CodigoTurno,
    //NomeTurno,
    //AnoLetivo,
    //Status
}
