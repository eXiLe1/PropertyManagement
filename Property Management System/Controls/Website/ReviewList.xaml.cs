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
using System.Data;

namespace Property_Management_System
{
    /// <summary>
    /// This is a User Control that presensts the reviews and allows the user to add/delete/edit other reviews.
    /// The database is queried for all the reviews and each is stored in a list of classes of each review. 
    /// </summary>
    public partial class ReviewList : UserControl
    {
        public ReviewList()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[ReviewList] Loaded.");
            }
            InitializeControl();
        }

        /// <summary>
        /// Sets up the control, fetches the data and creates instances of ReviewSingle for each review and binds to an instance of the ReviewSingleViewmodel
        /// </summary>
        private void InitializeControl()
        {
            List<ReviewSingleViewmodel> ReviewsList = new List<ReviewSingleViewmodel>();

            var data = Database.DataReader("SELECT * FROM REVIEWS ORDER BY ID DESC;");
            if (data != null)
            {
                while (data.Read())
                {
                    ReviewsList.Add(new ReviewSingleViewmodel
                    {
                        ID = (int)data["ID"],
                        Review = (string)data["Review"],
                        Rating = (int)data["Rating"]
                    });
                }
                ReviewSingleItems.ItemsSource = ReviewsList;
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

        /// <summary>
        /// For the Add Review box, this uploads the review to the database.
        /// </summary>
        /// <param name="sender">The button sending data</param>
        /// <param name="e">The data being sent, a click</param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //Validate Input
            if (ReviewField.Text == null || RatingField.Text == null)
            {
                MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Upload
                Database.ExQuery("INSERT INTO REVIEWS (Review, Rating) VALUES ('" + ReviewField.Text + "', '" + RatingField.Text + "')");
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ReviewList:Add] new review inserted.");
                }
                var result = MessageBox.Show("Review Added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                string Content = "Reviews";
                WebsiteHost.SetContent(Content);
            }
        }

        /// <summary>
        /// Just clears the group box.
        /// </summary>
        /// <param name="sender">The button sending data</param>
        /// <param name="e">The data being sent, a click</param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ReviewField.Clear();
            RatingField.SelectedItem = null;
        }
    }
}
