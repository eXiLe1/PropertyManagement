using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management_System.Viewmodels
{
    class DirectoryListViewmodel
    {
        private string _costName;
        private int _yearDue;
        private string _monthDue;
        private string _costDescription;
        private float _costTotal;

        public string CostName { get => _costName; set => _costName = value; }
        public int YearDue { get => _yearDue; set => _yearDue = value; }
        public string MonthDue { get => _monthDue; set => _monthDue = value; }
        public string CostDescription { get => _costDescription; set => _costDescription = value; }
        public float CostTotal { get => _costTotal; set => _costTotal = value; }
    }
}
