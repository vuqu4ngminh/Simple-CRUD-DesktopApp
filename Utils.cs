using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRUD_User
{
    internal class Utils
    {
        public static bool checkData(string _name, string _age)
        {
            if (string.IsNullOrEmpty(_name))
            {
                MessageBox.Show("Không được để trống tên", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(_age))
            {
                MessageBox.Show("Không được để trống tuổi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (int.TryParse(_age, out int age) == false)
            {
                MessageBox.Show("Tuổi không đúng định dạng", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } else
            {
                if (age >= 1)
                {
                    return true;
                } else
                {
                    MessageBox.Show("Tuổi phải lớn hơn 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
