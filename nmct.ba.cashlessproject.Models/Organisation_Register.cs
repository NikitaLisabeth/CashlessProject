using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.Models
{
    class Organisation_Register
    {
        private int _organisationId;

        public int OrganisationID
        {
            get { return _organisationId; }
            set { _organisationId = value; }
        }
        private int _registerId;

        public int RegisterId
        {
            get { return _registerId; }
            set { _registerId = value; }
        }
        private DateTime _fromDate;

        public DateTime FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }
        private DateTime _untilDate;

        public DateTime UntilDate
        {
            get { return _untilDate; }
            set { _untilDate = value; }
        }
    }
}
