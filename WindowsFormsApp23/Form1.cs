using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp23.Models;

namespace WindowsFormsApp23
{
    public partial class Form1 : Form
    {
        private ApplicationDbContext dbContext;

        public Form1()
        {
            InitializeComponent();
            dbContext = new ApplicationDbContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            dataGridView1.CellClick += dataGridView1_CellClick;

        }

        private void LoadData()
        {
            using (var db = new ApplicationDbContext())
            {
                // Load danh sách sinh viên
                var students = db.Students
                                 .Select(s => new
                                 {
                                     s.StudentID,
                                     s.FullName,
                                     s.AverageScore,
                                     FacultyName = s.Faculty.FacultyName
                                 }).ToList();

                // Gán dữ liệu cho DataGridView
                dataGridView1.AutoGenerateColumns = false; // Ngăn tạo cột tự động
                dataGridView1.DataSource = students;

                // Load danh sách khoa vào ComboBox
                cbbKhoa.DataSource = db.Faculties.ToList();
                cbbKhoa.DisplayMember = "FacultyName";
                cbbKhoa.ValueMember = "FacultyID";
            }
        }



        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtScore_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var newStudent = new Student
                {
                    StudentID = txtID.Text,
                    FullName = txtName.Text,
                    AverageScore = double.Parse(txtScore.Text),
                    FacultyID = int.Parse(cbbKhoa.SelectedValue.ToString())
                };

                db.Students.Add(newStudent);
                db.SaveChanges();
                LoadData();
                MessageBox.Show("Thêm sinh viên thành công!");
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var studentID = txtID.Text;
                var student = db.Students.SingleOrDefault(s => s.StudentID == studentID);

                if (student != null)
                {
                    student.FullName = txtName.Text;
                    student.AverageScore = double.Parse(txtScore.Text);
                    student.FacultyID = int.Parse(cbbKhoa.SelectedValue.ToString());

                    db.SaveChanges();
                    LoadData();
                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
                else
                {
                    MessageBox.Show("Sinh viên không tồn tại!");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var studentID = txtID.Text;
                var student = db.Students.SingleOrDefault(s => s.StudentID == studentID);

                if (student != null)
                {
                    db.Students.Remove(student);
                    db.SaveChanges();
                    LoadData();
                    MessageBox.Show("Xóa sinh viên thành công!");
                }
                else
                {
                    MessageBox.Show("Sinh viên không tồn tại!");
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtID.Text = row.Cells["StudentID"].Value.ToString();
                txtName.Text = row.Cells["FullName"].Value.ToString();
                txtScore.Text = row.Cells["AverageScore"].Value.ToString();
                cbbKhoa.Text = row.Cells["FacultyName"].Value.ToString();
            }
        }


    }
}
