using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem_CSV_Reader.classes
{
    public class Section
    {
        public long SIS_ID { get; set; }
        public long? School_SIS_ID { get; set; }
        public string Section_Name { get; set; }

        public static Section FromSyncAluno(SyncAluno syncAluno)
        {
            Section section = new Section();

            (section.SIS_ID, section.School_SIS_ID, section.Section_Name) = (syncAluno.CodigoTurma, syncAluno.School_ID, $"{syncAluno.NomeTurma} - {syncAluno.NomeSerie}");

            return section;
        }
        public static Section FromSyncProfessor(SyncProfessor syncProfessor)
        {
            Section section = new Section();

            (section.SIS_ID, section.School_SIS_ID, section.Section_Name) = ((long)syncProfessor.CodigoTurma, syncProfessor.School_ID, $"{syncProfessor.NomeTurma} - {syncProfessor.NomeSerie}");

            return section;
        }

        public static void ExportarToCSV(Section section)
        {
            string registro = $"{section.SIS_ID},{section.School_SIS_ID},{section.Section_Name}";

            Exportador.Exportar2(registro, "Section");
        }

        public static void ExportarHeaderToCSV()
        {
            string header = string.Empty;
            Section s = new Section();
            var props = s.GetType().GetProperties();

            foreach (var prop in props)
            {
                header += $"{prop.Name},";
            }

            header = header.Substring(0, header.Length - 1);

            header = header.Replace("_", " ");

            Exportador.Exportar2(header, "Section");
        }
    }
}
