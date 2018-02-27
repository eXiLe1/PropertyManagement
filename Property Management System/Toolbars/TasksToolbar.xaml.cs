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
    /// Interaction logic for BookingsToolbar.xaml
    /// </summary>
    public partial class TasksToolbar : UserControl
    {
        public TasksToolbar()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[BookingsToolbar] Loaded.");
            }
        }

        private void Tasks_Click(object sender, RoutedEventArgs e)
        {
            string Content = "Tasks";
            TasksHost.SetContent(Content);
        }
    }
}
