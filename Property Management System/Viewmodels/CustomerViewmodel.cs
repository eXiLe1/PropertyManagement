using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management_System
{
    public class CustomerViewmodel : BaseViewModel
    {
        private string _firstname;
        private string _lastname;
        private string _fullname;
        private string _email;
        private string _addressOne;
        private string _addressTwo;
        private string _city;
        private string _county;
        private string _postcode;
        private string _country;
        private string _mobile;

        public string Firstname { get => _firstname; set => _firstname = value; }
        public string Lastname { get => _lastname; set => _lastname = value; }
        public string Fullname { get => _fullname; set => _fullname = value; }
        public string Email { get => _email; set => _email = value; }
        public string AddressOne { get => _addressOne; set => _addressOne = value; }
        public string AddressTwo { get => _addressTwo; set => _addressTwo = value; }
        public string City { get => _city; set => _city = value; }
        public string County { get => _county; set => _county = value; }
        public string Postcode { get => _postcode; set => _postcode = value; }
        public string Country { get => _country; set => _country = value; }
        public string Mobile { get => _mobile; set => _mobile = value; }
    }
}
