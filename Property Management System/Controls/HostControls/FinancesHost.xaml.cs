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
    /// Interaction logic for FinancesHost.xaml
    /// </summary>
    public partial class FinancesHost : UserControl
    {
        private static FinancesHost Window;
        public FinancesHost()
        {
            InitializeComponent();
            Window = this;
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[FinanceHost] Loaded.");
            }
        }

        public static void SetContent(string Content)
        {
            switch (Content)
            {
                case "Prices":
                    Window.MainContent.Content = new Prices();
                    break;
                case "Costs":
                    Window.MainContent.Content = new CostReport();
                    break;
                case "Analysis":
                    break;
            }
        }
    }
}
