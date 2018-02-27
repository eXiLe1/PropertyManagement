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
    /// Interaction logic for TasksControl.xaml
    /// </summary>
    public partial class TasksControl : UserControl
    {
        private static TasksControl tasksControl;

        public TasksControl()
        {
            InitializeComponent();
            tasksControl = this;
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[TasksControl] Loaded.");
            }
            InitializeControl();
        }

        private void InitializeControl()
        {
            List<TaskViewmodel> TaskList = new List<TaskViewmodel>();
            var query = "";

            if (Properties.Settings.Default.User_ShowPastTasks)
            {
                query = "SELECT * FROM TASKS ORDER BY DueDate ASC;";
            }
            else
            {
                query = "SELECT * FROM TASKS WHERE DueDate  > '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY DueDate ASC;";
            }

            var data = Database.DataReader(query);
            if (data != null)
            {
                while (data.Read())
                {
                    var Colour = "";
                    var Icon = "";
                    var ButtonText = "";
                    switch (data["Status"])
                    {
                        case "incomplete":
                            Colour = "red";
                            Icon = "CheckAll";
                            ButtonText = "Mark as Complete";
                            break;
                        case "complete":
                            Colour = "LawnGreen";
                            Icon = "CloseBoxOutline";
                            ButtonText = "Mark as Incomplete";
                            break;
                        default:
                            Colour = "blue";
                            break;
                    }
                    var DateFormat = (DateTime)data["DueDate"];
                    TaskList.Add(new TaskViewmodel
                    {
                        ID = (int)data["ID"],
                        TaskName = (string)data["Name"],
                        Status = (string)data["Status"],
                        DueDate = DateFormat.ToString("dd MMM, yyyy"),
                        StatusColour = Colour,
                        Icon = Icon,
                        ButtonText = ButtonText
                    });
                }
                TaskSingleItems.ItemsSource = TaskList;
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ReviewList:InitializeControl] Reviews loaded.");
                }
                Database.Connection.Close();
            }
            else
            {
                MessageBox.Show("Error loading reviews.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ReviewList:InitializeControl] Reviews loading failed.");
                }
            }
        }

        public static void InitializeEditData(string ID, string Name, string DueDate)
        {
            tasksControl.EditButton.IsEnabled = true;
            tasksControl.CreateButton.IsEnabled = false;
            tasksControl.TaskNameField.Text = Name;
            tasksControl.DueDateField.Text = DueDate;
            tasksControl.ID.Text = ID;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            //Validate input
            if (string.IsNullOrWhiteSpace(TaskNameField.Text) || string.IsNullOrWhiteSpace(DueDateField.Text))
            {
                MessageBox.Show("Please enter required information.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var now = DateTime.Now;
                var DueDate = DueDateField.SelectedDate.Value.Date.ToString("yyyy-MM-dd");

                if (now > DueDateField.SelectedDate.Value.Date)
                {
                    //Fail
                    MessageBox.Show("Date must be in the future.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    //Upload
                    Database.ExQuery("INSERT INTO TASKS (ID, Name, Status, DueDate) VALUES (DEFAULT, '" + TaskNameField.Text + "', 'incomplete', '" + DueDate + "')");
                    if (Properties.Settings.Default.User_AdvancedLogging)
                    {
                        Log.Commit("[TasksControl:Create] New task added.");
                    }
                    var result = MessageBox.Show("Task Added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    string Content = "Tasks";
                    TasksHost.SetContent(Content);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //Validate
            if (string.IsNullOrWhiteSpace(TaskNameField.Text) || string.IsNullOrWhiteSpace(DueDateField.Text))
            {
                MessageBox.Show("Please enter required information.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var now = DateTime.Now;
                var DueDate = DueDateField.SelectedDate.Value.Date.ToString("yyyy-MM-dd");

                if (now > DueDateField.SelectedDate.Value.Date)
                {
                    //Fail
                    MessageBox.Show("Date must be in the future.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    //Upload
                    Database.ExQuery("UPDATE TASKS SET Name = '" + TaskNameField.Text + "', DueDate = '" + DueDate + "', Status = 'incomplete' WHERE ID = '" + ID.Text + "';");
                    if (Properties.Settings.Default.User_AdvancedLogging)
                    {
                        Log.Commit("[TaskSingle] Review with ID " + ID.Text + " Updated.");
                    }
                    string Content = "Tasks";
                    TasksHost.SetContent(Content);
                }
            }
        }
    }
}
