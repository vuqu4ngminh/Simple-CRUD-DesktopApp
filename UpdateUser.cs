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
            public string Age { get; set; }
        }
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
                    User user = JsonConvert.DeserializeObject<User>(response);
                    textBox1.Text = user.Name;
                    textBox2.Text = user.Age;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {
            MainForm form1 = new MainForm();
            form1.Show();
            this.Hide();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string age = textBox2.Text;
            if(Utils.checkData(name, age) == true)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string apiUrl = "https://simple-crud-x6b5.onrender.com/api/v1/update";
                        string userDataJson = $"{{\"id\":\"{userId}\",\"name\":\"{name}\",\"age\":{age}}}";

                        StringContent content = new StringContent(userDataJson, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Cập nhật thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } else
            {
                MessageBox.Show("Không được để trống thông tin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
