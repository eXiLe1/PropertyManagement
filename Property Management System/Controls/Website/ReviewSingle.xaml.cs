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
    /// Interaction logic for ReviewSingle.xaml
    /// </summary>
    public partial class ReviewSingle : UserControl
    {
        public ReviewSingle()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[ReviewSingle] Loaded");
            }
        }

        /// <summary>
        /// Adds the content of the textbox to the database.
        /// </summary>
        /// <param name="sender">The button sending the data</param>
        /// <param name="e">The data, the click.</param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //Validate input by checking it isn't empty
            if (ReviewTextBox.Text == null || RatingTextBox.Text == null)
            {
                //Display a message to the user
                MessageBox.Show("Please enter required information.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Update the selected review.
                Database.ExQuery("UPDATE REVIEWS SET Review = '" + ReviewTextBox.Text + "', Rating = '" + RatingTextBox.Text + "' WHERE ID = '" + ReviewID.Text + "';");
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ReviewSingle] Review with ID " + ReviewID.Text + " Updated.");
                }
                MessageBox.Show("Review Updated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Delete the selected review
        /// </summary>
        /// <param name="sender">The button sending the data</param>
        /// <param name="e">The click</param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //Check if the user is sure.
            if (MessageBox.Show("Are you sure you want to delete this review?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //Do the delete
                Database.ExQuery("DELETE FROM REVIEWS WHERE ID = '" + ReviewID.Text + "'");
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[ReviewSingle] Review with ID " + ReviewID.Text + " Deleted.");
                }
                MessageBox.Show("Review Deleted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                string Content = "Reviews";
                WebsiteHost.SetContent(Content);
            }
        }
    }
}
