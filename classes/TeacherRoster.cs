using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem_CSV_Reader.classes
{
    public class TeacherRoster
    {
        public long? ID_Section { get; set; }
        public long Teacher_ID { get; set; }

        public static void ExportarToCSV(TeacherRoster se)
        {
            string registro = $"{se.ID_Section},{se.Teacher_ID}";

            Exportador.Exportar2(registro, "TeacherRoster");
        }
    }
}
