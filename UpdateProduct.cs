using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class UpdateProduct : Form
    {
        private int productId;
        public UpdateProduct(int _id)
        {
            InitializeComponent();
            productId = _id;
            LoadData();
        }
        public class Product
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string imageUrl { get; set; }
            public int price { get; set; }
            public string status { get; set; }
        }
        private async void LoadData()
        {
            string apiUrl = "http://localhost:9595/api/v1/product/" + productId;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(apiUrl);
                    Product[] productArray = JsonConvert.DeserializeObject<Product[]>(response);

                    foreach (var product in productArray)
                    {
                        string statusText = (product.status == "1") ? "Còn hàng" : "Hết hàng";
                        textBox1.Text = product.name;
                        textBox2.Text = Convert.ToString(product.price);
                        textBox3.Text = product.description;
                        textBox4.Text = product.imageUrl;
                        comboBox1.Text = statusText;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string description = textBox3.Text;
            string price = textBox2.Text;
            string imageUrl = textBox4.Text;
            string status = (comboBox1.Text == "Còn hàng") ? "1" : "0";
            if (Utils.checkProductData(name, description, price, imageUrl, status) == true)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string apiUrl = "http://localhost:9595/api/v1/product/update";
                        string userDataJson = $"{{\"id\":\"{productId}\",\"name\":\"{name}\",\"description\":\"{description}\",\"price\":\"{Convert.ToInt32(price)}\",\"imageUrl\":\"{imageUrl}\",\"status\":\"{status}\"}}";
                        StringContent content = new StringContent(userDataJson, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            DisplayProducts form1 = new DisplayProducts();
                            form1.Show();
                            this.Close();
                            MessageBox.Show("Cập nhật sản phẩm thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            DisplayProducts form = new DisplayProducts();
            form.Show();
            this.Hide();
        }
    }
}
