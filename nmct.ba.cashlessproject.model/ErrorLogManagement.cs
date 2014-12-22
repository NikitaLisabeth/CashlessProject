using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.Models
{
    class ErrorLogManagement
    {
        private int _registerId;

        public int RegisterId
        {
            get { return _registerId; }
            set { _registerId = value; }
        }
        private Int32 _timestamp;

        public Int32 Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _stacktrace;

        public string Stacktrace
        {
            get { return _stacktrace; }
            set { _stacktrace = value; }
        }
    }
}
