using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class DisplayProducts : Form
    {
        public DisplayProducts()
        {
            InitializeComponent();
            LoadData();
            button3.Enabled = false;
            button2.Enabled = false;
        }
        private int productId;
        public class Product
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public int price { get; set; }
            public string status { get; set; }
        }
        private async void LoadData()
        {
            string apiUrl = "https://mecommerce.live/api/v1/product/";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(apiUrl);
                    Product[] productArray = JsonConvert.DeserializeObject<Product[]>(response);

                    dataGridView1.Rows.Clear();

                    foreach (var product in productArray)
                    {
                        string statusText = (product.status == "1") ? "Còn hàng" : "Hết hàng";
                        dataGridView1.Rows.Add(product.id, product.name, product.price, product.description, statusText);
                    }

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e) => Application.Exit();

        private void button4_Click(object sender, EventArgs e)
        {
            Menu form = new Menu();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateProduct form = new UpdateProduct(productId);
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteProduct form = new DeleteProduct(productId);
            form.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
                button3.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProduct form = new AddProduct();
            form.Show();
            this.Hide();
        }
    }
}
