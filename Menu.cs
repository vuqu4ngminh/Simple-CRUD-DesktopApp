using System;
using System.Windows.Forms;

namespace CRUD_User
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) => Application.Exit();

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayUsers displayUsers = new DisplayUsers();
            displayUsers.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DisplayProducts displayProducts = new DisplayProducts();
            displayProducts.Show();
            this.Hide();
        }
    }
}
