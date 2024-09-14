using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Business.Entities
{
    public class Account
    {
        public string email { get; set; }
        public string name { get; set; }
        public string password { get; set; }


        public Account(string email, string name, string wachtwoord)
        {
            this.email = email;
            this.name = name;
            this.password = wachtwoord;
        }
    }
}
