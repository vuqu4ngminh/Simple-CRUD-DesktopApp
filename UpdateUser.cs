using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class UpdateUser : Form
    {
        private string userId;
        public UpdateUser(string _userId)
        {
            InitializeComponent();
            userId = _userId;
            LoadData();
        }

        private async void LoadData()
        {
            string apiUrl = "https://simple-crud-x6b5.onrender.com/api/v1/" + userId;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(apiUrl);

                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void label3_Click(object sender, EventArgs e) => Application.Exit();

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm form1 = new MainForm();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine(userId);
        }
    }
}
