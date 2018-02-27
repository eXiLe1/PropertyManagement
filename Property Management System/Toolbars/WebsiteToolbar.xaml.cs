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
    /// Interaction logic for WebsiteToolbar.xaml
    /// </summary>
    public partial class WebsiteToolbar : UserControl
    {
        public WebsiteToolbar()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[WebsiteToolbar] Loaded.");
            }
        }

        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
            string Content = "Reviews";
            WebsiteHost.SetContent(Content);
        }
    }
}
