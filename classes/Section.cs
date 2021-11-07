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
        public long? School_ID { get; set; }
        public string SectionName { get; set; }

        public static Section FromSyncAluno(SyncAluno syncAluno)
        {
            Section section = new Section();

            (section.SIS_ID, section.School_ID, section.SectionName) = (syncAluno.CodigoTurma, syncAluno.School_ID, $"{syncAluno.NomeTurma} - {syncAluno.NomeSerie}");

            return section;
        }
        public static Section FromSyncProfessor(SyncProfessor syncProfessor)
        {
            Section section = new Section();

            (section.SIS_ID, section.School_ID, section.SectionName) = ((long)syncProfessor.CodigoTurma, syncProfessor.School_ID, $"{syncProfessor.NomeTurma} - {syncProfessor.NomeSerie}");

            return section;
        }

        public static void ExportarToCSV(Section section)
        {
            string registro = $"{section.SIS_ID},{section.School_ID},{section.SectionName}";

            Exportador.Exportar2(registro, "Section");
        }
    }
}
