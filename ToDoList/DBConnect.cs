using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "test";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);


            //Da commentare successivamente
            try
            {
                string query = "CREATE TABLE IF NOT EXISTS `tasks` ( `id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,`task` varchar(255) NOT NULL, `done` tinyint(1) DEFAULT 0) ENGINE = InnoDB DEFAULT CHARSET = latin1";
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                
                MessageBox.Show("Errore! Prova ad abilitare il server localhost", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);

            }
            //.
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Cannot " + ex);

                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public List<Todo> Select()
        {
            string query = "SELECT * FROM tasks";

            List<Todo> list = new List<Todo>();

            //Open connection
            if (this.OpenConnection() == true)
            {
               
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    list.Add(new Todo((bool)dataReader["done"], (string)dataReader["task"]));

                }

                dataReader.Close();
                this.CloseConnection();

                return list;
            }
            else
            {
                return list;
            }
        }

        public void Insert(string task)
        {
            string query = "INSERT INTO tasks(task) VALUES ('" + task + "')";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Update(string oldtask, string newtask)
        {

            string query = "UPDATE tasks SET task='" + newtask + "' WHERE task='" + oldtask + "';";

            //Open connection
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                MessageBox.Show("Elemento modificato correttamente. Premi Ok per aggiornare");
            }
        }
        public void Done(string task, int flag)
        {
            string query = "UPDATE tasks SET done='" + flag + "' WHERE task='" + task + "';";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Delete(string task)
        {
            string query = "DELETE FROM tasks WHERE task='" + task + "';";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                MessageBox.Show("Elemento eliminato correttamente. Premi Ok per aggiornare");
            }

        }
    }
}