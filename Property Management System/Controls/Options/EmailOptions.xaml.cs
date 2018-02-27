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
    public partial class EmailOptions : UserControl
    {
        public EmailOptions()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[EmailOptions] Loaded");
            }
        }

        #region XAML
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HostField.Text) ||
                string.IsNullOrWhiteSpace(SenderField.Text) ||
                string.IsNullOrWhiteSpace(PasswordField.Password) ||
                string.IsNullOrWhiteSpace(SMTPPortField.Text))
            {
                MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Properties.Settings.Default.Email_Host = HostField.Text;
                Properties.Settings.Default.Email_Sender = SenderField.Text;
                Properties.Settings.Default.Email_Password = PasswordField.Password;
                Properties.Settings.Default.Email_Port = SMTPPortField.Text;
                Properties.Settings.Default.Save();
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[EmailOptions] Email Details Saved: " + Properties.Settings.Default.Email_Host);
                }
            }
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HostField.Text) ||
                string.IsNullOrWhiteSpace(SenderField.Text) ||
                string.IsNullOrWhiteSpace(PasswordField.Password) ||
                string.IsNullOrWhiteSpace(SMTPPortField.Text) ||
                string.IsNullOrEmpty(TestRecipientField.Text))
            {
                MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Properties.Settings.Default.Email_Host = HostField.Text;
                Properties.Settings.Default.Email_Sender = SenderField.Text;
                Properties.Settings.Default.Email_Password = PasswordField.Password;
                Properties.Settings.Default.Email_Port = SMTPPortField.Text;
                Properties.Settings.Default.Save();
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[EmailOptions] Email Details Saved: " + Properties.Settings.Default.Email_Host);
                }
                string Subject = "Test Email";
                string Body = "This is a test email from the Property Management System Program";
                Email.SendMail(TestRecipientField.Text, Subject, Body);
            }
        }
        #endregion
    }
}
