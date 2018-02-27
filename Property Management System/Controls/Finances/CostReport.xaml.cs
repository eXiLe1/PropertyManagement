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
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Property_Management_System
{
    /// <summary>
    /// Interaction logic for CostReport.xaml
    /// </summary>
    public partial class CostReport : UserControl
    {
        private float TotalCost;
        private readonly ObservableCollection<CostSingleViewmodel> CostList;
        private Dictionary<string, int> MonthDictionary = new Dictionary<string, int>()
        {
            {"January", 1},
            {"February", 2},
            {"March", 3},
            {"April", 4},
            {"May", 5},
            {"June", 6},
            {"July", 7},
            {"August", 8},
            {"September", 9},
            {"October", 10},
            {"November", 11},
            {"December", 12}
        };

        public CostReport()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[CostReport] Loaded.");
            }
            CostList = new ObservableCollection<CostSingleViewmodel>
            {
                new CostSingleViewmodel {CostName = "No Report Loaded" }
            };
            CostsDataGrid.ItemsSource = CostList;
        }

        private void Load(int year)
        {
            //Clear the collection so we dont have duplicates in the table.
            CostList.Clear();
            //Clear the total cost so we can start from 0.
            ClearTotalCost();
            //Select the data from the database for the year the user wants.
            var data = Database.DataReader("SELECT * FROM COSTS WHERE  YearDue = '" + year + "'");
            //From the database class the data var returns null if there was a connection error. So we only continue if it wasn't null.
            if (data != null)
            {
                //Cycle through the returned results line by line and put them into our collection.
                while (data.Read())
                {
                    //Add items to the total cost each time.
                    IncrementTotalCost((float)data["Total"]);
                    //Add each row to our collection in the form of a CostItem list.
                    CostList.Add(new CostSingleViewmodel
                    {
                        ID = (int)data["CostID"],
                        CostName = (string)data["Cost"],
                        YearDue = (int)data["YearDue"],
                        MonthDue = (string)data["MonthDue"],
                        CostDescription = (string)data["Description"],
                        CostTotal = (float)data["Total"]
                    });
                }
                //Set the datasource again back to our collection.
                CostsDataGrid.ItemsSource = CostList;
                //Set the label for the TotalCost.
                CostTotalLabel.Content = "Total Cost: £" + TotalCost;
                Database.Connection.Close();
            }
            else
            {
                MessageBox.Show("Error loading costs.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Cost Report:Load] Loading failed.");
                }
            }
        }

        //Samea s previous load procedure, however this uses the month as well as the year.
        private void Load(string month, int year)
        {
            CostList.Clear();
            ClearTotalCost();
            var data = Database.DataReader("SELECT * FROM `COSTS` WHERE (`MonthDue` = '" + month + "' AND `YearDue` = '" + year + "')");
            if (data != null)
            {
                while (data.Read())
                {
                    IncrementTotalCost((float)data["Total"]);
                    CostList.Add(new CostSingleViewmodel
                    {
                        ID = (int)data["CostID"],
                        CostName = (string)data["Cost"],
                        YearDue = (int)data["YearDue"],
                        MonthDue = (string)data["MonthDue"],
                        CostDescription = (string)data["Description"],
                        CostTotal = (float)data["Total"]
                    });
                }
                CostsDataGrid.ItemsSource = CostList;
                CostTotalLabel.Content = "Total Cost: £" + TotalCost;
                Database.Connection.Close();
            }
            else
            {
                MessageBox.Show("Error loading costs.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Cost Report:Load] Loading failed.");
                }
            }
        }

        private void IncrementTotalCost(float value) => TotalCost = TotalCost + value;

        private void ClearTotalCost() => TotalCost = 0;

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //Check if something is selceted
            if (CostsDataGrid.SelectedItem != null)
            {
                //Fetch the row and store it as a costItem to manipulate
                try
                {
                    CostSingleViewmodel selectedRow = (CostSingleViewmodel)CostsDataGrid.SelectedItem;
                    if (MessageBox.Show("This item will be deleted.", "Alert", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Database.ExQuery("DELETE FROM `COSTS` WHERE `CostID` = '" + selectedRow.ID + "'");
                        Load(MonthInput.Text, int.Parse(YearInput.Text));
                    }
                }
                catch
                {
                    MessageBox.Show("No item selected.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("No item selected.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ShowCostsButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(YearInput.Text))
            {
                MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(MonthInput.Text))
                {
                    Load(int.Parse(YearInput.Text));
                }
                else
                {
                    Load(MonthInput.Text, int.Parse(YearInput.Text));
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ///Setup variables that hold the month and year in a way we can manipulate
            var YearDueInt = int.Parse(YearDueInput.Text);
            var CurrentMonthString = DateTime.Now.ToString("MMMM");
            var CurrentYearString = DateTime.Now.ToString("yyyy");

            //Validate the input by making sure its not empty.
            if (string.IsNullOrWhiteSpace(YearDueInput.Text) || string.IsNullOrWhiteSpace(MonthDueInput.Text) || string.IsNullOrWhiteSpace(CostInput.Text) ||
                string.IsNullOrWhiteSpace(TotalInput.Text) || string.IsNullOrWhiteSpace(CommentsInput.Text))
            {
                MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //If due year is in past
                if (int.Parse(CurrentYearString) < int.Parse(YearDueInput.Text))
                {
                    //Upload
                    Database.ExQuery("INSERT INTO COSTS (YearDue, MonthDue, Cost, Total, Description) VALUES ('" + int.Parse(YearDueInput.Text) + "', '" + MonthDueInput.Text + "', '" + CostInput.Text + "', '" + TotalInput.Text + "', '" + CommentsInput.Text + "')");
                    if (Properties.Settings.Default.User_AdvancedLogging)
                    {
                        Log.Commit("[Costs] New cost added.");
                    }
                    MessageBox.Show("The cost has been added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Load(MonthInput.Text, int.Parse(YearInput.Text));
                }
                else if (int.Parse(CurrentYearString) == int.Parse(YearDueInput.Text))
                {
                    //Years are the same so we check months
                    if (MonthDictionary[CurrentMonthString] < MonthDictionary[MonthDueInput.Text])
                    {
                        //Month is in future so we upload
                        Database.ExQuery("INSERT INTO COSTS (YearDue, MonthDue, Cost, Total, Description) VALUES ('" + int.Parse(YearDueInput.Text) + "', '" + MonthDueInput.Text + "', '" + CostInput.Text + "', '" + TotalInput.Text + "', '" + CommentsInput.Text + "')");
                        if (Properties.Settings.Default.User_AdvancedLogging)
                        {
                            Log.Commit("[Costs] New cost added.");
                        }
                        MessageBox.Show("The cost has been added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Load(MonthInput.Text, int.Parse(YearInput.Text));
                    }
                    else
                    {
                        //month in past
                        MessageBox.Show("Please check due date.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    //year in past
                    MessageBox.Show("Please check due date.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Field_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
