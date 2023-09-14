using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Object_Class;
using Data_Access_Layer_Class;

namespace Business_Logic_Layer_Class
{
    public class Admin_BLLClass
    {
        //Admin Methods



        public bool adminLogin(string login, int code)
        {
            //remember to use encryption and decryption methods here


            Admin_DALClass admin_DAL = new Admin_DALClass();
            if (admin_DAL.adminLogin(login, code))
            {
                return true;
            }
            else
                return false;
        }
        public (bool,int) createAccount(CustomerBO customer)
        {
            Admin_DALClass dal = new Admin_DALClass();

            
            //storing information in encrypted form
            customer.Login=encryptData(customer.Login);

            //converting int code to string
            string code = (customer.Password).ToString();
            code = encryptData(code);
            //changing it back to int
            customer.Password = Convert.ToInt32(code);

            //it receives bool and int
            var data = dal.createAccount(customer);

            if (data.Item1)
                return (true, data.Item2);
            else
                return (false, 0);
        }
        //method for encrypting data
        public string encryptData(string login)
        {
            char[] charArray = login.ToCharArray();

            //to store encrypted characters
            char[] encryptedChar = login.ToCharArray();
            int i = 0;
            foreach (char item in charArray)
            {

                //checking if the char is an alphabet or digit
                if ((item >= 'a' && item <= 'z') || (item >= 'A' && item <= 'Z'))
                {
                    //checking if the character is upper case
                    if (char.IsUpper(item))
                    {
                        //taking the ascii of the alphabet
                        int ascii = Convert.ToInt32(item);

                        //formula
                        int encrypted = (90 - ascii) + 65;

                        //converting it back to character and storing it
                        encryptedChar[i] = Convert.ToChar(encrypted);
                        i++;
                    }
                    //checking if the character is lower case 
                    else if (char.IsLower(item))
                    {
                        //taking the ascii of the alphabet
                        int ascii = Convert.ToInt32(item);

                        //formula
                        int encrypted = (122 - ascii) + 97;

                        //converting it back to character and storing it
                        //also changing it to lower case
                        encryptedChar[i] = char.ToLower(Convert.ToChar(encrypted));
                        i++;
                    }
                }

                //checking if the character is a digit
                else if (item >= '0' && item <= '9')
                {

                    //taking the ascii of the digit
                    int ascii = Convert.ToInt32(item);

                    //formula
                    int encrypted = (57 - ascii) + 48;

                    //converting it back to character and storing it
                    encryptedChar[i] = Convert.ToChar(encrypted);
                    i++;
                }

                //for special characters
                else
                {
                    encryptedChar[i] = item;
                    i++;
                }
            }
            //converting char array to string
            string Result = new string(encryptedChar);



            return Result;

        }
        public string customerName(int accountNo)
        {
            Admin_DALClass dal = new Admin_DALClass();
            string name = dal.customerName(accountNo);

            return name;
        }
        public bool deleteAccout(int accountNo)
        {
            Admin_DALClass dal = new Admin_DALClass();
            if (dal.deleteAccout(accountNo))
                return true;
            else
                return false;
        }
        public CustomerBO customerInfo(int accountNo)
        {
            Admin_DALClass dal = new Admin_DALClass();
            //decrypting the login
            CustomerBO customer = new CustomerBO();
            customer = dal.customerInfo(accountNo);

            //encrypting the data again will actually decrypt it
            customer.Login = encryptData(customer.Login);
            //converting int code to string
            string code = (customer.Password).ToString();
            code = encryptData(code);
            //changing it back to int
            customer.Password = Convert.ToInt32(code);
            return customer;
        }
        public bool updateCustomer(CustomerBO customer)
        {
            Admin_DALClass dal = new Admin_DALClass();

            //encrypting login
            customer.Login = encryptData(customer.Login);
            //converting int code to string
            string code = (customer.Password).ToString();
            code = encryptData(code);
            //changing it back to int
            customer.Password = Convert.ToInt32(code);

            if (dal.updateCustomer(customer))
            {
                return true;
            }
            else
                return false;
        }
        public List<CustomerBO> search(int[] arr, object[] objArr, int count)
        {
            //making the dynamic query here based on array data
            string query = string.Empty;

           
            //preparing the query
            query = "select * from Customer_Table where ";

            //forloop will work the number of criteria times

           
            for (int i = 0; i < count; i++)
            {
                //if search criteria contains account number
                if (arr[0] == 1)
                {
                    if(i==count-1)
                    {
                        //if there is only 1 criteria
                        query += $"AccountNumber = @A ";
                    }
                   else
                    {
                        //if there is more than 1 criteria
                        query += $"AccountNumber = @A AND ";
                        i++;
                    }
                }
                //if search criteria contains name
                if (arr[1] == 1)
                {
                    if (i == count-1)
                    {
                        //if there is only 1 criteria
                        query += $"Name = @N ";
                    }
                    else
                    {
                        //if there is more than 1 criteria
                        query += $"Name = @N AND ";
                        i++;
                    }

                }
                //if search criteria contains account type
                if (arr[2] == 1)
                {
                    if (i == count-1)
                    {
                        //if there is only 1 criteria
                        query += $"AccountType = @T ";
                    }
                    else
                    {
                        //if there is more than 1 criteria
                        query += $"AccountType = @T AND ";
                        i++;

                    }
                }
                //if search criteria contains account status
                if (arr[3] == 1)
                {
                    if (i == count-1)
                    {
                        //if there is only 1 criteria
                        query += $"Status = @S ";
                    }
                    else
                    {
                        //if there is more than 1 criteria
                        query += $"Status = @S AND ";
                        i++;
                    }
                }
                //if search criteria contains account balance
                if (arr[4] == 1)
                {
                    if (i == count-1)
                    {
                        //if there is only 1 criteria
                        query += $"Balance = @B ";
                    }
                    else
                    {
                        //if there is more than 1 criteria
                        query += $"Balance = @B AND ";
                        i++;
                    }
                }
            }
            
            Admin_DALClass admin_DAL = new Admin_DALClass();

            //first receiving this object list in bll
            List<CustomerBO> list = new List<CustomerBO>();
            list = admin_DAL.search(query, objArr);

            //then transferring it to PL
            return list;
        }
        //view report by amount
        public List<CustomerBO> searchByAmount(int min, int max)
        {
            Admin_DALClass admin_DAL = new Admin_DALClass();

            //first receiving this object list in bll
            List<CustomerBO> list = new List<CustomerBO>();
            list = admin_DAL.searchByAmount(min, max);
           
            return list;
        }
        //view reports by date
        public List<TransactionBO> searchByDate(DateTime StartDate, DateTime EndDate)
        {
            Admin_DALClass admin_DAL =new Admin_DALClass();
            List<TransactionBO> transactions = new List<TransactionBO>();

            //first receiving in BLL
            transactions = admin_DAL.searchByDate(StartDate, EndDate);
            return transactions;
        }
        public bool insertTransactionInfo(int accountNo, int Amount, DateTime date, string Type, string Name)
        {
            Admin_DALClass admin_DAL = new Admin_DALClass();
            if(admin_DAL.insertTransactionInfo(accountNo, Amount, date, Type, Name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<TransactionBO> getTransactionInfo(int accountNo)
        {
            Admin_DALClass admin_DAL = new Admin_DALClass();
            List<TransactionBO> list = new List<TransactionBO>();
            list = admin_DAL.getTransactionInfo(accountNo);
            return list;    
        }
    }
    public class Customer_BLLClass
    {

        //customer methods

        //method for decrypting data
        public string decryptData(string login)
        {
            char[] charArray = login.ToCharArray();

            //to store encrypted characters
            char[] decryptedChar = login.ToCharArray();
            int i = 0;
            foreach (char item in charArray)
            {

                //checking if the char is an alphabet or digit
                if ((item >= 'a' && item <= 'z') || (item >= 'A' && item <= 'Z'))
                {
                    //checking if the character is upper case
                    if (char.IsUpper(item))
                    {
                        //taking the ascii of the alphabet
                        int ascii = Convert.ToInt32(item);

                        //formula
                        int decrypted = (90 - ascii) + 65;

                        //converting it back to character and storing it
                        decryptedChar[i] = Convert.ToChar(decrypted);
                        i++;
                    }
                    //checking if the character is lower case 
                    else if (char.IsLower(item))
                    {
                        //taking the ascii of the alphabet
                        int ascii = Convert.ToInt32(item);

                        //formula
                        int decrypted = (122 - ascii) + 97;

                        //converting it back to character and storing it
                        //also changing it to lower case
                        decryptedChar[i] = char.ToLower(Convert.ToChar(decrypted));
                        i++;
                    }
                }

                //checking if the character is a digit
                else if (item >= '0' && item <= '9')
                {

                    //taking the ascii of the digit
                    int ascii = Convert.ToInt32(item);

                    //formula
                    int decrypted = (57 - ascii) + 48;

                    //converting it back to character and storing it
                    decryptedChar[i] = Convert.ToChar(decrypted);
                    i++;
                }

                //for special characters
                else
                {
                    decryptedChar[i] = item;
                    i++;
                }
            }
            //converting char array to string
            string Result = new string(decryptedChar);



            return Result;

        }
       
        public (bool, int, string) customerLogin(string login, int code)
        {
            Customer_DALClass customer_DAL = new Customer_DALClass();

            //encrypting the login to match with db
            login = decryptData(login);

            //converting int code to string
            string pass = code.ToString();
            pass = decryptData(pass);
            //changing it back to int
            code = Convert.ToInt32(pass);
            var data = customer_DAL.customerLogin(login, code);

            //checks true or false
            if (data.Item1)
            {
                //returns true and accountNumber
                return (true, data.Item2, data.Item3);
            }
            else
                return (false, 0, null);
        }
        public (int, decimal) displayBalance(int accountNo)
        {
            Customer_DALClass dal = new Customer_DALClass();
            return dal.displayBalance(accountNo);
        }
        public bool depositCash(int accountNo, decimal amount)
        {
            //fetching the old balance to add new balance in it

            Customer_DALClass dal = new Customer_DALClass();
            var data = dal.displayBalance(accountNo);
            decimal newAmount = amount + data.Item2;
            return (dal.depositCash(accountNo, newAmount));
        }
        public bool cashTransfer(int senderAccountNo, int receiverAccountNo, decimal NewSenderAmount, decimal NewReceiverAmount)
        {
            Customer_DALClass dal = new Customer_DALClass();
            if (dal.cashTransfer(senderAccountNo, receiverAccountNo, NewSenderAmount, NewReceiverAmount))
            {
                return true;
            }
            else
                return false;
        }
        public bool withdrawCash(int accountNo, decimal amount)
        {
            //fetching the old balance to subtract the amount from it

            Customer_DALClass dal = new Customer_DALClass();
            var data = dal.displayBalance(accountNo);
            decimal newAmount = data.Item2 - amount; 
            return (dal.withdrawCash(accountNo, newAmount));
        }

    }
}
