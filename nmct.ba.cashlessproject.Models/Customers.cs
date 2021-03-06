﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.Models
{
    public class Customers
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _customerName;

        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        private string _picture;

        public string Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }
        private double _balance;

        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
    }
}
