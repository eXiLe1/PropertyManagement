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
    /// Interaction logic for TasksHost.xaml
    /// </summary>
    public partial class TasksHost : UserControl
    {
        private static TasksHost Window;
        public TasksHost()
        {
            InitializeComponent();
            Window = this;
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[TasksHost] Loaded.");
            }
            SetContent("Tasks");
        }

        public static void SetContent(string Content)
        {
            switch (Content)
            {
                case "Tasks":
                    Window.MainContent.Content = new TasksControl();
                    break;
            }
        }
    }
}
