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
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class WelcomeHost : UserControl
    {
        public WelcomeHost()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[WelcomeHost] Loaded.");
            }
        }

        #region XAML
        private void StayDuration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Prices_Click(object sender, RoutedEventArgs e)
        {
            string Content = "Prices";
            MainWindow.SetContent(Content);
        }

        private void PriceAnalysis_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Bookings_Click(object sender, RoutedEventArgs e)
        {
            string Content = "Bookings";
            MainWindow.SetContent(Content);
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            string Content = "ViewCustomers";
            MainWindow.SetContent(Content);
        }

        private void Tasks_Click(object sender, RoutedEventArgs e)
        {
            string Content = "Tasks";
            MainWindow.SetContent(Content);
        }

        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
            string Content = "Reviews";
            MainWindow.SetContent(Content);
        }

        private void Costs_Click(object sender, RoutedEventArgs e)
        {
            string Content = "Costs";
            MainWindow.SetContent(Content);
        }
        #endregion
    }
}
