using System;
using System.Data.SqlClient;
using System.IO;

namespace simple_bank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            bank_operations bankOps = new bank_operations();
            int exit_flag = 0;
            while (exit_flag == 0)
            {
                Console.WriteLine("1. Create new user \n 2. Login \n 3. Exit ");
                string user_choice = Console.ReadLine();
                if (user_choice == "1")
                    bankOps.add_owner();
                if (user_choice == "2")
                {
                   bankOps.login();
                    int loggedin = 1;
                    string ans;
                    while (loggedin == 1)
                    {
                        Console.WriteLine("1. Deposit\n 2. Withdrawl\n 3. Balance Transfer\n 4. 'Logout");
                        ans = Console.ReadLine();
                        if (ans == "1")
                        {
                            Console.WriteLine("Enter the amount of money to deposit");
                            double amount = double.Parse(Console.ReadLine());
                            bankOps.deposit(amount, bankOps.bank_acc);
                        }
                        if (ans == "2")
                        {
                            Console.WriteLine("Enter the amount of money to withdrawl");
                            double amount = double.Parse(Console.ReadLine());
                            bankOps.withdrawl(amount, bankOps.bank_acc);
                        }
                        if (ans == "3")
                        {
                            Console.WriteLine("How much do you want to transfer?");
                            double amount = double.Parse(Console.ReadLine());
                            Console.WriteLine("What account do you want to transfer to?");
                            int acc2 = int.Parse(Console.ReadLine()); 
                            bankOps.transfer(amount, acc2);
                        }
                        if (ans == "4")
                        {
                            loggedin = 0;
                        }
                    }
                }
               // else if (user_choice == "3")
             //       bankOps.add_acc();
                else if (user_choice == "3")
                    exit_flag = 1;
                else
                    Console.WriteLine("Invalid Input. Please enter a number 1-5.");
            }
        }
    }
}
