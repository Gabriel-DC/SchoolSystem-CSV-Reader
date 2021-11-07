using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem_CSV_Reader.classes
{
    public class StudentEnrollment
    {
        public long ID_Section{ get; set; }
        public long Student_ID  { get; set; }


        public static void ExportarToCSV(StudentEnrollment se)
        {
            string registro = $"{se.ID_Section},{se.Student_ID}";

            Exportador.Exportar2(registro, "StudentEnrollment");
        }
    }
}
