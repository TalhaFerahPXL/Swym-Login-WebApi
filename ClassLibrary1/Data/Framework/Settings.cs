using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLibrary1.Data.Framework
{
    public static class Settings
    {
        public static string GetConnectionString()
        {

            return "Data Source = briskdemo.database.windows.net; Initial Catalog = Brisk; User ID = talha; Password = !!brisk30.11; Connect Timeout = 30; Encrypt = True;\r\n";
            //return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Brisk;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        }


    } 
}
