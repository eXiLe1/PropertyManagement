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
    /// Interaction logic for BookingsList.xaml
    /// </summary>
    public partial class BookingsList : UserControl
    {
        public BookingsList()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[BookingsList] Loaded");
            }
            InitializeControl();
        }

        private void InitializeControl()
        {
            var data = Database.DataReader($"SELECT BOOKING.*, PAYMENT.*, CUSTOMERS.* FROM (((BOOKINGMERGE INNER JOIN BOOKING ON BOOKINGMERGE.BookingID = BOOKING.BookingID) INNER JOIN PAYMENT ON BOOKINGMERGE.PaymentID = PAYMENT.PaymentID) INNER JOIN CUSTOMERS ON BOOKINGMERGE.CustomerID = CUSTOMERS.CustomerID) WHERE BOOKING.Checkout > '{DateTime.Now.ToString("yyyy-MM-dd")}'");
            if (data != null)
            {
                List<BookingSingleViewmodel> BookingsList = new List<BookingSingleViewmodel>();
                while (data.Read())
                {
                    var Checkin = (DateTime)data["Checkin"];
                    var Checkout = (DateTime)data["Checkout"];
                    BookingsList.Add(new BookingSingleViewmodel
                    {
                        ID = (int)data["BookingID"],
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
                        Mobile = (string)data["Mobile"],
                        Status = (string)data["Status"],
                        Total = (float)data["Total"],
                        Deposit = (float)data["Deposit"],
                        Adult = (int)data["Adult"],
                        Children = (int)data["Children"],
                        Checkin = (DateTime)data["Checkin"],
                        Checkout = (DateTime)data["Checkout"],
                        BookingDate = (DateTime)data["BookingDate"],
                        DatesFormat = Checkin.ToString("dd MMM, yyyy") + " - " + Checkout.ToString("dd MMM, yyyy"),
                        Duration = (Checkout - Checkin).Days
                    });
                }
                BookingSingleItems.ItemsSource = BookingsList;
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ReviewList:InitializeControl] Bookings loaded.");
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
    }
}
