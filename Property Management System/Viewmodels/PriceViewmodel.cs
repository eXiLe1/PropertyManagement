using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management_System
{
    class PriceViewmodel : BaseViewModel
    {
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private int _priceNightly;
        private int _priceWeekly;
        private string _dateToFormat;
        private string _dateFromFormat;

        public DateTime DateFrom { get => _dateFrom; set => _dateFrom = value; }
        public DateTime DateTo { get => _dateTo; set => _dateTo = value; }
        public int PriceNightly { get => _priceNightly; set => _priceNightly = value; }
        public int PriceWeekly { get => _priceWeekly; set => _priceWeekly = value; }
        public string DateToFormat { get => _dateToFormat; set => _dateToFormat = value; }
        public string DateFromFormat { get => _dateFromFormat; set => _dateFromFormat = value; }

    }
}
