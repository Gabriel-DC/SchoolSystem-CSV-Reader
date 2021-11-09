using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem_CSV_Reader.classes
{
    public class TeacherRoster
    {
        public long? Section_SIS_ID { get; set; }
        public long SIS_ID { get; set; }

        public static void ExportarToCSV(TeacherRoster se)
        {
            string registro = $"{se.Section_SIS_ID},{se.SIS_ID}";

            Exportador.Exportar2(registro, "TeacherRoster");
        }

        public static void ExportarHeaderToCSV()
        {
            string header = string.Empty;
            TeacherRoster tr = new TeacherRoster();
            var props = tr.GetType().GetProperties();

            foreach (var prop in props)
            {
                header += $"{prop.Name},";
            }

            header = header.Substring(0, header.Length - 1);

            header = header.Replace("_", " ");

            Exportador.Exportar2(header, "TeacherRoster");
        }
    }
}
