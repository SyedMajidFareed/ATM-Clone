using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation_Layer;

namespace Assigment01
{
    public class Program
    {
        static void Main(string[] args)
        {
            //there is a hardcoded admin in admin table use that or add new one if you want to'
            //login is admin
            //password is 123

            //database is available in Assignment01 folder
            Admin_PLClass admin_PL = new Admin_PLClass();
            admin_PL.generalMenu();
           
        }
    }
}

