using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Property_Management_System
{
    /// <summary>
    /// Interaction logic for Prices.xaml
    /// </summary>
    public partial class Prices : UserControl
    {
        public Prices()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[PricesControl] Loaded.");
            }
            InitializeControl();
        }

        private void InitializeControl()
        {
            var query = "";

            //Load base price
            var BasePriceData = Database.DataReader("SELECT PriceNightly, PriceWeekly FROM PRICING WHERE PricingID = 1");
            if(BasePriceData != null)
            {
                BasePriceData.Read();
                BaseNightPriceField.Text = "" + (int)BasePriceData["PriceNightly"] + "";
                BaseWeekPriceField.Text = "" + (int)BasePriceData["PriceWeekly"] + "";
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ReviewList:InitializeControl] Base prices loaded.");
                }
                Database.Connection.Close();
            }

            //Get the data from the database and populate our ObservableCollection
            if (Properties.Settings.Default.User_ShowPastPrices)
            {
                query = "SELECT * FROM PRICING";
            }
            else
            {
                query = "SELECT * FROM PRICING WHERE DateTo  > '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            }
            var data = Database.DataReader(query);
            if (data != null)
            {
                List<PriceViewmodel> PriceList = new List<PriceViewmodel>();
                while (data.Read())
                {
                    var dateFrom = (DateTime)data["DateFrom"];
                    var dateTo = (DateTime)data["DateTo"];
                    PriceList.Add(new PriceViewmodel
                    {
                        ID = (int)data["PricingID"],
                        DateFrom = (DateTime)data["DateFrom"],
                        DateTo = (DateTime)data["DateTo"],
                        DateFromFormat = dateFrom.ToString("dd MMM, yyyy"),
                        DateToFormat = dateTo.ToString("dd MMM, yyyy"),
                        PriceNightly = (int)data["PriceNightly"],
                        PriceWeekly = (int)data["PriceWeekly"]
                    });
                }
                PricesList.ItemsSource = PriceList;
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ReviewList:InitializeControl] Prices loaded.");
                }
                Database.Connection.Close();
            }
            else 
            {
                MessageBox.Show("Error loading data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Prices:InitializeControl] Loading failed.");
                }
            }
        }

        private void Field_PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //Check if there is something selected
            if (PricesList.SelectedItem != null)
            {
                if (MessageBox.Show("This item will be deleted.", "Alert", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //Fetch that row and store it as a PriceItem so it is easy to manipulate the data.
                    PriceViewmodel selectedRow = (PriceViewmodel)PricesList.SelectedItem;
                    Database.ExQuery("DELETE FROM PRICING WHERE PricingID = '" + selectedRow.ID + "'");
                    InitializeControl();
                }
            }
            else
            {
                MessageBox.Show("No item selected.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //Convert the dates into some friendly formats for the database
            var now = DateTime.Now;
            var StartDateUnformat = DateFromInput.SelectedDate.Value.Date;
            var EndDateUnformat = DateToInput.SelectedDate.Value.Date;
            var StartDate = StartDateUnformat.ToString("yyyy-MM-dd");
            var EndDate = EndDateUnformat.ToString("yyyy-MM-dd");

            //Validate Input
            if (DateFromInput.SelectedDate == null ||
                DateToInput.SelectedDate == null ||
                string.IsNullOrWhiteSpace(NightPriceField.Text) ||
                string.IsNullOrWhiteSpace(WeekPriceField.Text))
            {
                MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (now > StartDateUnformat || StartDateUnformat > EndDateUnformat)
                {
                    //Fail
                    MessageBox.Show("Please check dates.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    //Success
                    MySqlDataReader value = Database.DataReader("SELECT DateTo FROM PRICING WHERE (DateFrom BETWEEN '" + StartDate + "' AND '" + EndDate + "') OR (DateTo BETWEEN '" + StartDate + "' AND '" + EndDate + "') ");
                    if (value.HasRows)
                    {
                        //Previous entry found
                        MessageBox.Show("Cannot add multiple entries.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
                        Database.Connection.Close();
                    }
                    else
                    {
                        //Upload
                        Database.Connection.Close();
                        Database.ExQuery("INSERT INTO PRICING (DateFrom, DateTo, PriceNightly, PriceWeekly) VALUES ('" + StartDate + "', '" + EndDate + "', '" + NightPriceField.Text + "', '" + WeekPriceField.Text + "')");
                        if (Properties.Settings.Default.User_AdvancedLogging)
                        {
                            Log.Commit("[Prices] New price inserted.");
                        }
                        InitializeControl();
                    }
                    Database.Connection.Close();
                }
            }
        }

        private void BaseAdd_Click(object sender, RoutedEventArgs e)
        {
            //Validate Input
            if (string.IsNullOrWhiteSpace(BaseNightPriceField.Text) ||
                string.IsNullOrWhiteSpace(BaseWeekPriceField.Text))
            {
                MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Upload
                Database.ExQuery("UPDATE PRICING SET PriceNightly = '"+ BaseNightPriceField.Text +"', PriceWeekly = '"+ BaseWeekPriceField.Text +"' WHERE PricingID = 1;");
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Prices] New base price inserted.");
                }
                InitializeControl();
                Database.Connection.Close();
            }
        }
    }
}
