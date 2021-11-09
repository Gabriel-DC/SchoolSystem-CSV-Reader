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
        public long? School_SIS_ID { get; set; }
        public string First_Name  { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }        
        public long? State_ID { get; set; }
        public string Secondary_Email { get; set; }
        public string Student_Number { get; set; }
        public string Middle_Name { get; set; }
        public string Grade { get; set; }
        public int? Status { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? Graduation_year { get; set; }

        public static Student FromSyncAluno(SyncAluno syncAluno, HashSet<Student> studentSet)
        {
            Student student = new Student();

            student.SIS_ID = syncAluno.ID;
            student.School_SIS_ID = syncAluno.School_ID;
            student.First_Name = syncAluno.Nome;
            student.Last_Name = syncAluno.Sobrenome;
            
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
                student.Username = syncAluno.EmailInstitucional.Substring(0, syncAluno.EmailInstitucional.IndexOf("@"));                
            }

            student.Password = $"Esd@{syncAluno.NumeroMatricula}";

            student.State_ID = null;
            student.Secondary_Email = syncAluno.EmailParticular;
            student.Student_Number = null;
            student.Middle_Name = syncAluno.NomeDoMeio;

            student.Grade = null;
            student.Status = syncAluno.Status;
            student.Birthdate = syncAluno.DataNascimento;
            student.Graduation_year = null;            

            return student;
        }

        public static void ExportarHeaderToCSV()
        {
            string header = string.Empty;
            Student s = new Student();
            var props = s.GetType().GetProperties();

            foreach (var prop in props)
            {
                header += $"{prop.Name},";
            }

            header = header.Substring(0, header.Length - 1);

            header = header.Replace("_", " ");

            Exportador.Exportar2(header, "Student");
        }

        public static void ExportarToCSV(Student s)
        {
            var t = s.GetType().GetProperties();            

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

            registro = registro.Substring(0, registro.Length-1);

            Exportador.Exportar2(registro, "Student");
        }
    }
}
