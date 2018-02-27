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
    /// Interaction logic for FinancesToolbar.xaml
    /// </summary>
    public partial class FinancesToolbar : UserControl
    {
        public FinancesToolbar()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[FinancesToolbar] Loaded.");
            }
        }

        private void Analytics_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Prices_Click(object sender, RoutedEventArgs e)
        {
            string Content = "Prices";
            FinancesHost.SetContent(Content);
        }

        private void Costs_Click(object sender, RoutedEventArgs e)
        {
            string Content = "Costs";
            FinancesHost.SetContent(Content);
        }
    }
}
