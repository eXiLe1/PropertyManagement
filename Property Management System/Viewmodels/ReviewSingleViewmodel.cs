using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management_System
{
    class ReviewSingleViewmodel : BaseViewModel
    {
        private string _review;
        private int _rating;

        public string Review { get => _review; set => _review = value; }
        public int Rating { get => _rating; set => _rating = value; }
    }
}
