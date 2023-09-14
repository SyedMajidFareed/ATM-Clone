using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Object_Class
{
    public class AdminBO
    {
        static int Admin_id;
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class CustomerBO
    {
        public int AccountNo { get; set; }
        public string Login { get; set; }
        public int Password { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; }

    }
    public class TransactionBO
    {
        public int AccountNo { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
