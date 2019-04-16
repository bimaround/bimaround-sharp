using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMAroundClient.ObjectModel.Users
{
    public class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string fatherName { get; set; }
        public string accountType { get; set; }
    }
}
