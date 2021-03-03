using System;
using System.Data.SqlClient;
using System.Dynamic;

namespace simple_bank
{
    class bank_queries
    {
       private string connStr = @"Data Source=DESKTOP-QNORU54\BWDATOOLSET;Initial Catalog=simplebank;Integrated Security=True";

       public void insert_owner(owner owner1)
        {
              //string connectString = "Data Source=DESKTOP-OMVH3NE;Initial Catalog=dbo;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            // Create query
            string cmdStr = "INSERT INTO ACC_Owner (owner_name, social, username, passwowrd) VALUES (@name, @social, @user, @password)";
            SqlCommand comd = new SqlCommand(cmdStr, conn);
    
            // Add parameters to command
            comd.Parameters.AddWithValue("@name", owner1.owner_name);
            comd.Parameters.AddWithValue("@social", owner1.social);
            comd.Parameters.AddWithValue("@user", owner1.username);
            comd.Parameters.AddWithValue("@password", owner1.password);
            comd.ExecuteNonQuery();
            conn.Close();
        }


        // Verify bank exists
        public bool check_owner_ID(owner owner1)
        {
            int count;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            // Create query
            // string cmdStr = "SELECT COUNT(*) FROM ACC_Owner where username = '@username' AND passwowrd = '@password'";
            string cmdStr = "SELECT COUNT(*) FROM ACC_Owner where username = '" + owner1.username + "' AND passwowrd = '" + owner1.password+ "'";
            SqlCommand comd = new SqlCommand(cmdStr, conn);
           // comd.Parameters.AddWithValue("@username", owner1.username);
           // comd.Parameters.AddWithValue("@password", owner1.password);
            count = (int)comd.ExecuteScalar();
            conn.Close();

            if (count > 0)
                return true;
            else
                return false;
        }


        // returns id of owner
        public int get_owner_id(owner owner1)
        {
            // Create query
            string cmdStr = "SELECT ACC_Owner_ID FROM ACC_Owner where username = '" + owner1.username + "' AND passwowrd = '" + owner1.password + "'";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand comd = new SqlCommand(cmdStr, conn);
            int id = -1;
            SqlDataReader reader = comd.ExecuteReader();
            while (reader.Read())
            {
                id = (int) reader[0];
            }

            reader.Close();
            conn.Close();

            return id;
        }

        public double get_account_balance(account acc)
        {
            // Create query
            string cmdStr = "SELECT balance FROM ACC where owner_ID = @owner_id AND bank_ID = @bank_ID";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand comd = new SqlCommand(cmdStr, conn);
            double balance = -1.0;
            comd.Parameters.AddWithValue("@owner_id", acc.owner_ID);
            comd.Parameters.AddWithValue("@bank_ID", acc.bank_ID);

            SqlDataReader reader = comd.ExecuteReader();
            while (reader.Read())
            {
                balance = Convert.ToDouble(reader[0]);
            }

            reader.Close();
            conn.Close();

            return balance;
        }

        public void update_acc_balance(int acc_id, double balance)
        {
            // Create query
            string cmdStr = "UPDATE ACC SET balance = @balance where ACC_ID = @ACC_ID ";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand comd = new SqlCommand(cmdStr, conn);
            comd.Parameters.AddWithValue("@balance", balance);
            comd.Parameters.AddWithValue("@ACC_ID", acc_id);
            comd.ExecuteNonQuery();
            conn.Close();
        }

        // Verify an account exists
        public bool check_acc_ID(int id)
        {
            int count;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            // Create query
            string cmdStr = "SELECT COUNT(*) FROM ACC where ACC_ID= @id";
            SqlCommand comd = new SqlCommand(cmdStr, conn);
            comd.Parameters.AddWithValue("@id", id);
            count = (int)comd.ExecuteScalar();
            conn.Close();

            if (count > 0)
                return true;
            else
                return false;
        }

        public account get_account_info(account acc)
        {
            // Create query
            string cmdStr = "SELECT ACC_ID, bank_ID, ACC_Type, balance FROM ACC where owner_ID = @owner_id";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand comd = new SqlCommand(cmdStr, conn);
            comd.Parameters.AddWithValue("@owner_id", acc.owner_ID);


            SqlDataReader reader = comd.ExecuteReader();
            while (reader.Read())
            {
                acc.acc_id = (int)reader[0];
                acc.bank_ID = (int)reader[1];
                acc.ACC_Type = (int)reader[2];
                acc.balance = Convert.ToDouble(reader[3]);
            }

            reader.Close();
            conn.Close();

            return acc;
        }

        public account get_account_info_with_id(account acc)
        {
            // Create query
            string cmdStr = "SELECT owner_ID, bank_ID, ACC_Type, balance FROM ACC where ACC_ID = @ACC_ID";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand comd = new SqlCommand(cmdStr, conn);
            comd.Parameters.AddWithValue("@ACC_ID", acc.acc_id);


            SqlDataReader reader = comd.ExecuteReader();
            while (reader.Read())
            {
                acc.owner_ID = (int)reader[0];
                acc.bank_ID = (int)reader[1];
                acc.ACC_Type = (int)reader[2];
                acc.balance = Convert.ToDouble(reader[3]);
            }

            reader.Close();
            conn.Close();

            return acc;
        }

        // List all existing banks
        public void listBanks()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            // Create query
            string cmdStr = "SELECT * FROM bank";
            SqlCommand comd = new SqlCommand(cmdStr, conn);

     
            SqlDataReader reader = comd.ExecuteReader();
            while (reader.Read())
            {
                Console.Write("\t" + reader[1] + " : ");
                Console.WriteLine(reader[0]);
            }
            reader.Close();
            conn.Close();
        }

        // Verify bank exists
        public bool check_bank_ID(int id)
        {
            int count;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            // Create query
            string cmdStr = "SELECT COUNT(*) FROM bank where bank_id= @id ";
            SqlCommand comd = new SqlCommand(cmdStr, conn);
            comd.Parameters.AddWithValue("@id", id);
            count = (int) comd.ExecuteScalar();
            conn.Close();

            if (count > 0)
                return true;
            else
                return false;
        }

        public void insert_acc(account acc)
        {
            //string connectString = "Data Source=DESKTOP-OMVH3NE;Initial Catalog=dbo;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            // Create query
            string cmdStr = "INSERT INTO ACC (bank_ID, owner_ID, ACC_Type, balance) VALUES (@bank, @owner, @type, @balance)";
            SqlCommand comd = new SqlCommand(cmdStr, conn);
            // Add parameters to command
            comd.Parameters.AddWithValue("@bank", acc.bank_ID);
            comd.Parameters.AddWithValue("@owner", acc.owner_ID);
            comd.Parameters.AddWithValue("@type", acc.ACC_Type);
            comd.Parameters.AddWithValue("@balance", acc.balance);
            comd.ExecuteNonQuery();
            conn.Close();
        }
        void test(string[] args)
        {
                  string connectString = "Data Source=localhost;Initial Catalog=simplebank;Integrated Security=True";

        //string connectString = "Data Source=DESKTOP-OMVH3NE;Initial Catalog=dbo;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectString);
            conn.Open();

            // Create query
            string getScalers = "SELECT * FROM bank";
            SqlCommand comd = new SqlCommand(getScalers, conn);

            // Execute command and read results
            SqlDataReader dr = comd.ExecuteReader();
            Console.Write(dr.RecordsAffected + "\n");
            conn.Close();
        }
    }
}
