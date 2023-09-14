using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Logic_Layer_Class;
using Business_Object_Class;

namespace Presentation_Layer
{
    public class Admin_PLClass
    {
        //general menu
        public void generalMenu()
        {
            bool check = true;
            while (check)
            {
                Console.Clear();
                Console.WriteLine("~~ATM~~");
                Console.WriteLine();
                Console.WriteLine("1. Log in as Admin\n" +
                    "2. Log in as Customer\n" +
                    "3. Exit Application");
                bool checkchoose = int.TryParse(Console.ReadLine(), out int choose);
                if (checkchoose)
                {
                    switch (choose)
                    {
                        case 1:
                            {
                                Console.Clear();
                                Console.WriteLine("~~Admin Login~~");
                                Console.WriteLine();
                                adminLogin();
                            }
                            break;
                        case 2:
                            {
                                Console.Clear();
                                Console.WriteLine("~~Customer Login~~");
                                Console.WriteLine();
                                Customer_PLClass customer_PL = new Customer_PLClass();
                                customer_PL.customerLogin();
                            }
                            break;
                        case 3:
                            {
                                check = false;
                            }
                            break;
                        default:
                            {
                                Console.WriteLine("NOT A VALID CHOICE!");
                                Console.WriteLine();
                                Console.WriteLine("Press Enter to get back to menu");
                                Console.ReadLine();
                            }
                            break;
                    }
                }
            }
        }

