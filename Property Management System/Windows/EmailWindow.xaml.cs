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
using System.Windows.Shapes;

namespace Property_Management_System
{
    /// <summary>
    /// Interaction logic for EmailWindow.xaml
    /// </summary>
    public partial class EmailWindow : Window
    {
        //An instance of this window that allows other controls to access public methods. 
        public static EmailWindow Window;
        public EmailWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[EmailWindow] Loaded.");
            }
            //Define the instance of the EmailWindow "Window" as this window.
            Window = this;
        }

        /// <summary>
        /// Load data into the window such as the recipient and the first and lastnames into the body so the user knows what their name is.
        /// </summary>
        /// <param name="Recipient">The receiver of the email. The customer that was clicked on will have their email loaded in.</param>
        /// <param name="Firstname">The first name of the selected customer.</param>
        /// <param name="Lastname">The last name of the selected customer.</param>
        public static void SetContent(string Recipient, string Firstname, string Lastname)
        {
            Window.RecipientField.Text = Recipient;
            Window.BodyField.Text = "Dear "+ Firstname +" "+ Lastname +",";
            //Get the sender from the settings as it requires a password to send and we don't want to let them send from any email.
            Window.SenderField.Text = Properties.Settings.Default.Email_Sender;
        }

        /// <summary>
        /// Once the user clicks the Send button, this function calls SendMail in the Email class which actually sends the email.
        /// </summary>
        /// <param name="sender">Reference to the control that sent the event.</param>
        /// <param name="e">The data that is sent.</param>
        private void Send_Click(object sender, RoutedEventArgs e) => Email.SendMail(RecipientField.Text, SubjectField.Text, BodyField.Text);
    }
}
