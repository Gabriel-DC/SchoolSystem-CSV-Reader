using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem_CSV_Reader.classes
{
    public class Teacher
    {
        public long SIS_ID { get; set; }
        public long? School_SIS_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long? State_ID { get; set; }
        public string Teacher_Number { get; set; }
        public string Status { get; set; }
        public string Middle_Name { get; set; }
        public string Secondary_Email { get; set; }
        public string Title { get; set; }
        public string Qualification { get; set; }

        public static Teacher FromSyncProfessor(SyncProfessor syncProfessor, HashSet<Teacher> teacherSet)
        {
            Teacher t = new Teacher();

            if (teacherSet.Any(teacher => teacher.SIS_ID == syncProfessor.CodigoProfessor))
                return null;

            t.SIS_ID = (long)syncProfessor.CodigoProfessor;
            t.School_SIS_ID = syncProfessor.School_ID;

            (t.First_Name, t.Middle_Name, t.Last_Name) = SyncProfessor.SepararNomes(syncProfessor.NomeProfessor);

            //t.FirstName = syncProfessor.Nome;
            //t.LastName = syncProfessor.Sobrenome;
            //t.MiddleName = syncProfessor.NomeDoMeio;

            if (string.IsNullOrEmpty(syncProfessor.EmailInstitucional))
            {
                var username = $"{t.First_Name}{t.Last_Name}";

                if (teacherSet.Any(x => x.Username == username))
                {
                    username = $"{t.First_Name}{t.Middle_Name}";
                }

                t.Username = username;                
            }
            else
            {
                t.Username = syncProfessor.EmailInstitucional.Substring(0, syncProfessor.EmailInstitucional.IndexOf("@"));                
            }

            t.Password = $"Esd@{t.SIS_ID}";

            t.State_ID = null;
            t.Secondary_Email = syncProfessor.EmailPessoal;
            t.Teacher_Number = null;

            t.Status = null;
            t.Title = null;
            t.Qualification = null;

            return t;
        }
        public static void ExportarToCSV(Teacher s)
        {
            var t = s.GetType().GetProperties();                        

            string registro = string.Empty;

            foreach (var seila in t)
            {
                registro += $"{seila.GetValue(s)},";
            }

            registro = registro.Replace("\"", "").Replace("NULL", "");

            registro = registro.Substring(0, registro.Length - 1);

            Exportador.Exportar2(registro, "Teacher");
        }

        public static void ExportarHeaderToCSV()
        {
            string header = string.Empty;
            Teacher t = new Teacher();
            var props = t.GetType().GetProperties();

            foreach (var prop in props)
            {
                header += $"{prop.Name},";
            }

            header = header.Substring(0, header.Length - 1);

            header = header.Replace("_", " ");

            Exportador.Exportar2(header, "Teacher");
        }
    }
}
