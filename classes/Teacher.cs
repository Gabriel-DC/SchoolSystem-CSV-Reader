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
        public long? School_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long? State_ID { get; set; }
        public string TeacherNumber { get; set; }
        public string Status { get; set; }
        public string MiddleName { get; set; }
        public string SecondaryEmail { get; set; }
        public string Title { get; set; }
        public string Qualification { get; set; }

        public static Teacher FromSyncProfessor(SyncProfessor syncProfessor, HashSet<Teacher> teacherSet)
        {
            Teacher t = new Teacher();

            if (teacherSet.Any(teacher => teacher.SIS_ID == syncProfessor.CodigoProfessor))
                return null;

            t.SIS_ID = (long)syncProfessor.CodigoProfessor;
            t.School_ID = syncProfessor.School_ID;

            (t.FirstName, t.MiddleName, t.LastName) = SyncProfessor.SepararNomes(syncProfessor.NomeProfessor);

            //t.FirstName = syncProfessor.Nome;
            //t.LastName = syncProfessor.Sobrenome;
            //t.MiddleName = syncProfessor.NomeDoMeio;

            if (string.IsNullOrEmpty(syncProfessor.EmailInstitucional))
            {
                var username = $"{t.FirstName}{t.LastName}";

                if (teacherSet.Any(x => x.Username == username))
                {
                    username = $"{t.FirstName}{t.MiddleName}";
                }

                t.Username = username;
            }
            else
            {
                //student.Username = syncAluno.EmailInstitucional;
                t.Password = $"esd@{t.SIS_ID}";
            }

            t.State_ID = null;
            t.SecondaryEmail = syncProfessor.EmailPessoal;
            t.TeacherNumber = null;

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

            Exportador.Exportar2(registro, "Teacher");
        }
    }
}
