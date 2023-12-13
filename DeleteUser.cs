using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class DeleteUser : Form
    {
        private string userId;
        private string name;
        private string age;
        public DeleteUser(string _userId, string _name, string _age)
        {
            InitializeComponent();
            userId = _userId;
            name = _name;
            age = _age;
            LoadData();
        }
        private void LoadData()
        {
            label2.Text += $" {userId}";
            label4.Text += $" {name}";
            label5.Text += $" {age}";
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://simple-crud-x6b5.onrender.com/api/v1/delete/" + userId;
                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        MainForm form1 = new MainForm();
                        form1.Show();
                        this.Close();
                        MessageBox.Show("Xóa người dùng thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MainForm form1 = new MainForm();
                    form1.Show();
                    this.Close();
                    
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MainForm form = new MainForm();
            form.Show();
            this.Close();
        }
    }
}
