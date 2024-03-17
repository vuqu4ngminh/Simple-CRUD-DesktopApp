using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class UpdateUser : Form
    {
        private string userId;
        public class User
        {
            [JsonProperty("_id")]
            public string Id {  get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Role { get; set; }
        }
        public UpdateUser(string _userId)
        {
            InitializeComponent();
            userId = _userId;
            LoadData();
        }

        private async void LoadData()
        {
            string apiUrl = "https://mecommerce.live/api/v1/users/" + userId;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(apiUrl);
                    User user = JsonConvert.DeserializeObject<User>(response);
                    textBox1.Text = user.Name;
                    textBox2.Text = user.Email;
                    textBox3.Text = user.Phone;
                    textBox4.Text = user.Address;
                    comboBox1.Text = user.Role;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
            string role = comboBox1.Text;
            if (Utils.checkUserData(name, email, phone, address, role) == true)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string apiUrl = "https://mecommerce.live/api/v1/users/update";
                        string userDataJson = $"{{\"id\":\"{userId}\",\"name\":\"{name}\",\"email\":\"{email}\",\"phone\":\"{phone}\",\"address\":\"{address}\",\"role\":\"{role}\"}}";

                        StringContent content = new StringContent(userDataJson, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            DisplayUsers form1 = new DisplayUsers();
                            form1.Show();
                            this.Close();
                            MessageBox.Show("Cập nhật người dùng thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
