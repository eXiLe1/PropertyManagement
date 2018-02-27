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
    /// Interaction logic for CustomersToolbar.xaml
    /// </summary>
    public partial class CustomersToolbar : UserControl
    {
        public CustomersToolbar()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[CustomersToolbar] Loaded.");
            }
        }

        /// <summary>
        /// Sets the content of the host control to the user control associated with this button
        /// </summary>
        /// <param name="sender">The button sending the data.</param>
        /// <param name="e">The data being sent, a click</param>
        private void ViewCustomersButton_Click(object sender, RoutedEventArgs e)
        {
            string Content = "ViewCustomers";
            CustomersHost.SetContent(Content);
        }
    }
}
