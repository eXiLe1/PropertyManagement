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
using System.IO;

namespace Property_Management_System
{
    /// <summary>
    /// Interaction logic for DatabaseOptions.xaml
    /// </summary>
    public partial class CloudOptions : UserControl
    {
        public CloudOptions()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[CloudOptions] Loaded");
            }
        }

        #region XAML
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddressField.Text) ||
                string.IsNullOrWhiteSpace(UserField.Text) ||
                string.IsNullOrWhiteSpace(PasswordField.Password))
            {
                MessageBox.Show("Please enter required information.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Properties.Settings.Default.FTP_Address = AddressField.Text;
                Properties.Settings.Default.FTP_User = UserField.Text;
                Properties.Settings.Default.FTP_Password = PasswordField.Password;
                Properties.Settings.Default.Save();
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[CloudOptions] Cloud Details Saved: " + Properties.Settings.Default.Email_Host);
                }
                if (MessageBox.Show("Cloud connection information saved!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    var ThisWindow = Window.GetWindow(this);
                    ThisWindow.Close();
                }
            }
        }
        #endregion

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            Folder
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string name = dlg.
            }
        }
    }
}