        public void adminLogin()
        {
            bool check = true;
            while (check)
            {
                Console.WriteLine("Enter Login: ");
                string login = Console.ReadLine();


                Console.WriteLine("Enter PinCode: ");
                //checking if the code is entered in correct data type
                bool checkcode = int.TryParse(Console.ReadLine(), out int code);
                if (checkcode)
                {
                    Admin_BLLClass admin_BLL = new Admin_BLLClass();
                    if (admin_BLL.adminLogin(login, code))
                    {
                        

                        Console.WriteLine("Login Successfull");
                        adminMenu();

                        //going out of for loop
                        check = false;
                        //just in case
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Failed to login\n" +
                            "Please enter correct information");
                        
                    }

                }
                else
                    Console.WriteLine("Invalid Data Type");
            }
        }
        public void adminMenu()
        {

            bool check = true;
            while (check)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("~~Admin Menu~~");
                Console.WriteLine();
                Console.WriteLine("1. Create New Account\n" +
                                  "2. Delete Existing Account\n" +
                                  "3. Update Account Information\n" +
                                  "4. Search for Accounts\n" +
                                  "5. View Reports\n" +
                                  "6. Exit\n"
                                 );

                bool temp = int.TryParse(Console.ReadLine(), out int choice);
                if (temp)
                {
                    switch (choice)
                    {
                        case 1:
                            {
                                CustomerBO customer = new CustomerBO();

                                Console.Clear();
                                Console.WriteLine("~~Create a New Account~~");
                                Console.WriteLine();
                                Console.WriteLine("Login: ");
                                string login = Console.ReadLine();

                                //checking if the login contains something
                                if(login.Length==0)
                                {
                                    Console.WriteLine("You must enter a login");
                                    Console.WriteLine("failed to add");
                                    Console.WriteLine();
                                    Console.WriteLine("Press enter to get to the menu");
                                    Console.ReadLine();
                                    break;
                                }

                                customer.Login = login;

                                //validating inputs
                                Console.WriteLine("PinCode: ");
                                bool codecheck = int.TryParse(Console.ReadLine(), out int code);

                                //checking datatype and length of code
                                if (codecheck && (code.ToString().Length==5))
                                {
                                    customer.Password = code;

                                    Console.WriteLine("Holder's Name: ");
                                    string name = Console.ReadLine();
                                    if (name.Length == 0)
                                    {
                                        Console.WriteLine("You must enter a name");
                                        Console.WriteLine("failed to add");
                                        Console.WriteLine();
                                        Console.WriteLine("Press enter to get to the menu");
                                        Console.ReadLine();
                                        break;
                                    }
                                    customer.Name = name;

                                    Console.WriteLine("Type (Savings, Current): ");

                                    customer.Type = Console.ReadLine();
                                    //checking if the Type contains something
                                    if ((customer.Type).Length == 0)
                                    {
                                        Console.WriteLine("You must enter Account Type");
                                        Console.WriteLine("failed to add");
                                        Console.WriteLine();
                                        Console.WriteLine("Press enter to get to the menu");
                                        Console.ReadLine();
                                        break;
                                    }
                                    if (customer.Type == "Savings" || customer.Type == "Current" || customer.Type == "savings" || customer.Type == "current")
                                    {
                                        Console.WriteLine("Status: ");


                                        customer.Status = Console.ReadLine();
                                        //checking if the Status contains something
                                        if ((customer.Status).Length == 0)
                                        {
                                            Console.WriteLine("You must enter a Status");
                                            Console.WriteLine("failed to add");
                                            Console.WriteLine();
                                            Console.WriteLine("Press enter to get to the menu");
                                            Console.ReadLine();
                                            break;
                                        }
                                        if (customer.Status == "Active" || customer.Status == "active" || customer.Status == "Disabled" || customer.Status == "disabled")
                                        {
                                            Console.WriteLine("Starting Balance: ");
                                            bool checkbalance = decimal.TryParse(Console.ReadLine(), out decimal balance);

                                            //checking if the type is decimal and also it is 500 atleast
                                            if (checkbalance && balance >= 500)
                                            {
                                                customer.Balance = balance;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid Data Type or balance is less than Rs. 500");
                                                Console.WriteLine();
                                                Console.WriteLine("Press Enter to continue");
                                                Console.ReadLine();
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please select either active or disabled\n" +
                                                  "failed to add");
                                            Console.WriteLine();
                                            Console.WriteLine("Press Enter to continue");
                                            Console.ReadLine();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please choose either savings or current\n" +
                                              "Failed to add");
                                        Console.WriteLine();
                                        Console.WriteLine("Press Enter to continue");
                                        Console.ReadLine();
                                        break;
                                    }
                                    
                                }
                                else
                                {

                                    Console.WriteLine("invalid Data Type or pin length is not 5");
                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to continue");
                                    Console.ReadLine();
                                    break;
                                }


                                //passing this information to bll
                                

                                Admin_BLLClass bll = new Admin_BLLClass();

                                //it receives bool and int
                                var data = bll.createAccount(customer);

                                if (data.Item1)
                                {
                                    Console.WriteLine($"\nAccount Created Successfully!\nAccount Number Assigned is {data.Item2}");
                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to continue");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("\nFailed to Create Account!!");

                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to continue");
                                    Console.ReadLine();
                                }
                            }
                            break;
                        case 2:
                            {
                                Console.Clear();
                                Console.WriteLine("~~Delete an Account~~");
                                Console.WriteLine();
                                Console.WriteLine("Enter the Account Number to DELETE: ");


                                bool CheckaccountNo = int.TryParse(Console.ReadLine(), out int accountNo);
                                if (CheckaccountNo)
                                {


                                    //fetching the account holder name and also checking if the account exists or not
                                    Admin_BLLClass bll = new Admin_BLLClass();


                                    //this has to be done in bll
                                    string name = bll.customerName(accountNo);
                                    if (name == null)
                                    {
                                        Console.WriteLine($"Account with Account Number: {accountNo} does not exist!");
                                        Console.WriteLine();
                                        Console.WriteLine("Press Enter to continue");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine($"You wish to delete the account held by {name}; " +
                                            $"If this information is correct please re - enter the account number: ");

                                        bool checkNewAccountNo = int.TryParse(Console.ReadLine(),out int NewAccountNo);

                                        //checking if the re entered number is in correct datatype
                                        if(!checkNewAccountNo)
                                        {
                                            Console.WriteLine("Invalid Data Type");
                                            Console.WriteLine("failed to Delete");
                                            Console.WriteLine();
                                            Console.WriteLine("Press enter to get to the menu");
                                            Console.ReadLine();
                                            break;
                                        }
                                        //checking if the re-entered number matches the previous one
                                        if (NewAccountNo == accountNo)
                                        {
                                            if (bll.deleteAccout(accountNo))
                                            {
                                                Console.WriteLine("Account Deleted Successfully");

                                                Console.WriteLine();
                                                Console.WriteLine("Press Enter to continue");
                                                Console.ReadLine();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Failed to Delete the Account");

                                                Console.WriteLine();
                                                Console.WriteLine("Press Enter to continue");
                                                Console.ReadLine();
                                            }
                                        }
                                        else
                                        { 
                                            Console.WriteLine("Re-entered Number Does not Match!");
                                            Console.WriteLine();
                                            Console.WriteLine("Press Enter to continue");
                                            Console.ReadLine();
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to continue");
                                    Console.ReadLine();
                                    break;
                                }
                            }
                            break;
                        case 3:
                            {
                                Console.Clear();
                                Console.WriteLine("~~Update Account Information~~");
                                Console.WriteLine();
                                Console.WriteLine("Enter the Account Number: ");


                                bool checkAccountNo = int.TryParse(Console.ReadLine(), out int accountNo);

                                //checking if the re entered number is in correct datatype
                                if (!checkAccountNo)
                                {
                                    Console.WriteLine("Invalid Data Type");
                                    Console.WriteLine("failed to Update");
                                    Console.WriteLine();
                                    Console.WriteLine("Press enter to get to the menu");
                                    Console.ReadLine();
                                    break;
                                }

                                //checking validity of account number

                                //fetching the account holder name to check if the account exists or not
                                Admin_BLLClass bll = new Admin_BLLClass();

                                string name = bll.customerName(accountNo);
                                if (name == null)
                                {
                                    Console.WriteLine($"Account with Account Number: {accountNo} does not exist!");
                                }
                                else
                                {
                                    CustomerBO customer = new CustomerBO();
                                    customer = bll.customerInfo(accountNo);

                                    //showing previous information
                                    Console.Clear();
                                    Console.WriteLine($"Account Number: {customer.AccountNo}\n" +
                                        $"Holder's Name: {customer.Name}\n" +
                                        $"Account Type: {customer.Type}\n" +
                                        $"Current Balance: {customer.Balance}\n" +
                                        $"Account Status: {customer.Status}\n");
                                    Console.WriteLine();
                                    Console.WriteLine("Please enter in the fields you wish to update (leave blank otherwise):");
                                    Console.WriteLine();

                                    //making new empty variables
                                    string newLogin = string.Empty;
                                    //taking newcode as a string to check if changed or not
                                    string newCode = string.Empty;
                                    string newName = string.Empty;
                                    string newType = string.Empty;
                                    string newStatus = string.Empty;

                                    //receiving new information in new variables
                                    Console.WriteLine("Login: ");
                                    newLogin = Console.ReadLine();

                                    //if skipped old information is assigned
                                    if(newLogin == "")
                                    {
                                        newLogin=customer.Login;
                                    }
                                    else
                                    {
                                        customer.Login = newLogin;
                                    }
                                    
                                    Console.WriteLine("PinCode: ");
                                    newCode = Console.ReadLine();

                                    //if no new info is provided, then old info is kept
                                    if(newCode == "")
                                    {
                                        //converting the customer password to string to assign to string code
                                        newCode = (customer.Password).ToString();

                                        //converting the newCode to int
                                        Convert.ToInt32(newCode);
                                    }
                                    else
                                    {
                                        //checking the data type of newCode
                                        //it must be integer
                                        bool checkCode = int.TryParse(newCode, out int validCode);
                                        if(checkCode)
                                        {
                                            customer.Password = validCode;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Code Data type incorrect");
                                            Console.WriteLine();
                                            Console.WriteLine("Press enter to get back to menu");
                                            Console.ReadLine();
                                            break;
                                        }
                                    }

                                    Console.WriteLine("Holder's Name: ");
                                    newName = Console.ReadLine();
                                    if(newName=="")
                                    {
                                        newName = customer.Name;
                                    }
                                    else
                                    {
                                        customer.Name = newName;
                                    }

                                    Console.WriteLine("Type (Savings, Current): ");
                                    newType = Console.ReadLine();

                                    //checking the new entered type
                                    //also if new type is empty
                                    if (newType == "Savings" || newType == "Current" || newType == "savings" || newType == "current" || newType == "") 
                                    {

                                        //if new type is skipped
                                        //old information is assigned
                                        if (newType == "")
                                        {
                                            newType = customer.Type;
                                        }
                                        else
                                        {
                                            customer.Type = newType;
                                        }
                                        Console.WriteLine("Status: ");
                                        newStatus = Console.ReadLine();
                                        if (newStatus == "Active" || newStatus == "active" || newStatus == "Disabled" || newStatus == "disabled" || newStatus=="")
                                        {
                                            if (newStatus == "")
                                            {
                                                newStatus = customer.Status;
                                            }
                                            else
                                            {
                                                customer.Status = newStatus;
                                            }
                                            if (bll.updateCustomer(customer))
                                            {
                                                Console.WriteLine("Account Updated Successfully");
                                                Console.WriteLine();
                                                Console.WriteLine("Press Enter TO Continue");
                                                Console.ReadLine();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Failed to Update Account");
                                                Console.WriteLine();
                                                Console.WriteLine("Press enter to get to the menu");
                                                Console.ReadLine();
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please select either active or disabled\n" +
                                                "failed to update");
                                            Console.WriteLine();
                                            Console.WriteLine("Press enter to get to the menu");
                                            Console.ReadLine();
                                            break;
                                        }
                                            
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please choose either savings or current\n" +
                                            "Failed to update");
                                        Console.WriteLine();
                                        Console.WriteLine("Press enter to get to the menu");
                                        Console.ReadLine();
                                        break;
                                    }
                                    
                                   
                                }

                            }
                            break;
                        case 4:
                            {
                                Console.Clear();
                                Console.WriteLine("~~Search Account~~");
                                Console.WriteLine();
                                Console.WriteLine("Enter the search criteria (leave blank otherwise)");
                                //making new empty variables

                                string newName = string.Empty;
                                string newType = string.Empty;
                                string newStatus = string.Empty;

                                //taking balance as a string
                                string newBalance = string.Empty;

                                //taking accont number as a string
                                string newAccountNo = string.Empty;

                                //taking search criteria
                                Console.WriteLine("Account Number: ");
                                newAccountNo = Console.ReadLine();

                                Console.WriteLine("Holder's Name:");
                                newName = Console.ReadLine();

                                Console.WriteLine("Account Type: (Savings, Current)");
                                newType = Console.ReadLine();

                                Console.WriteLine("Account Status: (Active, Disabled)");
                                newStatus = Console.ReadLine();

                                Console.WriteLine("Balance: ");
                                newBalance = Console.ReadLine();

                                //making an array of objects to pass this info to bll
                                object[] objArr = { newAccountNo, newName, newType, newStatus, newBalance };

                                //making an int array to check criteria
                                //1st index represents acc#
                                //2nd index name
                                //3rd index Type
                                //4th index status
                                //5th index balance
                                int[] arr = { 0, 0, 0, 0, 0 };

                                //making an integer to check number of criteria
                                int count = 0;

                                //checks
                                //-1 means skipped
                                //0 means user typed something
                                //1 means the criteria is valid
                                if (newAccountNo == "")
                                {
                                    //not to include in search query
                                    arr[0] = -1;
                                }
                                if (newName == "")
                                {
                                    arr[1] = -1;
                                }
                                if (newType == "")
                                {
                                    arr[2] = -1;
                                }
                                if (newStatus == "")
                                {
                                    arr[3] = -1;
                                }
                                if (newBalance == "")
                                {
                                    arr[4] = -1;
                                }
                                
                                //now validating data type and format based on array indices

                                //checking the data type of account number
                                if (arr[0] == 0)
                                {
                                    bool checkacc = int.TryParse(newAccountNo, out int acc);
                                    if (checkacc)
                                    {
                                        //doing this to help make dynamic query in bll

                                        arr[0] = 1;
                                        objArr[0] = acc;
                                        count++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Account number must be integer");
                                        Console.WriteLine();
                                        Console.WriteLine("Press enter to get back to menu");
                                        Console.ReadLine();
                                        break;
                                    }
                                    
                                }

                                //checking the name
                                if (arr[1] == 0)
                                {
                                   
                                    arr[1] = 1;
                                    objArr[1] = newName;
                                    count++;
                                }
                              

                                //checking the type
                                if (arr[2] == 0)
                                {
                                    if (newType == "Savings" || newType == "Current" || newType == "savings" || newType == "current")
                                    {
                                        //doing this to help make dynamic query in bll

                                        arr[2] = 1;
                                        objArr[2] = newType;
                                        count++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Type Must be either savings or current");
                                        Console.WriteLine();
                                        Console.WriteLine("Press enter to get back to menu");
                                        Console.ReadLine();
                                        break;
                                    }
                                }
                              
                                //checking the status
                                if (arr[3] == 0)
                                {
                                    if (newStatus == "Active" || newStatus == "active" || newStatus == "Disabled" || newStatus == "disabled")
                                    {
                                        //doing this to help make dynamic query in bll

                                        arr[3] = 1;
                                        objArr[3] = newStatus;
                                        count++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Status Must be either active or disabled");
                                        Console.WriteLine();
                                        Console.WriteLine("Press enter to get back to menu");
                                        Console.ReadLine();
                                        break;
                                    }
                                }
                               
                                //checking the data type of balance
                                if (arr[4] == 0)
                                {
                                    bool checkbal = decimal.TryParse(newBalance, out decimal bal);
                                    if (checkbal)
                                    {
                                        //doing this to help make dynamic query in bll

                                        arr[4] = 1;
                                        objArr[4] = bal;
                                        count++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Balance must be decimal");
                                        Console.WriteLine();
                                        Console.WriteLine("Press enter to get back to menu");
                                        Console.ReadLine();
                                        break;
                                    }
                                }
                               
                                //now passing these arrays to bll to make query accordingly
                                Admin_BLLClass admin_BLL = new Admin_BLLClass();

                                //checking if no criteria is given
                                if(count==0)
                                {
                                    Console.WriteLine("No criteria is given!");
                                    Console.WriteLine();
                                    Console.WriteLine("Press enter to get back to menu");
                                    Console.ReadLine();
                                    break;
                                }
                                else
                                {
                                    //calling search function from bll
                                    List<CustomerBO> list = new List<CustomerBO>();
                                    list = admin_BLL.search(arr, objArr, count);

                                    //displaying results in a format
                                    Console.WriteLine("--------------------------------------------------------------------");
                                    Console.WriteLine("Acc#  |     Name      |      Type     |    Balance   |   Status    ");
                                    Console.WriteLine("--------------------------------------------------------------------");

                                    foreach (CustomerBO item in list)
                                    {
                                        Console.WriteLine(String.Format("{0,-5} | {1,-13} | {2,-13} | {3,-12} | {4,-15}  ", item.AccountNo, item.Name, item.Type, item.Balance, item.Status));

                                    }
                                    Console.WriteLine("--------------------------------------------------------------------");

                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to get back to menu");
                                    Console.ReadLine();

                                }
                            }
                            break;
                        case 5:
                            {
                                Console.Clear();
                                Console.WriteLine("~~View Reports~~");
                                Console.WriteLine();
                                Console.WriteLine("1. Search By Amount\n" +
                                    "2. Search By Date");
                                bool checkChoice = int.TryParse(Console.ReadLine(), out int Choice);
                                if(checkChoice)
                                {
                                    switch(Choice)
                                    {
                                        case 1:
                                            {
                                                Console.Clear();
                                                Console.WriteLine("~~Search By Amount~~");
                                                Console.WriteLine();
                                                Console.Write("Enter the minmum amount: ");
                                                bool checkMinimum = int.TryParse(Console.ReadLine(), out int Minimum);

                                                //checking the type
                                                if(checkMinimum)
                                                {
                                                    Console.Write("Enter the maximum Amount: ");
                                                    bool checkMaximum = int.TryParse(Console.ReadLine(), out int Maximum);
                                                    if(checkMaximum)
                                                    {

                                                        Admin_BLLClass bLLClass = new Admin_BLLClass();
                                                        //calling search by amount function from bll
                                                        List<CustomerBO> list = new List<CustomerBO>();
                                                        list = bLLClass.searchByAmount(Minimum, Maximum);

                                                        //displaying results in a format
                                                        Console.WriteLine("-----------------------------------------------------------------------");
                                                        Console.WriteLine("Acc#  |     Name      |      Type     |    Balance   |   Status    ");
                                                        Console.WriteLine("-----------------------------------------------------------------------");

                                                        foreach(CustomerBO item in list)
                                                        {
                                                            Console.WriteLine(String.Format("{0,-5} | {1,-13} | {2,-13} | {3,-12} | {4,-15}  ", item.AccountNo, item.Name, item.Type, item.Balance, item.Status));

                                                        }
                                                        Console.WriteLine("-----------------------------------------------------------------------");

                                                        Console.WriteLine();
                                                        Console.WriteLine("Press Enter to get back to menu");
                                                        Console.ReadLine();

                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid data Type");
                                                        Console.WriteLine();
                                                        Console.WriteLine("Press enter to continue");
                                                        Console.ReadLine();
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid data Type");
                                                    Console.WriteLine();
                                                    Console.WriteLine("Press enter to continue");
                                                    Console.ReadLine();
                                                    break;
                                                }
                                            }
                                            break;
                                        case 2:
                                            {
                                                Console.Clear();
                                                Console.WriteLine("~~Search By Date~~");
                                                Console.WriteLine();
                                                Console.WriteLine("Under Maintenance");
                                                Console.WriteLine("We are working very hard to provide you services, please bear with us\n");
                                                Console.WriteLine("press enter to get back to menu");
                                                Console.ReadLine();
                                                break;  
                                                Console.Write("Enter the starting Date in Format DD/MM/YYYY ");
                                                string strtdate = Console.ReadLine();
                                                DateTime date = DateTime.Today;
                                                strtdate = date.ToString();
                                                bool checkMinimum = DateTime.TryParse(Console.ReadLine(), out DateTime StartDate);
                                                Console.WriteLine($"Start date{StartDate}");
                                                //checking the type
                                                if (checkMinimum)
                                                {
                                                    Console.Write("Enter the Ending Date in Format DD/MM/YYYY ");
                                                    bool checkMaximum = DateTime.TryParse(Console.ReadLine(), out DateTime EndDate);
                                                    if (checkMaximum)
                                                    {

                                                        Admin_BLLClass bLLClass = new Admin_BLLClass();
                                                        //calling search by date function from bll
                                                        List<TransactionBO> list = new List<TransactionBO>();
                                                        list = bLLClass.searchByDate(StartDate, EndDate);

                                                        //displaying results in a format
                                                        Console.WriteLine("---------------------------------------------------------------------------------");
                                                        Console.WriteLine("Acc#  |     Name      |          Type          |    Amount   |        Date       ");
                                                        Console.WriteLine("---------------------------------------------------------------------------------");

                                                        foreach (TransactionBO item in list)
                                                        {
                                                            Console.WriteLine(String.Format("{0,-5} | {1,-13} | {2,-13} | {3,-12} | {4,-15}  ", item.AccountNo, item.Name, item.Type, item.Amount, item.Date));

                                                        }
                                                        Console.WriteLine("---------------------------------------------------------------------------------");

                                                        Console.WriteLine();
                                                        Console.WriteLine("Press Enter to get back to menu");
                                                        Console.ReadLine();

                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid data Type");
                                                        Console.WriteLine();
                                                        Console.WriteLine("Press enter to continue");
                                                        Console.ReadLine();
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid data Type");
                                                    Console.WriteLine();
                                                    Console.WriteLine("Press enter to continue");
                                                    Console.ReadLine();
                                                    break;
                                                }
                                            }
                                            break;
                                        default:
                                            {
                                                Console.WriteLine("Not a valid choice");
                                                Console.WriteLine();
                                                Console.WriteLine("Press enter to continue");
                                                Console.ReadLine();
                                            }
                                            break;
                                    }    
                                }
                                else
                                {
                                    Console.WriteLine("Invalid data Type");
                                    Console.WriteLine();
                                    Console.WriteLine("Press enter to continue");
                                    Console.ReadLine();
                                }
                            }
                            break;
                        case 6:
                            {
                                check = false;
                                generalMenu();
                            }
                            break;
                        default:
                            {
                                Console.WriteLine("Please Select a valid Choice!");
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }


        }
    }
    public class Customer_PLClass
    {
        public void customerLogin()
        {

            int check = 0;
            while (check < 3)
            {
                Console.WriteLine("Enter Login: ");
                string login = Console.ReadLine();


                Console.WriteLine("Enter PinCode: ");
                bool checkcode = int.TryParse(Console.ReadLine(), out int code);
                if (checkcode)
                {
                    Customer_BLLClass customer_BLL = new Customer_BLLClass();
                    var data = customer_BLL.customerLogin(login, code);

                    if (data.Item1)
                    {
                        //checking if the account is disabled
                        if (data.Item3 == "Disabled" || data.Item3 == "disabled")
                        {
                            Console.WriteLine("Your Account is currently disabled ");
                            Console.WriteLine("Please contact the administrator to Activiate");
                            Console.WriteLine();
                            Console.WriteLine("Press Enter to Exit");
                            Console.ReadLine();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Login Successfull");
                            //assigning the accountNumer of the user
                            int accountNo = data.Item2;
                            customerMenu(accountNo);

                        }

                    }
                    else
                    {
                        Console.WriteLine("Failed to login\n" +
                            "Please enter correct information");
                        //Console.WriteLine("If login fails 2 more times, your account will be disabled");
                        Console.WriteLine("Press Enter to Continue");
                        Console.ReadLine();
                        break;
                        //check++;
                        if(check==3)
                        {
                            //if user entered pin wrong three times

                        }
                    }

                }
                else
                { 
                    Console.WriteLine("Invalid Data Type");
                    Console.WriteLine("Press Enter to Continue");
                    Console.ReadLine();
                }
            }
        }
        //helper function for cash withdraw
        public void withdrawHelper(int amount, int accountNo)
        {
            //checking the amount should not be zero or negative
            if(amount <= 0)
            {
                Console.WriteLine("Amount typed is 0 or Negative");
                Console.WriteLine("Press enter to get back to menu");
                Console.ReadLine();
                customerMenu(accountNo);
            }
            Console.WriteLine($"Are you sure you want to withdraw Rs. {amount}   (Y/N) ?");

            bool checkChar = char.TryParse(Console.ReadLine(), out char confirm);
            if (checkChar)
            {
                if (confirm == 'Y' || confirm == 'y')
                {
                    //checking the balance of customer
                    Customer_BLLClass bll = new Customer_BLLClass();

                    //item will receive two values as follows
                    var data = bll.displayBalance(accountNo);

                    //checking if the amount is valid
                    if (data.Item2 < amount)
                    {
                        Console.WriteLine("Not Enough Balance for this Transaction");
                        Console.WriteLine();
                        Console.WriteLine("Press Enter to get back to menu");
                        Console.ReadLine();
                    }
                    else if (data.Item2 >= amount)
                    {
                        //now checking the 20k limit
                        Admin_BLLClass admin = new Admin_BLLClass();
                        List<TransactionBO> list = new List<TransactionBO>();

                        //getting a list of transaction the respective customer has made so far
                        list = admin.getTransactionInfo(accountNo);
                        int ammount = 0;
                        DateTime date = DateTime.Today;
                        
                        foreach (TransactionBO transaction in list)
                        {
                           
                            //if the date of withdrawal transaction is equal to today's date
                            //then add amount where transaction type is withdrawal
                            string dataDate = transaction.Date.ToString("dd/MM/yyyy");
                            string now = date.ToString("dd/MM/yyyy");
                            
                            
                            if (now == dataDate)
                            {
                               
                                if(transaction.Type=="Cash Withdraw")
                                {
                                    ammount+=transaction.Amount;
                                }
                            }
                        }
                        int total = amount + ammount;

                        //checking if the new amount is equal to or greater than 20k
                        if(amount > 20000)
                        {
                            Console.WriteLine($"You can only withdraw 20000 today");
                            Console.WriteLine("Press enter to get back to menu");
                            Console.ReadLine();
                            customerMenu(accountNo);
                        }
                        else if (ammount >= 20000)
                        {
                            Console.WriteLine("You have reached today's limit of 20k");
                            Console.WriteLine("Come back tommorow");
                            Console.WriteLine("Press enter to get back to menu");
                            Console.ReadLine();
                            customerMenu(accountNo);
                        }
                        else if(total > 20000)
                        {
                            Console.WriteLine($"You can only withdraw {20000-ammount} today");
                            Console.WriteLine("Press enter to get back to menu");
                            Console.ReadLine();
                            customerMenu(accountNo);

                        }
                        
                        if (bll.withdrawCash(accountNo, amount))
                        {
                            Console.WriteLine("Cash successfully withdrawn!");
                            Console.WriteLine();

                            //feeding this information in transaction table aswell
                            Admin_BLLClass admin_BLL = new Admin_BLLClass();
                            DateTime dateTime = DateTime.Now;

                            //getting customer name
                            CustomerBO customerBO = new CustomerBO();
                            customerBO = admin_BLL.customerInfo(accountNo);

                            //inserting this transaction in transaction table aswell
                            if(admin_BLL.insertTransactionInfo(accountNo,amount,dateTime,"Cash Withdraw",customerBO.Name))
                            {
                                //Console.WriteLine("information Fed Successfully");
                                //Console.ReadLine();
                            }
                            else
                            {

                                //Console.WriteLine("information Not Fed Successfully");
                                //Console.ReadLine();
                            }
                            Console.WriteLine("Do You Wish to print the Receipt? (Y/N)");

                            //checking if the input is a character
                            bool temp3 = char.TryParse(Console.ReadLine(), out char selection);

                            //if yes then
                            if (temp3)
                            {

                                //if user typed Y or y
                                if (selection == 'Y' || selection == 'y')
                                {
                                    Console.Clear();
                                    //printing the receipt
                                    //displaying updated balance
                                    //taking today's date from the system
                                    DateTime today = DateTime.Now;

                                    //item will receive two values as follows
                                    var data1 = bll.displayBalance(accountNo);
                                    Console.WriteLine($"AccountNo: {data1.Item1}\n" +
                                        $"Date: {today}\n" +
                                        $"Amount Withdrawn: {amount}\n" +
                                        $"Balance: {data1.Item2}\n");
                                    Console.WriteLine();
                                    Console.WriteLine("Thank You");
                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to get back to menu");
                                    Console.ReadLine();
                                }
                                else if (selection == 'N' || selection == 'n')
                                {
                                    Console.WriteLine("Thank You");
                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to get back to menu");
                                    Console.ReadLine();
                                }

                                //if user typed other than y or n
                                else
                                { 
                                    Console.WriteLine("Select a valid choice!");
                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to get back to menu");
                                    Console.ReadLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Select a valid choice!");
                                Console.WriteLine();
                                Console.WriteLine("Press Enter to get back to menu");
                                Console.ReadLine();
                            }
                        }
                    }
                }
                else if (confirm == 'N' || confirm == 'n')
                {
                    Console.WriteLine("Withdrawal Canceled \nThank You");
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to get back to menu");
                    Console.ReadLine();
                    
                }
                else
                {
                    Console.WriteLine("invalid choice");
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to get back to menu");
                    Console.ReadLine();
                }

            }
            else
            {
                Console.WriteLine("invalid datatype");
                Console.WriteLine();
                Console.WriteLine("Press Enter to get back to menu");
                Console.ReadLine();
            }
        }
        public void customerMenu(int accountNo)
        {

            bool check = true;
            while (check)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("~~Welcome~~");
                Console.WriteLine();
                Console.WriteLine("1. Withdraw Cash\n" +
                                  "2. Cash Transfer\n" +
                                  "3. Deposit Cash\n" +
                                  "4. Display Balance\n" +
                                  "5. Exit\n");

                bool tempCust = int.TryParse(Console.ReadLine(), out int choice);
                if (tempCust)
                {
                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Clear();
                                Console.WriteLine("~~Withdraw Cash~~");
                                Console.WriteLine();
                                Console.WriteLine("1. Fast Cash\n" +
                                    "2. Normal Cash");
                                bool checkchoose = int.TryParse(Console.ReadLine(), out int choose);
                                if (checkchoose)
                                {
                                    switch (choose)
                                    {
                                        case 1:
                                            {
                                                Console.Clear();
                                                Console.WriteLine("~~Fast Cash Withdrawal~~");
                                                Console.WriteLine();

                                                //preparing the menu
                                                Console.WriteLine("Please Select One of the given options: ");
                                                Console.WriteLine("1----500\n" +
                                                    "2----1000\n" +
                                                    "3----2000\n" +
                                                    "4----5000\n" +
                                                    "5----10000\n" +
                                                    "6----15000\n" +
                                                    "7----20000\n");
                                                bool checkChoice = int.TryParse(Console.ReadLine(), out int Choice);
                                                if (checkChoice)
                                                {
                                                    switch (Choice)
                                                    {
                                                        case 1:
                                                            {
                                                                withdrawHelper(500, accountNo);
                                                            }
                                                            break;
                                                        case 2:
                                                            {
                                                                withdrawHelper(1000, accountNo);
                                                            }
                                                            break;
                                                        case 3:
                                                            {
                                                                withdrawHelper(2000, accountNo);

                                                            }
                                                            break;
                                                        case 4:
                                                            {
                                                                withdrawHelper(5000, accountNo);

                                                            }
                                                            break;
                                                        case 5:
                                                            {
                                                                withdrawHelper(10000, accountNo);

                                                            }
                                                            break;
                                                        case 6:
                                                            {
                                                                withdrawHelper(15000, accountNo);

                                                            }
                                                            break;
                                                        case 7:
                                                            {
                                                                withdrawHelper(20000, accountNo);

                                                            }
                                                            break;
                                                        default:
                                                            {
                                                                Console.WriteLine("Invalid Choice");
                                                                Console.WriteLine();
                                                                Console.WriteLine("Press Enter to get back to menu");
                                                                Console.ReadLine();
                                                            }
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid Data Type");
                                                    Console.WriteLine();
                                                    Console.WriteLine("Press Enter to get back to menu");
                                                    Console.ReadLine();
                                                }
                                            }
                                            break;
                                        case 2:
                                            {
                                                Console.Clear();
                                                Console.WriteLine("~~Normal Cash Withdrawal~~");
                                                Console.WriteLine();
                                                Console.WriteLine("Enter the amount that you wish to withdraw: ");
                                                bool checkamount = int.TryParse(Console.ReadLine(), out int Amount);

                                                //checking if the amount is convertable and also amount is not zero and not negative
                                                if (checkamount)
                                                {
                                                    withdrawHelper(Amount, accountNo);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid Data Type");
                                                    Console.WriteLine();
                                                    Console.WriteLine("Press Enter to get back to menu");
                                                    Console.ReadLine();
                                                }
                                            }
                                            break;
                                        default:
                                            {
                                                Console.WriteLine("NOT A VALID CHOICE!");
                                                Console.WriteLine();
                                                Console.WriteLine("Press Enter to get back to menu");
                                                Console.ReadLine();
                                            }
                                            break;
                                            
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to get back to menu");
                                    Console.ReadLine();
                                }
                            }
                            break;
                        case 2:
                            {
                                Console.Clear();
                                Console.WriteLine("~~Cash Transfer~~");
                                Console.WriteLine();
                                //first check the balance of the holder and the amount to transfer
                                //if it is less than balance then move further
                                Console.WriteLine("Enter the amount in multiples of 500: ");

                                //using tryparse to evaluate datatype
                                bool checkamount = int.TryParse(Console.ReadLine(),out int amount);
                                //if incorrect data type
                                if(!checkamount)
                                {
                                    Console.WriteLine("Invalid data type");
                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to get back to menu");
                                    Console.ReadLine();
                                    break;
                                }

                                //checking if the amount is not zero or negative and also multiple of 500
                                if ( amount > 0 && amount % 500 == 0 )
                                {
                                    //checking the balance of sender
                                    Customer_BLLClass bll = new Customer_BLLClass();

                                    //accountNumber of the sender
                                    int senderAccountNo = accountNo;

                                    //item will receive two values as follows
                                    var senderItem = bll.displayBalance(senderAccountNo);

                                    //checking is the amount is valid
                                    if (senderItem.Item2 <= amount)
                                    {
                                        Console.WriteLine("Not Enough Balance for this Transaction");
                                        Console.WriteLine();
                                        Console.WriteLine("Press Enter to get back to menu");
                                        Console.ReadLine();
                                    }
                                    else if (senderItem.Item2 >= amount)
                                    {
                                        Console.WriteLine("Enter the Account Number to which you want to Transfer: ");

                                        //checking if the entered value is in correct data type
                                        bool temp = int.TryParse(Console.ReadLine(), out int receiverAccountNo);
                                        if (temp)
                                        {
                                            Admin_BLLClass bll_admin = new Admin_BLLClass();
                                            string name = bll_admin.customerName(receiverAccountNo);
                                            if (name == null)
                                            {
                                                Console.WriteLine($"Account with Account Number: {receiverAccountNo} does not exist!");
                                                Console.WriteLine();
                                                Console.WriteLine("Press Enter to get back to menu");
                                                Console.ReadLine();
                                            }
                                            else
                                            {
                                                Console.WriteLine($"You wish to deposit Rs. {amount} in account held by {name}\n" +
                                                    $"if this information is correct then please Re-Enter the Account Number: ");
                                                bool temp1 = int.TryParse(Console.ReadLine(), out int newAccountNo);
                                                if (temp1)
                                                {
                                                    if (receiverAccountNo != newAccountNo)
                                                    {
                                                        Console.WriteLine("Account Number does not Match with previous one!");
                                                        Console.WriteLine();
                                                        Console.WriteLine("Press Enter to get back to menu");
                                                        Console.ReadLine();
                                                    }
                                                    else
                                                    {
                                                        //storing the new amount of sender
                                                        decimal newSenderAmount = senderItem.Item2 - amount;

                                                        //fetching balance of receiver aswell
                                                        var itemReceiver = bll.displayBalance(receiverAccountNo);
                                                        decimal newReceiverAmount = itemReceiver.Item2 + amount;

                                                        //calling the function
                                                        if (bll.cashTransfer(senderAccountNo, receiverAccountNo, newSenderAmount, newReceiverAmount))
                                                        {
                                                            Console.WriteLine("Transaction Confirmed");
                                                            Console.WriteLine();

                                                            Admin_BLLClass admin_BLL = new Admin_BLLClass();
                                                            DateTime dateTime = DateTime.Now;
                                                            //getting customer name
                                                            CustomerBO customerBO = new CustomerBO();
                                                            customerBO = admin_BLL.customerInfo(senderAccountNo);

                                                            if (admin_BLL.insertTransactionInfo(accountNo, amount, dateTime, "Cash Transfer", customerBO.Name))
                                                            {
                                                                //Console.WriteLine("information Fed Successfully");
                                                                //Console.ReadLine();
                                                            }
                                                            else
                                                            {

                                                                //Console.WriteLine("information Not Fed Successfully");
                                                                //Console.ReadLine();
                                                            }
                                                            Console.WriteLine("Do You Wish to print the Receipt? (Y/N)");


                                                            //checking if the input is a character
                                                            bool temp3 = char.TryParse(Console.ReadLine(), out char selection);

                                                            //if yes then
                                                            if (temp3)
                                                            {

                                                                //if user typed Y or y
                                                                if (selection == 'Y' || selection == 'y')
                                                                {
                                                                    Console.Clear();
                                                                    //printing the receipt
                                                                    //displaying updated balance
                                                                    //taking today's date from the system
                                                                    DateTime today = DateTime.Now;

                                                                    //item will receive two values as follows
                                                                    var item = bll.displayBalance(senderAccountNo);
                                                                    Console.WriteLine($"AccountNo: {item.Item1}\n" +
                                                                        $"Date: {today}\n" +
                                                                        $"Amount Transferred: {amount}\n" +
                                                                        $"Balance: {item.Item2}\n");
                                                                    Console.WriteLine();
                                                                    Console.WriteLine("Thank You");
                                                                    Console.WriteLine();
                                                                    Console.WriteLine("Press Enter to get back to menu");
                                                                    Console.ReadLine();
                                                                }
                                                                else if (selection == 'N' || selection == 'n')
                                                                {
                                                                    Console.WriteLine("Thank You");
                                                                    Console.WriteLine();
                                                                    Console.WriteLine("Press Enter to get back to menu");
                                                                    Console.ReadLine();
                                                                }

                                                                //if user typed other than y or n
                                                                else
                                                                {
                                                                    Console.WriteLine("Select a valid choice!");
                                                                    Console.WriteLine();
                                                                    Console.WriteLine("Press Enter to get back to menu");
                                                                    Console.ReadLine();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Select a valid choice!");
                                                                Console.WriteLine();
                                                                Console.WriteLine("Press Enter to get back to menu");
                                                                Console.ReadLine();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Failed to Transfer Cash");
                                                            Console.WriteLine();
                                                            Console.WriteLine("Press Enter to get back to menu");
                                                            Console.ReadLine();
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid data type!");
                                                    Console.WriteLine();
                                                    Console.WriteLine("Press Enter to get back to menu");
                                                    Console.ReadLine();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Data type invalid");
                                            Console.WriteLine();
                                            Console.WriteLine("Press Enter to get back to menu");
                                            Console.ReadLine();
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Amount NOT multiple of 500");
                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to get back to menu");
                                    Console.ReadLine();
                                }
                            }
                            break;
                        case 3:
                            {
                                Console.Clear();
                                Console.WriteLine("~~Deposit Cash~~");
                                Console.WriteLine();
                                Console.WriteLine("Enter the Amount to deposit: ");

                                bool temp = decimal.TryParse(Console.ReadLine(), out decimal amount);

                                //checking that the amount should be positive and not zero and valid
                                if (temp && amount > 0)
                                {

                                    Customer_BLLClass bll = new Customer_BLLClass();
                                    if (bll.depositCash(accountNo, amount))
                                    {
                                        Console.WriteLine("Cash Deposited Successfully");
                                        Console.WriteLine();

                                        //displaying updated balance
                                        //taking today's date from the system
                                        DateTime today = DateTime.Today;

                                        //item will receive two values as follows
                                        var data = bll.displayBalance(accountNo);
                                        Console.WriteLine($"AccountNo: {data.Item1}\n" +
                                            $"Date: {today}\n" +
                                            $"Amount Deposited: {amount}\n" +
                                            $"Balance: {data.Item2}\n");
                                        Console.WriteLine("Thank You");
                                        Console.WriteLine();
                                        Console.WriteLine("Press Enter to get back to menu");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to Deposit Cash!");
                                        Console.WriteLine();
                                        Console.WriteLine("Press Enter to get back to menu");
                                        Console.ReadLine();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Enter a valid number! or amount is 0");
                                    Console.WriteLine();
                                    Console.WriteLine("Press Enter to get back to menu");
                                    Console.ReadLine();
                                }
                            }
                            break;
                        case 4:
                            {
                                Console.Clear();
                                Console.WriteLine("~~Display Balance~~");
                                Console.WriteLine();

                               

                                Customer_BLLClass bll = new Customer_BLLClass();

                                //taking today's date from the system
                                DateTime today = DateTime.Now;

                                //item will receive two values as follows
                                var data = bll.displayBalance(accountNo);
                                Console.WriteLine($"AccountNo: {data.Item1}\n" +
                                    $"Date: {today}\n" +
                                    $"Balance: {data.Item2}\n");
                                Console.WriteLine("Thank You");
                                Console.WriteLine();
                                Console.WriteLine("Press Enter to get back to menu");
                                Console.ReadLine();

                            }
                            break;
                        case 5:
                            {
                                check = false;
                                Admin_PLClass admin_PL = new Admin_PLClass();
                                admin_PL.generalMenu();
                            }
                            break;
                        default:
                            {
                                Console.WriteLine("Please Select a valid Choice!");
                                Console.WriteLine();
                                Console.WriteLine("Press Enter to get back to menu");
                                Console.ReadLine();
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invaid Input");
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to get back to menu");
                    Console.ReadLine();
                }
            }
        }
    }
}
