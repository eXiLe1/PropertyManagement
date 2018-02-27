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
    /// Interaction logic for General.xaml
    /// </summary>
    public partial class GeneralOptions : UserControl
    {
        public GeneralOptions()
        {
            InitializeComponent();
            if(Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[GeneralOptions] Loaded");
            }
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                AdvancedLogging.IsChecked = true;
            }
            else
            {
                AdvancedLogging.IsChecked = false;
            }

            if (Properties.Settings.Default.User_ShowPastPrices)
            {
                ShowPastPrices.IsChecked = true;
            }
            else
            {
                ShowPastTasks.IsChecked = false;
            }

            if (Properties.Settings.Default.User_ShowPastTasks)
            {
                ShowPastTasks.IsChecked = true;
            }
            else
            {
                ShowPastTasks.IsChecked = false;
            }
        }

        #region XAML
        private void AdvancedLogging_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.User_AdvancedLogging = true;
            Properties.Settings.Default.Save();
        }

        private void AdvancedLogging_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.User_AdvancedLogging = false;
            Properties.Settings.Default.Save();
        }
        private void ShowPastPrices_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.User_ShowPastPrices = true;
            Properties.Settings.Default.Save();
        }

        private void ShowPastPrices_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.User_ShowPastPrices = false;
            Properties.Settings.Default.Save();
        }
        private void ShowPastTasks_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.User_ShowPastTasks = true;
            Properties.Settings.Default.Save();
        }

        private void ShowPastTasks_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.User_ShowPastTasks = false;
            Properties.Settings.Default.Save();
        }
        #endregion
    }
}
