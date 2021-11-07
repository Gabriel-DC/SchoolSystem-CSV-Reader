using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem_CSV_Reader.classes
{
    public class Exportador
    {
        const string PATH = @"C:\\Users\\Public\\";

        public static void Exportar<T>(HashSet<T> ts, Type Te)
        {
            //String path = @"C:\\Users\\Public\\logcatraca.txt";

            var nameFile = ts.GetType().GenericTypeArguments[0].Name;

            var props = Te.GetMembers();

            foreach(var registro in ts)
            {
                File.AppendAllText($"{PATH}{nameFile}.csv", "1" + Environment.NewLine);
            }

            //File.AppendAllText($"{PATH}{nameFile}.csv",);

            //FileInfo fileInfo = new FileInfo(path);

            //if (fileInfo.Length > 2097152)
            //{
            //    File.Delete(path);
            //}

        }

        public static void Exportar2(string registro, string fileName)
        {
            File.AppendAllText($"{PATH}{fileName}.csv", registro + Environment.NewLine);
        }

    }
}
