﻿using System;
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
    /// Interaction logic for CustomersHost.xaml
    /// </summary>
    public partial class CustomersHost : UserControl
    {
        private static CustomersHost Window;

        public CustomersHost()
        {
            InitializeComponent();
            Window = this;
            if (Properties.Settings.Default.User_AdvancedLogging)
            {
                Log.Commit("[CustomersHost] Loaded.");
            }
        }

        public static void SetContent(string Content)
        {
            switch (Content)
            {
                case "ViewCustomers":
                    Window.MainContent.Content = new ViewCustomers();
                    break;
                case "Analysis":
                    break;
            }
        }
    }
}
