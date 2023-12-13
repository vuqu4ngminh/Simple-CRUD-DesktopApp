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
            MainForm form1 = new MainForm();
            form1.Show();
            this.Close();
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
                        string apiUrl = "https://simple-crud-x6b5.onrender.com/api/v1/add";
                        string userDataJson = $"{{\"name\":\"{name}\",\"age\":{age}}}";

                        // Tạo nội dung yêu cầu
                        StringContent content = new StringContent(userDataJson, Encoding.UTF8, "application/json");

                        // Gửi yêu cầu POST
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            textBox1.Text = "";
                            textBox2.Text = "";
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
