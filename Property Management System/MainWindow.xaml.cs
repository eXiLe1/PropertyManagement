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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //An instance of this window that allows other controls to access public methods. 
        private static MainWindow Window;

        public MainWindow()
        {
            InitializeComponent();
            //Set the instance of MainWindow created above to this window.
            Window = this;
            //Create a new log
            Log.NewLog();
            //Set label to false
            SetDatabaseStatus(false);
            //Set label to false
            SetFTPStatus(false);
            //Open the database connection
            Database.InitialOpen();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[Program] Loaded.");
            }
            //Set the content to a new instance of WelcomeHost
            MainContent.Content = new WelcomeHost();
            //new Analysis();
        }

        /// <summary>
        /// Changes the label at the bottom of the window indicating if the connection to the database is open or not.
        /// </summary>
        /// <param name="Status">Bool will be true if the label should be green.</param>
        public static void SetDatabaseStatus(bool Status)
        {
            if (Status)
            {
                //As this method is static we can't access the xaml. So we access it throught the instance of the window created above.
                Window.DatabaseStatus.Content = "Database Status: Online";
                Window.DatabaseStatus.Foreground = Brushes.LimeGreen;
            }
            else
            {
                Window.DatabaseStatus.Content = "Database Status: Offline";
                Window.DatabaseStatus.Foreground = Brushes.Red;
            }
        }

        public static void SetFTPStatus(bool Status)
        {
            if (Status)
            {
                //As this method is static we can't access the xaml. So we access it throught the instance of the window created above.
                Window.FTPStatus.Content = "FTP Status: Online";
                Window.FTPStatus.Foreground = Brushes.LimeGreen;
            }
            else
            {
                Window.FTPStatus.Content = "FTP Status: Offline";
                Window.FTPStatus.Foreground = Brushes.Red;
            }
        }

        /// <summary>
        /// Sets the content of the main content section of the window.
        /// </summary>
        /// <param name="Content">The usercontrol that is to be loaded.</param>
        public static void SetContent(string Content)
        {
            switch (Content)
            {
                case "Reviews":
                    //Set the content
                    Window.MainContent.Content = new WebsiteHost();
                    //Set the toolbar
                    Window.ToolbarContent.Content = new WebsiteToolbar();
                    //Set the specific content on the host control.
                    WebsiteHost.SetContent(Content);
                    break;
                case "Costs":
                    Window.MainContent.Content = new FinancesHost();
                    Window.ToolbarContent.Content = new FinancesToolbar();
                    FinancesHost.SetContent(Content);
                    break;
                case "Prices":
                    Window.MainContent.Content = new FinancesHost();
                    Window.ToolbarContent.Content = new FinancesToolbar();
                    FinancesHost.SetContent(Content);
                    break;
                case "Bookings":
                    Window.MainContent.Content = new BookingsHost();
                    Window.ToolbarContent.Content = new BookingsToolbar();
                    BookingsHost.SetContent(Content);
                    break;
                case "ViewCustomers":
                    Window.MainContent.Content = new CustomersHost();
                    Window.ToolbarContent.Content = new CustomersToolbar();
                    CustomersHost.SetContent(Content);
                    break;
                case "Tasks":
                    Window.MainContent.Content = new TasksHost();
                    Window.ToolbarContent.Content = new TasksToolbar();
                    TasksHost.SetContent(Content);
                    break;
            }
        }

        /// <summary>
        /// Closes the program.
        /// </summary>
        /// <param name="sender">The object sending the data.</param>
        /// <param name="e">The data being sent.</param>
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Opens the options page for the database connection
        /// </summary>
        /// <param name="sender">The menu item sending the data.</param>
        /// <param name="e">The data, a click</param>
        private void DatabaseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var options = new Options();
            options.Show();
            options.OptionsContent.Content = new DatabaseOptions();
        }

        /// <summary>
        /// Opens the options page for the email
        /// </summary>
        /// <param name="sender">The menu item sending the data.</param>
        /// <param name="e">The data, a click</param>
        private void EmailingItem_Click(object sender, RoutedEventArgs e)
        {
            var options = new Options();
            options.Show();
            options.OptionsContent.Content = new EmailOptions();
        }

        /// <summary>
        /// Opens the options window
        /// </summary>
        /// <param name="sender">The menu item sending the data.</param>
        /// <param name="e">The data, a click</param>
        private void OptionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var options = new Options();
            options.Show();
        }

        /// <summary>
        /// Will open a help page, not coded yet as it is not a requirement
        /// </summary>
        /// <param name="sender">The menu item sending the data.</param>
        /// <param name="e">The data, a click</param>
        private void HelpMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Allows the user to recheck the database connection if they are having issues.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshDatabaseStatus_Click(object sender, RoutedEventArgs e)
        {
            Database.Refresh();
        }

        //The following are all navigation buttons and take the user to the appropriate page.
        private void Website_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new WebsiteHost();
            ToolbarContent.Content = new WebsiteToolbar();
        }
        private void Bookings_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new BookingsHost();
            ToolbarContent.Content = new BookingsToolbar();
        }
        private void Finances_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new FinancesHost();
            ToolbarContent.Content = new FinancesToolbar();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new WelcomeHost();
            ToolbarContent.Content = null;
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CustomersHost();
            ToolbarContent.Content = new CustomersToolbar();
        }

        private void Tasks_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TasksHost();
            ToolbarContent.Content = new TasksToolbar();
        }

        private void Cloud_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
