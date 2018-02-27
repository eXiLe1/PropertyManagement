using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management_System
{
    class BookingSingleViewmodel : PaymentViewmodel
    {
        private int _adult;
        private int _children;
        private DateTime _checkin;
        private DateTime _checkout;
        private DateTime _bookingDate;
        private string _datesFormat;
        private int _duration;

        public int Adult { get => _adult; set => _adult = value; }
        public int Children { get => _children; set => _children = value; }
        public DateTime Checkin { get => _checkin; set => _checkin = value; }
        public DateTime Checkout { get => _checkout; set => _checkout = value; }
        public DateTime BookingDate { get => _bookingDate; set => _bookingDate = value; }
        public string DatesFormat { get => _datesFormat; set => _datesFormat = value; }
        public int Duration { get => _duration; set => _duration = value; }
    }
}
