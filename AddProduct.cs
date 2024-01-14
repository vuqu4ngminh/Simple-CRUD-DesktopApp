using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            DisplayProducts displayProducts = new DisplayProducts();
            displayProducts.Show();
            this.Hide();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string price = textBox2.Text;
            string imageUrl = textBox3.Text;
            string description = textBox4.Text;
            string status = (comboBox1.Text == "Còn hàng") ? "1" : "0";
            if(Utils.checkProductData(name,description,price,imageUrl,status) == true)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string apiUrl = "http://localhost:9595/api/v1/product/add";
                        string userDataJson = $"{{\"name\":\"{name}\",\"description\":\"{description}\",\"price\":\"{Convert.ToInt32(price)}\",\"imageUrl\":\"{imageUrl}\",\"status\":\"{status}\"}}";
                        StringContent content = new StringContent(userDataJson, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            MessageBox.Show("Thêm sản phẩm thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
