using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.Models
{
    public  class RegistersKlant
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _registerName;

        public string RegisterName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }
        private string _device;

        public string Device
        {
            get { return _device; }
            set { _device = value; }
        }

        private int _employeeId;

        public int EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }
        

        private string _employeeName;

        public string EmployeeName
        {
            get { return _employeeName; }
            set { _employeeName = value; }
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
