using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.Models
{
    class Register_Employee
    {
        private int _registerId;

        public int RegisterId
        {
            get { return _registerId; }
            set { _registerId = value; }
        }
        private int _employeeId;

        public int EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }
        private DateTime _from;

        public DateTime From
        {
            get { return _from; }
            set { _from = value; }
        }

        private DateTime _until;

        public DateTime Until
        {
            get { return _until; }
            set { _until = value; }
        }
    }
}
