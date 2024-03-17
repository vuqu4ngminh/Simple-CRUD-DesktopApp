using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class DeleteUser : Form
    {
        private string userId;

        public DeleteUser(string _userId)
        {
            InitializeComponent();
            userId = _userId;
            LoadData();
        }
        private void LoadData()
        {
            label2.Text += $" {userId}";
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://mecommerce.live/api/v1/users/delete/" + userId;
                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        DisplayUsers form1 = new DisplayUsers();
                        form1.Show();
                        this.Close();
                        MessageBox.Show("Xóa người dùng thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DisplayUsers form1 = new DisplayUsers();
                    form1.Show();
                    this.Close();
                    
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            DisplayUsers form = new DisplayUsers();
            form.Show();
            this.Close();
        }
    }
}
