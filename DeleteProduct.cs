using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class DeleteProduct : Form
    {
        private int productId;
        public DeleteProduct(int _id)
        {
            InitializeComponent();
            productId = _id;
            LoadData();
        }
        private void LoadData()
        {
            label2.Text += $" {productId}";
        }
        private void label3_Click(object sender, EventArgs e)
        {
            DisplayProducts form = new DisplayProducts();
            form.Show();
            this.Hide();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://mecommerce.live/api/v1/product/delete/" + productId;
                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        DisplayProducts form = new DisplayProducts();
                        form.Show();
                        this.Hide();
                        MessageBox.Show("Xóa sản phẩm thành công", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
