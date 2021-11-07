using SchoolSystem_CSV_Reader.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolSystem_CSV_Reader
{
    public partial class Form1 : Form
    {
        const string SYNC_ALUNO_HEADER = "ID;NumeroMatricula;EscolaID;Inep;NomeCompleto;Nome;NomeDoMeio;SobreNome;DataNascimento;EmailInstitucional;EmailParticular;CodigoSerie;NomeSerie;CodigoTurma;NomeTurma;CodigoTurno;NomeTurno;AnoLetivo;Status";
        const string SYNC_PROFESSOR_HEADER = "ID;EscolaID;CodigoProfessor;NomeProfessor;EmailInstitucional;EmailPessoal;CodigoDisciplina;NomeDisciplina;DescricaoDisciplina;DataInicial;DataFinal;CodigoCurso;NomeCurso;CodigoSerie;NomeSerie;CodigoTurma;NomeTurma;CodigoTurno;NomeTurno";
        const string PATH_ARQUIVOS = "C:\\Users\\Public";
        HashSet<Section> sections = new HashSet<Section>();
        HashSet<Student> students = new HashSet<Student>();
        HashSet<Teacher> teachers = new HashSet<Teacher>();
        HashSet<StudentEnrollment> studentEnrollments = new HashSet<StudentEnrollment>();
        HashSet<TeacherRoster> teacherRosters = new HashSet<TeacherRoster>();

        string arquivoAluno = string.Empty;
        string arquivoProfessor = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;

                string[] dadosAlunos = System.IO.File.ReadAllLines(arquivoAluno);
                string[] dadosProfessor = System.IO.File.ReadAllLines(arquivoProfessor);

                if (dadosAlunos.FirstOrDefault() != SYNC_ALUNO_HEADER)
                    return;

                if (dadosProfessor.FirstOrDefault() != SYNC_PROFESSOR_HEADER)
                    return;

                List<SyncAluno> syncAlunos = dadosAlunos.Skip(1).Select(r => SyncAluno.FromCsv(r)).ToList();
                List<SyncProfessor> syncProfessors = dadosProfessor.Skip(1).Select(p => SyncProfessor.FromCsv(p)).ToList();

                foreach (SyncProfessor professor in syncProfessors)
                {
                    Teacher t = Teacher.FromSyncProfessor(professor, teachers);

                    if(t != null)
                        teachers.Add(t);                    

                    Section section = Section.FromSyncProfessor(professor);

                    if (!sections.Any(x => x.SIS_ID == section.SIS_ID))
                        sections.Add(section);

                    if (professor.CodigoTurma != null)
                    {
                        if (sections.Any(s => s.SIS_ID == professor.CodigoTurma))
                        {
                            TeacherRoster tr = new TeacherRoster();
                            tr.ID_Section = (long)professor.CodigoTurma;
                            tr.Teacher_ID = (long)professor.CodigoProfessor;

                            teacherRosters.Add(tr);
                        }
                    }
                }

                foreach (SyncAluno aluno in syncAlunos)
                {
                    students.Add(Student.FromSyncAluno(aluno, students));

                    Section section = Section.FromSyncAluno(aluno);

                    if (!sections.Any(x => x.SIS_ID == section.SIS_ID))
                        sections.Add(section);

                    //if (aluno.CodigoTurma != null)
                    //{
                    if (sections.Any(s => s.SIS_ID == aluno.CodigoTurma))
                    {
                        StudentEnrollment se = new StudentEnrollment();
                        se.ID_Section = aluno.CodigoTurma;
                        se.Student_ID = aluno.ID;

                        studentEnrollments.Add(se);
                    }
                    //}
                }

                foreach (var x in studentEnrollments)
                {
                    StudentEnrollment.ExportarToCSV(x);
                }

                foreach (var y in students)
                {
                    Student.ExportarToCSV(y);
                }

                foreach (var z in sections)
                {
                    Section.ExportarToCSV(z);
                }

                foreach (var t in teachers)
                {
                    Teacher.ExportarToCSV(t);
                }

                foreach (var tr in teacherRosters)
                {
                    TeacherRoster.ExportarToCSV(tr);
                }

                try
                {
                    var result = MessageBox.Show("Os arquivos foram gerados com sucesso! Local: C:/Usuários/Público", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (result == DialogResult.OK)
                    {
                        System.Diagnostics.Process.Start(PATH_ARQUIVOS);
                    }
                }
                catch (Exception ex)
                {
                    var ex1 = ex;
                }
            }
            catch (Exception ex)
            {
                var ex2 = ex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                arquivoAluno = dialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog2 = new OpenFileDialog();

            if (dialog2.ShowDialog() == DialogResult.OK)
            {
                arquivoProfessor = dialog2.FileName;
            }
        }
    }
}
