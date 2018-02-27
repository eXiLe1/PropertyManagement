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
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Property_Management_System
{
    /// <summary>
    /// Interaction logic for ViewCustomers.xaml
    /// </summary>
    public partial class ViewCustomers : UserControl
    {
        //An instance of this control that allows other controls to access public methods. 
        public static ViewCustomers Control;

        public ViewCustomers()
        {
            InitializeComponent();
            Control = this;
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[ViewCustomers] Loaded.");
            }
            InitializeControl();
        }

        /// <summary>
        /// Sets up the control by fetching data from the database, loading it into a viewmodel and saving that viewmodel to a list.
        /// </summary>
        private void InitializeControl()
        {
            //Get the data from the database and populate our table
            var data = Database.DataReader("SELECT * FROM CUSTOMERS");
            if (data != null)
            {
                List<CustomerViewmodel> CustomerViewmodelList = new List<CustomerViewmodel>();
                while (data.Read())
                {
                    CustomerViewmodelList.Add(new CustomerViewmodel
                    {
                        ID = (int)data["CustomerID"],
                        Firstname = (string)data["Firstname"],
                        Lastname = (string)data["Lastname"],
                        Fullname = (string)data["Firstname"] +" "+ (string)data["Lastname"],
                        Email = (string)data["Email"],
                        AddressOne = (string)data["AddressOne"],
                        AddressTwo = (string)data["AddressTwo"],
                        City = (string)data["City"],
                        County = (string)data["County"],
                        Postcode = (string)data["Postcode"],
                        Country = (string)data["Country"],
                        Mobile = (string)data["Mobile"]
                    });
                }
                //Set the source of the datagrid to the list.
                CustomerList.ItemsSource = CustomerViewmodelList;
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ViewCustoemrs:InitializeControl] Customers loaded.");
                }
                Database.Connection.Close();
            }
            else
            {
                MessageBox.Show("Error loading data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ViewCustomers:InitializeControl] Loading failed.");
                }
            }
        }

        /// <summary>
        /// Identical to the other InitializeControl, however through polymorphism we can load in a specific customer from the query.
        /// </summary>
        /// <param name="query">SQL query to select specific customer.</param>
        public void InitializeControl(string query)
        {
            //Get the data from the database and populate our table
            var data = Database.DataReader(query);
            if (data != null)
            {
                List<CustomerViewmodel> CustomerViewmodelList = new List<CustomerViewmodel>();
                while (data.Read())
                {
                    CustomerViewmodelList.Add(new CustomerViewmodel
                    {
                        ID = (int)data["CustomerID"],
                        Firstname = (string)data["Firstname"],
                        Lastname = (string)data["Lastname"],
                        Fullname = (string)data["Firstname"] + " " + (string)data["Lastname"],
                        Email = (string)data["Email"],
                        AddressOne = (string)data["AddressOne"],
                        AddressTwo = (string)data["AddressTwo"],
                        City = (string)data["City"],
                        County = (string)data["County"],
                        Postcode = (string)data["Postcode"],
                        Country = (string)data["Country"],
                        Mobile = (string)data["Mobile"]
                    });
                }
                CustomerList.ItemsSource = CustomerViewmodelList;
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ViewCustoemrs:InitializeControl] Customers loaded from query: "+ query);
                }
                Database.Connection.Close();
            }
            else
            {
                MessageBox.Show("Error loading data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ViewCustomers:InitializeControl] Loading failed from query: "+ query);
                }
            }
        }

        //Checks what the user enters matches the regex. If it doesn't the character won't be displayed. This regex allows letters only.
        private void TextField_PreviewTextInput(object sender, TextCompositionEventArgs e) => Regex.IsMatch(e.Text, @"^[a-zA-Z]+$");

        //Checks what the user enters matches the regex. If it doesn't the character won't be displayed. This regex allows numbers only.
        private void MobileField_PreviewTextInput(object sender, TextCompositionEventArgs e) => Regex.IsMatch(e.Text, "[^0-9]+");

        //Searches the database by one of the terms specified.
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "";
            if(string.IsNullOrWhiteSpace(FirstnameField.Text))
            {
                if (string.IsNullOrWhiteSpace(LastnameField.Text))
                {
                    if (string.IsNullOrWhiteSpace(EmailField.Text))
                    {
                        if (string.IsNullOrWhiteSpace(CountryField.Text))
                        {
                            MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            //Search by Country
                            query = "SELECT * FROM CUSTOMERS WHERE Country = '"+ CountryField.Text +"'";
                            InitializeControl(query);
                        }
                    }
                    else
                    {
                        //Search by Email
                        query = "SELECT * FROM CUSTOMERS WHERE Email = '" + EmailField.Text + "'";
                        InitializeControl(query);
                    }
                }
                else
                {
                    //Search by Lastname
                    query = "SELECT * FROM CUSTOMERS WHERE Lastname = '" + LastnameField.Text + "'";
                    InitializeControl(query);
                }
            }
            else
            {
                //Seach by firstname
                query = "SELECT * FROM CUSTOMERS WHERE Firstname = '" + FirstnameField.Text + "'";
                InitializeControl(query);
            }
        }

        //Loads the details of the selected customer into the edit box so they can be edited by the user.
        private void Manage_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerList.SelectedItem != null)
            {
                //Fetch that row and store it as a PriceItem so it is easy to manipulate the data.
                CustomerViewmodel SelectedCustomer = (CustomerViewmodel)CustomerList.SelectedItem;
                EditID.Text = ""+SelectedCustomer.ID+"";
                EditFirstnameField.Text = SelectedCustomer.Firstname;
                EditLastnameField.Text = SelectedCustomer.Lastname;
                EditEmailField.Text = SelectedCustomer.Email;
                EditAddressOneField.Text = SelectedCustomer.AddressOne;
                EditAddressTwoField.Text = SelectedCustomer.AddressTwo;
                EditCityField.Text = SelectedCustomer.City;
                EditCountyField.Text = SelectedCustomer.County;
                EditPostcodeField.Text = SelectedCustomer.Postcode;
                EditCountryField.Text = SelectedCustomer.Country;
                EditMobileField.Text = SelectedCustomer.Mobile;
            }
            else
            {
                MessageBox.Show("No item selected.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Clears the search fields so all customers are shown again.
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            FirstnameField.Clear();
            LastnameField.Clear();
            EmailField.Clear();
            CountryField.Clear();
            InitializeControl();
        }

        //Saves the customer to the database.
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Validate Input
            if (string.IsNullOrWhiteSpace(EditFirstnameField.Text) ||
                string.IsNullOrWhiteSpace(EditLastnameField.Text) ||
                string.IsNullOrWhiteSpace(EditEmailField.Text) ||
                string.IsNullOrWhiteSpace(EditAddressOneField.Text) ||
                string.IsNullOrWhiteSpace(EditAddressTwoField.Text) ||
                string.IsNullOrWhiteSpace(EditCityField.Text) ||
                string.IsNullOrWhiteSpace(EditCountyField.Text) ||
                string.IsNullOrWhiteSpace(EditPostcodeField.Text) ||
                string.IsNullOrWhiteSpace(EditCountryField.Text) ||
                string.IsNullOrWhiteSpace(EditMobileField.Text))
            {
                MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Upload
                Database.ExQuery("UPDATE CUSTOMERS SET Firstname = '" + EditFirstnameField.Text + "', Lastname = '" + EditLastnameField.Text + "'," +
                    " Email = '" + EditEmailField.Text + "', AddressOne = '" + EditAddressOneField.Text + "', AddressTwo = '" + EditAddressTwoField.Text + "'," +
                    " City = '" + EditCityField.Text + "', County = '" + EditCountyField.Text + "', Postcode = '" + EditPostcodeField.Text + "'," +
                    " Country = '" + EditCountryField.Text + "', Mobile = '" + EditMobileField.Text + "' WHERE CustomerID = '"+ EditID.Text +"';");
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ViewCustomer] Customer updated: " + EditID.Text);
                }
                Database.Connection.Close();
                InitializeControl();
            }
        }

        //Opens the email window and fills it with the data for the selected customer so they can be emailed.
        private void Email_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerList.SelectedItem != null)
            {
                //Fetch that row and store it as a CustomerViewmodel so it is easy to manipulate the data.
                CustomerViewmodel SelectedCustomer = (CustomerViewmodel)CustomerList.SelectedItem;
                EditFirstnameField.Text = SelectedCustomer.Firstname;
                EditLastnameField.Text = SelectedCustomer.Lastname;
                EditEmailField.Text = SelectedCustomer.Email;
                EmailWindow emailWindow = new EmailWindow();
                emailWindow.Show();
                EmailWindow.SetContent(SelectedCustomer.Email, SelectedCustomer.Firstname, SelectedCustomer.Lastname);
            }
            else
            {
                MessageBox.Show("No item selected.", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
