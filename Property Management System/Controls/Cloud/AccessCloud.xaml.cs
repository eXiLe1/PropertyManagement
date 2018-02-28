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

namespace Property_Management_System
{
    /// <summary>
    /// Interaction logic for AccessCloud.xaml
    /// </summary>
    public partial class AccessCloud : UserControl
    {
        private readonly ObservableCollection<string> DirectoryCollection;

        public AccessCloud()
        {
            InitializeComponent();
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[AccessCloud] Loaded.");
            }
            InitializeControl();
        }

        private void InitializeControl()
        {
            string[] DirectoryList = Ftp.ListDirectory("/book");
            for (int i = 0; i < DirectoryList.Count(); i++)
            {
                DirectoryCollection.Add(DirectoryList[i]);
            }
            CostsDataGrid.ItemsSource = DirectoryCollection;
        }
    }
}
