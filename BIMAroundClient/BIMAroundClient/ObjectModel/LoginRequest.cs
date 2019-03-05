using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMAroundClient.ObjectModel
{
    class LoginRequest
    {
        public string login { get; set; }//не менять на на заглавную, иначе
        public string password { get; set; }//в api не парсится json
    }
}
