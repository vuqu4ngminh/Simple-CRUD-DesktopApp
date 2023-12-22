﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadData();
            button3.Enabled = false;
            button2.Enabled = false;
        }
        private string userId;
        private string username;
        private string userage;
        private async void LoadData()
        {
            string apiUrl = "https://simple-crud-x6b5.onrender.com/api/v1";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(apiUrl);
                    UserData[] userDataArray = JsonConvert.DeserializeObject<UserData[]>(response);

                    dataGridView1.Rows.Clear();
                    foreach (var userData in userDataArray)
                    {
                        dataGridView1.Rows.Add(userData._id, userData.Name, userData.Age);
                    }
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateUser updateUser = new UpdateUser(userId);
            updateUser.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                userId = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                username = dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString();
                userage = dataGridView1.Rows[e.RowIndex].Cells["age"].Value.ToString();
                button3.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser(userId,username,userage);
            deleteUser.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
