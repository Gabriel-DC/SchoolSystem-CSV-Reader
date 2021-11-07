using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem_CSV_Reader.classes
{
    public class SyncProfessor
    {
        public long ID { get; set; }
        public long? School_ID { get; set; }
        public long? CodigoProfessor { get; set; }
        public string NomeProfessor { get; set; }
        public string EmailInstitucional { get; set; }
        public string EmailPessoal { get; set; }
        public int CodigoDisciplina { get; set; }
        public string NomeDisciplina { get; set; }
        public string DescricaoDisciplina { get; set; }        
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }               
        public int? CodigoCurso { get; set; }               
        public string NomeCurso { get; set; }
        public int? CodigoSerie { get; set; }
        public string NomeSerie { get; set; }
        public long? CodigoTurma { get; set; }
        public string NomeTurma { get; set; }        
        public int? CodigoTurno { get; set; }
        public string NomeTurno { get; set; }

        public static SyncProfessor FromCsv(string csvLine)
        {
            string[] values = FormataValores(csvLine.Split(';').ToList());

            SyncProfessor syncProfessor = new SyncProfessor();
            syncProfessor.ID = Convert.ToInt64(values[0]);
            syncProfessor.School_ID = Convert.ToInt64(values[1]);
            syncProfessor.CodigoProfessor = Convert.ToInt64(values[2]);
            
            syncProfessor.NomeProfessor = Convert.ToString(values[3]);            

            //(syncProfessor.Nome, syncProfessor.NomeDoMeio, syncProfessor.Sobrenome) = SepararNomes(syncProfessor.NomeProfessor);
            
            syncProfessor.EmailInstitucional = Convert.ToString(values[4]);
            syncProfessor.EmailPessoal = Convert.ToString(values[5]);

            syncProfessor.CodigoDisciplina = Convert.ToInt32(values[6]);
            syncProfessor.NomeDisciplina = Convert.ToString(values[7]);
            syncProfessor.DescricaoDisciplina = Convert.ToString(values[8]) == "NULL" ? null : Convert.ToString(values[8]);

            syncProfessor.DataInicial = Convert.ToDateTime(values[9]);
            syncProfessor.DataFinal = Convert.ToDateTime(values[10]);

            syncProfessor.CodigoCurso = Convert.ToInt32(values[11]);
            syncProfessor.NomeCurso = Convert.ToString(values[12]);

            syncProfessor.CodigoSerie = Convert.ToInt32(values[13]);
            syncProfessor.NomeSerie = Convert.ToString(values[14]);

            syncProfessor.CodigoTurma = Convert.ToInt32(values[15]);
            syncProfessor.NomeTurma = Convert.ToString(values[16]);

            syncProfessor.CodigoTurno = Convert.ToInt32(values[17]);
            syncProfessor.NomeTurno = Convert.ToString(values[18]);
            
            return syncProfessor;
        }

        public static string[] FormataValores(List<string> valores)
        {
            valores.ForEach(valor =>
            {
                //if(valor == "NULL" || string.IsNullOrWhiteSpace(valor))
                //{
                //    valor = "";
                //}
                //else
                //{
                //    valor = valor.Trim();
                //}

                valor = valor.Trim() == "NULL" || string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
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
}

