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
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        /// <summary>
        /// Constructor for the class. Called when the window is created.
        /// </summary>
        public Options()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[Options] Loaded");
            }
        }

        /// <summary>
        /// The user has clicked on the General option and the content of the options window is being set to the GeneralOptions Control.
        /// </summary>
        /// <param name="sender">The object sending the data.</param>
        /// <param name="e">The data being sent.</param>
        private void General_Click(object sender, MouseButtonEventArgs e)
        {
            OptionsContent.Content = new GeneralOptions();
        }

        /// <summary>
        /// The user has clicked on the Database option and the content of the options window is being set to the DatabaseOptions Control.
        /// </summary>
        /// <param name="sender">The object sending the data.</param>
        /// <param name="e">The data being sent.</param>
        private void Database_Click(object sender, MouseButtonEventArgs e)
        {
            OptionsContent.Content = new DatabaseOptions();
        }

        /// <summary>
        /// The user has clicked on the Email option and the content of the options window is being set to the EmailOptions Control.
        /// </summary>
        /// <param name="sender">The object sending the data.</param>
        /// <param name="e">The data being sent.</param>
        private void Email_Click(object sender, MouseButtonEventArgs e)
        {
            OptionsContent.Content = new EmailOptions();
        }

        private void Cloud_Click(object sender, MouseButtonEventArgs e)
        {
            OptionsContent.Content = new CloudOptions();
        }
    }
}
