using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net.NetworkInformation;
using System.Text;

namespace simple_bank
{
    class owner
    {
        public int owner_id;
        public string owner_name;
        public string social;
        public string username;
        public string password; // If extra time, hash this.
    }

    class account
    {

        public int acc_id;
        public int bank_ID;
        public int owner_ID;
        public int ACC_Type;
        public double balance;
    }

    class bank_operations
    {
        public account bank_acc = new account();
        public owner owner1 = new owner();
        bank_queries query = new bank_queries();
        public void add_owner()
        {
            // Gather owner information from the user
            Console.WriteLine("Enter your name");
            owner1.owner_name  = Console.ReadLine();
            Console.WriteLine("Enter your social");
            owner1.social = Console.ReadLine();
            Console.WriteLine("Enter your username");
            owner1.username = Console.ReadLine();
            Console.WriteLine("Enter your password");
            owner1.password = Console.ReadLine();

            // insert user into databse
            query.insert_owner(owner1);
            Console.WriteLine(query.check_owner_ID(owner1));
            owner1.owner_id = query.get_owner_id(owner1);
            add_acc(owner1);
        }

        public void deposit(double amount, account accID)
        {
            // update balance
            account temp = new account();
            accID.balance = accID.balance += amount;

            query.update_acc_balance(accID.acc_id, accID.balance);

            // get updated balance
            double balance = -1.0;
            balance = query.get_account_balance(accID);
            if (balance != -1.0)
                Console.Write("Your balance is now : " + balance + "\n");
            else
                Console.Write("Error retrieving balance \n");
        }

        public bool withdrawl(double amount, account accID)
        {
            if (amount > 1000 && accID.ACC_Type == 3)
            {
                Console.WriteLine("Cannot withdrawl more than $1000 with an individual investment account. Returning to menu\n");
                return false;
            }

            // update balance
            account temp = new account();

            double newbalance = accID.balance - amount;

            if (newbalance < 0)
            {
                Console.WriteLine("Cannot complete transaction. Overdraft.\n");
                return false;
            }
            else
                accID.balance = newbalance;

            query.update_acc_balance(accID.acc_id, accID.balance);

            // get updated balance
            double balance = -1.0;
            balance = query.get_account_balance(accID);
            if (balance != -1.0)
                Console.Write("Your balance is now : " + balance + "\n");
            else
                Console.Write("Error retrieving balance \n");
            return true;

        }

        public void transfer(double amount, int acc2)
        {
            account transferAcc = new account();
            transferAcc.acc_id = acc2;
            transferAcc = query.get_account_info_with_id(transferAcc);

            if (query.check_acc_ID(acc2))
            {
                bool flag = withdrawl(amount, bank_acc);
                if (!flag)
                    return;
                // If have time, change the accounts to be in an array -- many accounts, 1 owner so that can do this the proper way.
                
                deposit(amount, transferAcc);

                //  if more time, insert fail safe. If withdrawl goes through, but deposit fails, deposit money back into the original account.
            }
            else
            {
                Console.WriteLine("Account entered does not exist.\n");
            }
        }

        public bool login()
        {
            bool flag = false;
            Console.WriteLine("Username:");
            owner1.username = Console.ReadLine();
            Console.WriteLine("Password");
            owner1.password = Console.ReadLine();
            try
            {
                owner1.owner_id = query.get_owner_id(owner1);
            }
            catch
            {
                Console.Write("Login Failed\n");
                flag = true;
            }
            account temp = new account();
            bank_acc.owner_ID = owner1.owner_id;
            temp = query.get_account_info(bank_acc);
            bank_acc.acc_id = temp.acc_id;
            bank_acc.bank_ID = temp.bank_ID;
            bank_acc.balance = temp.balance;
            return flag;
        }

        public void add_acc(owner owner1)
        {
            
            int exitFlag = 0;
            Console.WriteLine("What bank would you like to bank at?");

            // Get  bank
            while (exitFlag == 0)
            {
                query.listBanks();
                bank_acc.bank_ID = Int32.Parse(Console.ReadLine());

                if (query.check_bank_ID(bank_acc.bank_ID))
                {
                    exitFlag = 1;
                }
                else
                    Console.WriteLine("Bank not recognized, please enter the numeric option for the bank you want an account at.\n");
            }

            exitFlag = 0;
            while (exitFlag == 0)
            {
                Console.WriteLine("Enter the desired account type: \n");
                Console.WriteLine(" 1 : Checking \n 2 : Corporate Investment \n 3 : Individual Investment "); // Create account type table to define these if time.
                bank_acc.ACC_Type = Int32.Parse(Console.ReadLine());

                if (bank_acc.ACC_Type <= 3)
                {
                    exitFlag = 1;
                }

                else
                    Console.WriteLine("Bank not recognized, please enter the numeric option for the bank you want an account at.\n");
            }

            bank_acc.owner_ID = owner1.owner_id;
            bank_acc.balance = 0.0; // initialize balance to 0
            query.insert_acc(bank_acc);
        }
    }
}
