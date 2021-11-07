using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem_CSV_Reader.classes
{
    public class Student
    {
        public long SIS_ID { get; set; }
        public long? School_ID { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public long? State_ID { get; set; }
        public string SecondaryEmail { get; set; }
        public string StudentNumber { get; set; }
        public string MiddleName { get; set; }
        public string Grade { get; set; }
        public int? Status { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? Graduation_year { get; set; }

        public static Student FromSyncAluno(SyncAluno syncAluno, HashSet<Student> studentSet)
        {
            Student student = new Student();

            student.SIS_ID = syncAluno.ID;
            student.School_ID = syncAluno.School_ID;
            student.FirstName = syncAluno.Nome;
            student.LastName = syncAluno.Sobrenome;
            
            if(string.IsNullOrEmpty(syncAluno.EmailInstitucional))
            {
                var username = $"{syncAluno.Nome}{syncAluno.Sobrenome}";
                
                if(studentSet.Any(x => x.Username == username))
                {
                    username = $"{syncAluno.Nome}{syncAluno.NomeDoMeio}";
                }

                student.Username = username;
            }
            else
            {
                //student.Username = syncAluno.EmailInstitucional;
                student.Password = $"esd@{syncAluno.NumeroMatricula}";
            }          
                
            student.State_ID = null;
            student.SecondaryEmail = syncAluno.EmailParticular;
            student.StudentNumber = null;
            student.MiddleName = syncAluno.NomeDoMeio;

            student.Grade = null;
            student.Status = syncAluno.Status;
            student.Birthdate = syncAluno.DataNascimento;
            student.Graduation_year = null;            

            return student;
        }

        public static void ExportarToCSV(Student s)
        {
            var t = s.GetType().GetProperties();
            var f = s.GetType().GetFields();

            var formato = "yyyy-MM-hh";

            string registro = string.Empty;

            foreach(var seila in t)
            {               
                if (seila.Name == "Birthdate")
                    registro += $"{Convert.ToDateTime(seila.GetValue(s)).ToString(formato)},";
                else
                    registro += $"{seila.GetValue(s)},";
            }

            registro = registro.Replace("\"","");            

            Exportador.Exportar2(registro, "Student");
        }
    }
}
