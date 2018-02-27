using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management_System
{
    public class BaseViewModel
    {
        //Holds the ID of the data that the viewmodel has been created to hold.
        private int _iD;

        //Accessor function for the private variable _iD
        public int ID { get => _iD; set => _iD = value; }
    }
}
