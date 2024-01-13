using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace CRUD_User
{
    internal class Utils
    {
        // check user data value
        public static bool checkUserData(string _name, string _email, string _phone, string _address, string _role)
        {
            if (string.IsNullOrEmpty(_name))
            {
                MessageBox.Show("Không được để trống tên", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(_email))
            {
                MessageBox.Show("Không được để trống email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } else
            {
                string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
                Regex regex = new Regex(pattern);

                if (regex.IsMatch(_email) == false)
                {
                    MessageBox.Show("Định dạng email không chính xác", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (string.IsNullOrEmpty(_address))
            {
                MessageBox.Show("Không được để trống địa chỉ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(_role))
            {
                MessageBox.Show("Không được để trống Role", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(_phone))
            {
                MessageBox.Show("Không được để trống số điện thoại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } else
            {
                string pattern = @"^0\d{9}$";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(_phone) == true)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Số điện thoại không đúng định dạng", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // hash password
        public static string hashPassword(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
