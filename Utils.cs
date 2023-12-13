using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_User
{
    internal class Utils
    {
        public static bool checkData(string _name, string _age)
        {
            if (string.IsNullOrEmpty(_name)) return false;
            if (string.IsNullOrEmpty(_age)) return false;

            return true;
        }
    }
}
