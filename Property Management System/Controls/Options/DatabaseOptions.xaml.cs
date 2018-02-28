using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Property_Management_System
{
    /// <summary>
    /// Interaction logic for DatabaseOptions.xaml
    /// </summary>
    public partial class DatabaseOptions : UserControl
    {
        public DatabaseOptions()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[DatabaseOptions] Loaded");
            }
        }

        #region XAML
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HostField.Text) ||
                string.IsNullOrWhiteSpace(DatabaseField.Text) ||
                string.IsNullOrWhiteSpace(PasswordField.Password) ||
                string.IsNullOrWhiteSpace(UsernameField.Text))
            {
                MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Properties.Settings.Default.Database_ConnectionString = "DATA SOURCE=" + HostField.Text + ";PORT=3306;DATABASE=" + DatabaseField.Text + ";UID=" + UsernameField.Text + ";PASSWORD=" + PasswordField.Password + ";";
                Properties.Settings.Default.Save();
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[DatabaseOptions] Connection string saved: "+ Properties.Settings.Default.Database_ConnectionString);
                }
                Database.Refresh();
                if (MessageBox.Show("Database connection information saved!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    var ThisWindow = Window.GetWindow(this);
                    ThisWindow.Close();
                }
            }
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HostField.Text) ||
                string.IsNullOrWhiteSpace(DatabaseField.Text) ||
                string.IsNullOrWhiteSpace(PasswordField.Password) ||
                string.IsNullOrWhiteSpace(UsernameField.Text))
            {
                MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Properties.Settings.Default.Database_ConnectionString = "DATA SOURCE=" + HostField.Text + ";PORT=3306;DATABASE=" + DatabaseField.Text + ";UID=" + UsernameField.Text + ";PASSWORD=" + PasswordField.Password + ";";
                Properties.Settings.Default.Save();
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[DatabaseOptions] Connection string saved: " + Properties.Settings.Default.Database_ConnectionString);
                }
                Database.Refresh();
            }
        }
        #endregion
    }
}
