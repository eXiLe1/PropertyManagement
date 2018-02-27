using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management_System
{
    class PaymentViewmodel : CustomerViewmodel
    {
        private string _status;
        private float _total;
        private float _deposit;

        public string Status { get => _status; set => _status = value; }
        public float Total { get => _total; set => _total = value; }
        public float Deposit { get => _deposit; set => _deposit = value; }
    }
}
