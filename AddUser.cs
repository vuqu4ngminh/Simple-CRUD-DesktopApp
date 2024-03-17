using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            DisplayUsers form1 = new DisplayUsers();
            form1.Show();
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string email = textBox2.Text;
            string phone = textBox3.Text;
            string address = textBox4.Text;
            string password = textBox5.Text;
            string role = comboBox1.Text;
            if (Utils.checkUserData(name, email, phone, address, role) == true)
            {
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Không được để trống mật khẩu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string apiUrl = "https://mecommerce.live/api/v1/users/add";
                            string userDataJson = $"{{\"name\":\"{name}\",\"email\":\"{email}\",\"phone\":\"{phone}\",\"address\":\"{address}\",\"password\":\"{Utils.hashPassword(password)}\",\"role\":\"{role}\"}}";

                            StringContent content = new StringContent(userDataJson, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                            if (response.IsSuccessStatusCode)
                            {
                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox3.Text = "";
                                textBox4.Text = "";
                                textBox5.Text = "";
                                MessageBox.Show("Thêm người dùng thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
