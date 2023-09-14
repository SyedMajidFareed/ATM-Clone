using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Business_Object_Class;


namespace Data_Access_Layer_Class
{
    public class Admin_DALClass
    {
        //Admin Methods
        public bool adminLogin(string login, int code)
        {
            //using parametrized query
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection connection = new SqlConnection(connectionString);
            string query = $"select * from Admin_Table where UserName = @U and Password = @P";

            //defining parameters
            SqlParameter p1 = new SqlParameter("U", login);
            SqlParameter p2 = new SqlParameter("P", code);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);

            //adding parameters
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                connection.Close();

                return true;
            }
            else
            {
                connection.Close();
                return false;

            }

        }
        public (bool,int) createAccount(CustomerBO customer)
        {
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";;

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to insert data
            string query = $"insert into Customer_Table(Login, Password, Name, AccountType, Balance, Status) " +
                           $"values('{customer.Login}','{customer.Password}','{customer.Name}','{customer.Type}','{customer.Balance}','{customer.Status}')";
            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            int alteredrows = cmd.ExecuteNonQuery();
            if (alteredrows >= 1)
            {
                //sending back the recent account number
                //updating query
                query = $"select AccountNo From Customer_Table where (Login = '{customer.Login}' and Password = '{customer.Password}' and Name = '{customer.Name}' and AccountType = '{customer.Type}' and Balance = '{customer.Balance}' and Status = '{customer.Status}')";
                SqlCommand cmd1 = new SqlCommand(query, connection);
                SqlDataReader dr = cmd1.ExecuteReader();
                dr.Read();
                int accountno = Convert.ToInt32(dr[0]);
                connection.Close();

                return (true, accountno);

            }
            else
            {
                connection.Close();

                return (false, 0);
            }

        }

        public bool deleteAccout(int accountNo)
        {
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";;

            SqlConnection connection = new SqlConnection(connectionString);

            

            //writing a query to delete data
            string query = $"delete from Customer_Table where AccountNo = @A";

            //defining parameters
            SqlParameter p1 = new SqlParameter("A", accountNo);

            SqlCommand cmd = new SqlCommand(query, connection);

            //adding parameter
            cmd.Parameters.Add(p1);
            connection.Open();
            int alteredrows = cmd.ExecuteNonQuery();
            if (alteredrows >= 1)
            {
                
                connection.Close();

                return true;
            }
            else
            {
                connection.Close();

                return false;
            }

        }
        public string customerName(int accountNo)
        {
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";;

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to fetch data
            string query = $"select Name from Customer_Table where AccountNo = @A";
            SqlCommand cmd = new SqlCommand(query, connection);
            //defining parameters
            SqlParameter p1 = new SqlParameter("A", accountNo);

            //adding parameter
            cmd.Parameters.Add(p1);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
           
            if (dr.Read())
            {
               

                string RETURN = dr[0].ToString();
                connection.Close();
                return RETURN;
            }
            else
            {
                connection.Close();
                return null;

            }
        }
        public CustomerBO customerInfo(int accountNo)
        {
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";;

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to fetch data
            string query = $"select AccountNo , Login, Password, Name, AccountType, Balance, Status from Customer_Table where AccountNo = @A";
            SqlCommand cmd = new SqlCommand(query, connection);
            //defining parameters
            SqlParameter p1 = new SqlParameter("A", accountNo);

            //adding parameter
            cmd.Parameters.Add(p1);

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                CustomerBO customer = new CustomerBO();

                customer.AccountNo = Convert.ToInt32(dr[0]);
                customer.Login = Convert.ToString(dr[1]);
                customer.Password = Convert.ToInt32(dr[2]);
                customer.Name = Convert.ToString(dr[3]);
                customer.Type = Convert.ToString(dr[4]);
                customer.Balance = Convert.ToDecimal(dr[5]);
                customer.Status = Convert.ToString(dr[6]);
                connection.Close();

                return customer;
            }
           else
            {
                return null;
            }    
        }
        public bool updateCustomer(CustomerBO customer)
        {
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";;

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to fetch data
            string query = $"Update Customer_Table set Login = '{customer.Login}' ,Name = '{customer.Name}', AccountType = '{customer.Type}', Password='{customer.Password}', Status='{customer.Status}' where AccountNo=@A";
            SqlCommand cmd = new SqlCommand(query, connection);
            //defining parameters
            SqlParameter p1 = new SqlParameter("A", customer.AccountNo);

            //adding parameter
            cmd.Parameters.Add(p1);
            connection.Open();
            int alteredrows = cmd.ExecuteNonQuery();
            if (alteredrows >= 1)
            {
                
                connection.Close();

                return true;
            }
            else
            {
                connection.Close();

                return false;
            }

        }
        //search by criteria
        public List<CustomerBO> search(string query, object[] objArr)
        {
            List<CustomerBO> list = new List<CustomerBO>();
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; ;

            SqlConnection connection = new SqlConnection(connectionString);

            //sending the query from bll to dal to fetch data
            SqlCommand cmd = new SqlCommand(query, connection);
            //assigning parameters
            //defining parameters
            SqlParameter AccountNo = new SqlParameter("A", objArr[0]);
            SqlParameter Name = new SqlParameter("N", objArr[1]);
            SqlParameter Type = new SqlParameter("T", objArr[2]);
            SqlParameter Status = new SqlParameter("S", objArr[3]);
            SqlParameter Balance = new SqlParameter("B", objArr[4]);

            //adding parameters
            cmd.Parameters.Add(AccountNo);
            cmd.Parameters.Add(Name);
            cmd.Parameters.Add(Type);
            cmd.Parameters.Add(Status);
            cmd.Parameters.Add(Balance);
            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CustomerBO customer = new CustomerBO();

                customer.AccountNo = Convert.ToInt32(dr[0]);
                customer.Name = Convert.ToString(dr[3]);
                customer.Type = Convert.ToString(dr[4]);
                customer.Balance = Convert.ToDecimal(dr[5]);
                customer.Status = Convert.ToString(dr[6]);
                list.Add(customer);
            }
            connection.Close();
            return list;
        }

        //view report by Amount function
        public List<CustomerBO> searchByAmount(int min, int max)
        {
            List<CustomerBO> list = new List<CustomerBO>();
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; ;

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to fetch data
            string query = $"select AccountNo , Name, AccountType, Balance, Status from Customer_Table where (Balance >= @Min AND Balance <= @Max)";
            SqlCommand cmd = new SqlCommand(query, connection);
            //defining parameters
            SqlParameter p1 = new SqlParameter("Min", min);
            SqlParameter p2 = new SqlParameter("Max", max);

            //adding parameter
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                CustomerBO customer = new CustomerBO();

                customer.AccountNo = Convert.ToInt32(dr[0]);
                customer.Name = Convert.ToString(dr[1]);
                customer.Type = Convert.ToString(dr[2]);
                customer.Balance = Convert.ToDecimal(dr[3]);
                customer.Status = Convert.ToString(dr[4]);
                list.Add(customer);
            }
            connection.Close();
            return list;
        }
        public List<TransactionBO> searchByDate(DateTime StartDate, DateTime EndDate)
        {
            List<TransactionBO> list = new List<TransactionBO>();
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; ;

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to fetch data
            string query = $"select AccountNo , Name, Transaction Type, Amount, Date from Transaction_Table where (Date >= @Min AND Date <= @Max)";
            SqlCommand cmd = new SqlCommand(query, connection);
            //defining parameters
            SqlParameter p1 = new SqlParameter("Min", StartDate);
            SqlParameter p2 = new SqlParameter("Max", EndDate);

            //adding parameter
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TransactionBO transaction = new TransactionBO();

                transaction.AccountNo = Convert.ToInt32(dr[0]);
                transaction.Name = Convert.ToString(dr[1]);
                transaction.Type = Convert.ToString(dr[2]);
                transaction.Amount = Convert.ToInt32(dr[3]);
                transaction.Date = Convert.ToDateTime(dr[4]);
                list.Add(transaction);
            }
            connection.Close();
            return list;
        }
        public bool insertTransactionInfo(int accountNo, int Amount, DateTime date, string Type, string Name)
        {
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; ;

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to insert data
            string query = $"insert into Transaction_Table(AccountNo , Name, Transaction_Type, Amount, Date) " +
                           $"values('{accountNo}','{Name}','{Type}','{Amount}','{date}')";
            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            int alteredrows = cmd.ExecuteNonQuery();
            if (alteredrows >= 1)
            {
                connection.Close();

                return true;

            }
            else
            {
                connection.Close();

                return false;
            }

        }
        public List<TransactionBO> getTransactionInfo(int accountNo)
        {
            List<TransactionBO> list = new List<TransactionBO>(); 
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; ;

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to fetch data
            string query = $"select Transaction_Type, Amount, Date from Transaction_Table where AccountNo =@A";
            SqlCommand cmd = new SqlCommand(query, connection);
            //defining parameters
            SqlParameter p1 = new SqlParameter("A", accountNo);

            //adding parameter
            cmd.Parameters.Add(p1);

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TransactionBO customer = new TransactionBO();

                customer.Type = Convert.ToString(dr[0]);
                customer.Amount = Convert.ToInt32(dr[1]);
                customer.Date = Convert.ToDateTime(dr[2]);
                list.Add(customer);
            }
            connection.Close();
            return list;
        }
    }

    //customer methods
    public class Customer_DALClass
    {
        public (bool, int, string) customerLogin(string login, int code)
        {
            //using parametrized query
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";;

            SqlConnection connection = new SqlConnection(connectionString);
            string query = $"select * from Customer_Table where Login = @U and Password = @P";

            //defining parameters
            SqlParameter p1 = new SqlParameter("U", login);
            SqlParameter p2 = new SqlParameter("P", code);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);

            //adding parameters
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //fetching account number and also status (to check if disabled)
                int accNo = Convert.ToInt32(dr[0]);
                string status = Convert.ToString(dr[6]);
                connection.Close();

                return (true, accNo, status);
            }
            else
            {
                connection.Close();
                return (false, 0, null);

            }

        }
        public (int, decimal) displayBalance(int accountNo)
        {

            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";;

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to fetch data
            string query = $"select AccountNo ,Balance from Customer_Table where AccountNo = @A";
            SqlCommand cmd = new SqlCommand(query, connection);

            //defining parameters
            SqlParameter p1 = new SqlParameter("A", accountNo);

            //adding parameter
            cmd.Parameters.Add(p1);

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int accNo = Convert.ToInt32(dr[0]);
            decimal bal = Convert.ToDecimal(dr[1]);
            connection.Close();
            return (accNo, bal);
        }

        public bool depositCash(int accountNo, decimal amount)
        {
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";;

            SqlConnection connection = new SqlConnection(connectionString);

            //writing a query to update data
            string query = $"update Customer_Table set Balance = '{amount}' where AccountNo = @A";
            SqlCommand cmd = new SqlCommand(query, connection);
            //defining parameters
            SqlParameter p1 = new SqlParameter("A", accountNo);

            //adding parameter
            cmd.Parameters.Add(p1);
            connection.Open();
            int alteredrows = cmd.ExecuteNonQuery();
            if (alteredrows >= 1)
            {
                connection.Close();

                return true;

            }
            else
            {
                connection.Close();

                return false;
            }

        }

        public bool cashTransfer(int senderAccountNo, int receiverAccountNo, decimal NewSenderAmount, decimal NewReceiverAmount)
        {
            //for sender
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";;

            SqlConnection connection = new SqlConnection(connectionString);

            //updating the sender balance
            //deducting from sender and adding into receiver's account balance

            //writing a query to update data
            string query = $"Update Customer_Table set Balance = '{NewSenderAmount}' where AccountNo = @SA";
            SqlCommand cmd = new SqlCommand(query, connection);
            //defining parameters
            SqlParameter p1 = new SqlParameter("SA", senderAccountNo);

            //adding parameter
            cmd.Parameters.Add(p1);
            connection.Open();
            int alteredrows = cmd.ExecuteNonQuery();
            bool temp = false;
            if (alteredrows >= 1)
            {
                connection.Close();

                temp = true;

            }
            else
            {
                connection.Close();

                temp = false;
            }

            //for receiver

            //updating the Receiver balance
            //writing a query to update data
            string query2 = $"Update Customer_Table set Balance = '{NewReceiverAmount}' where AccountNo = @RA";
            SqlCommand cmd2 = new SqlCommand(query2, connection);
            //defining parameters
            SqlParameter p2 = new SqlParameter("RA", receiverAccountNo);

            //adding parameter
            cmd2.Parameters.Add(p2);
            connection.Open();
            int alteredrows2 = cmd2.ExecuteNonQuery();
            bool temp2 = false;
            if (alteredrows >= 1)
            {
                temp2 = true;

            }
            else
            {
                temp2 = false;
            }
            if (temp && temp2)
            {
                connection.Close();

                return true;
            }
            else
            {
                connection.Close();

                return false;
            }
        }
        public bool withdrawCash(int accountNo, decimal amount)
        {
            //establishing connection with database
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ATM_DATABASE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";;

            SqlConnection connection = new SqlConnection(connectionString);

            //updating the customer balance


            //writing a query to update data
            string query = $"Update Customer_Table set Balance = '{amount}' where AccountNo = @A";
            SqlCommand cmd = new SqlCommand(query, connection);
            //defining parameters
            SqlParameter p1 = new SqlParameter("A", accountNo);

            //adding parameter
            cmd.Parameters.Add(p1);
            connection.Open();
            int alteredrows = cmd.ExecuteNonQuery();

            if (alteredrows >= 1)
            {
                connection.Close();

                return true;

            }
            else
            {
                connection.Close();

                return false;
            }
        }
    }


}
