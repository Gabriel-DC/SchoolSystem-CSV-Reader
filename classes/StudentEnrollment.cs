using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem_CSV_Reader.classes
{
    public class StudentEnrollment
    {
        public long Section_SIS_ID{ get; set; }
        public long SIS_ID  { get; set; }


        public static void ExportHeaderToCSV()
        {
            string header = $"{nameof(Section_SIS_ID)},{nameof(SIS_ID)}".Replace("_", " ");

            Exportador.Exportar2(header, "StudentEnrollment");
        }

        public static void ExportarToCSV(StudentEnrollment se)
        {
            string registro = $"{se.Section_SIS_ID},{se.SIS_ID}";

            Exportador.Exportar2(registro, "StudentEnrollment");
        }
    }
}
