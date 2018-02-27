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
    /// Interaction logic for BookingsSingle.xaml
    /// </summary>
    public partial class BookingsSingle : UserControl
    {
        public BookingsSingle()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[BookignsSingle] Loaded");
            }
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            string Content = "ViewCustomers";
            MainWindow.SetContent(Content);
            string Query = "SELECT * FROM CUSTOMERS WHERE Email = '"+ Email.Text +"'";
            ViewCustomers.Control.InitializeControl(Query);
        }
    }
}
