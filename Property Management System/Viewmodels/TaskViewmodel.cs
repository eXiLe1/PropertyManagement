using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management_System
{
    class TaskViewmodel : BaseViewModel
    {
        private string _taskName;
        private string _status;
        private string _statusColour;
        private string _dueDate;
        private string _icon;
        private string _buttonText;

        public string TaskName { get => _taskName; set => _taskName = value; }
        public string Status { get => _status; set => _status = value; }
        public string StatusColour { get => _statusColour; set => _statusColour = value; }
        public string DueDate { get => _dueDate; set => _dueDate = value; }
        public string Icon { get => _icon; set => _icon = value; }
        public string ButtonText { get => _buttonText; set => _buttonText = value; }
    }
}
