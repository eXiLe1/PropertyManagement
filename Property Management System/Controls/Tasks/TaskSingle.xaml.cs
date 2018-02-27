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
    /// Interaction logic for TaskSingle.xaml
    /// </summary>
    public partial class TaskSingle : UserControl
    {
        public TaskSingle()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[TaskSingle] Loaded");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            TasksControl.InitializeEditData(ID.Text, TaskName.Text, DueDate.Text);
        }

        private void StatusButton_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Status.Content == "complete")
            {
                //Update the selected task.
                Database.ExQuery("UPDATE TASKS SET Status = 'incomplete' WHERE ID = '" + ID.Text + "';");
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[TaskSingle] Review with ID " + ID.Text + " Updated.");
                }
            }
            else
            {
                Database.ExQuery("UPDATE TASKS SET Status = 'complete' WHERE ID = '" + ID.Text + "';");
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[TaskSingle] Review with ID " + ID.Text + " Updated.");
                }
            }
            string Content = "Tasks";
            TasksHost.SetContent(Content);
        }
    }
}
