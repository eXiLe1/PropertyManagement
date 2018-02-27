using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MySql.Data.MySqlClient;

namespace Property_Management_System
{
    class Database
    {
        // Private MySQL Connection, can be opened, closed or created within this class using a connection string.
        private static MySqlConnection _connection;

        // The current MySQL Connection, other methods access this to close the database connection.
        public static MySqlConnection Connection { get => _connection; private set => _connection = value; }
        
        // The connection string, loaded from the settings where it is stored.
        private static string ConnectionString = Properties.Settings.Default.Database_ConnectionString;
        // Boolean that is set to true if a connection is available, reduces loading times if a connection doesn't have to be attempted each time if we know it is closed.
        private static bool _canConnect;

        /// <summary>
        /// Creates the first connection on program launch, it checks for an existing connection string and prompts the user if one is not found.
        /// </summary>
        public static void InitialOpen()
        {
            if(ConnectionString == "")
            {
                _canConnect = false;
                Log.Commit("[Database:InitialOpen] No saved connection.");
                //Prompt the user to provide connection data.
                if (MessageBox.Show("No saved database connection, would you like to create one?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //Open a new options window so the user can enter the database details.
                    var options = new Options();
                    options.Show();
                    options.OptionsContent.Content = new DatabaseOptions(); 
                }
            }
            else
            {
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Database:InitialOpen] Saved connection found: " + ConnectionString);
                }
                try
                {
                    //Create a new MySql Connection and open then close it to check that the connection is valid. 
                    _connection = new MySqlConnection(ConnectionString);
                    _connection.Open();
                    _connection.Close();
                    Log.Commit("[Database:InitialOpen] Connection created.");
                    MainWindow.SetDatabaseStatus(true);
                    _canConnect = true;
                }
                catch (Exception e)
                {
                    //Log the error.
                    if (Properties.Settings.Default.User_AdvancedLogging)
                    {
                        Log.Commit("[Database:InitialOpen] Initial connection failed: " + e.Message);
                        Log.Commit("[Database:InitialOpen] Using connection string: " + ConnectionString);
                    }
                    //Update the label to indicate there is no connection.
                    MainWindow.SetDatabaseStatus(false);
                    _canConnect = false;
                }
            }
        }

        /// <summary>
        /// Re-checks if the connection is valid and sets CanConnect to true to let other methods know they can connect.
        /// </summary>
        public static void Refresh()
        {
            try
            {
                //Open and close to verify the connection can be made.
                _connection.Open();
                _connection.Close();
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Database:Refresh] Connection refreshed.");
                }
                //Update the label.
                MainWindow.SetDatabaseStatus(true);
                //We can connect so update the boolean to let other methods know we can connect.
                _canConnect = true;
            }
            catch (Exception e)
            {
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Database:Refresh:E01] Connection failed: " + e.Message);
                }   
                //Tell the user the connection failed.
                MessageBox.Show("No connection! Please check the database connection and try again.", "Database Connection", MessageBoxButton.OK, MessageBoxImage.Error);
                _canConnect = false;
            }
        }

        /// <summary>
        /// Executes a simple SQL Query such as insert or delete, a query that does not return any data.
        /// </summary>
        /// <param name="query">The SQL Query defined by sending method that is executed.</param>
        public static void ExQuery(string query)
        {
            //Check the connection can be made.
            Refresh();
            if (_canConnect)
            {
                //Open connection and execute the command.
                _connection.Open();
                var cmd = _connection.CreateCommand();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Database:ExQuery] Query: " + query + " executed.");
                }
                //Close the connection.
                _connection.Close();
            }
        }

        /// <summary>
        /// Executes an SQL Query that returns data.
        /// </summary>
        /// <param name="query">The SQL Query defined by sending method that is executed.</param>
        /// <returns>The data from the SQL Query is returned to sending method, however if the connection couldn't be established it returns null to show no data can be loaded.</returns>
        public static MySqlDataReader DataReader(string query)
        {
            //Check the connection can be made.
            Refresh();
            if (_canConnect)
            {
                //Open connection and fetch the data.
                _connection.Open();
                var cmd = _connection.CreateCommand();
                cmd.CommandText = query;
                var dr = cmd.ExecuteReader();
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Database:DataReader] DataReader '" + query + "' executed.");
                }
                return dr;
            }
            return null;
        }
    }
}
