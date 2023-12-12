using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadData();
        }
        private async void LoadData()
        {
            string apiUrl = "https://simple-crud-x6b5.onrender.com/api/v1";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(apiUrl);

                    // Chuyển đổi JSON thành mảng đối tượng (UserData[])
                    UserData[] userDataArray = JsonConvert.DeserializeObject<UserData[]>(response);

                    // Gán mảng đối tượng vào DataSource của DataGridView
                    dataGridView1.DataSource = userDataArray;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        public class UserData
        {
            public string _id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e) => Application.Exit();
    }
}
