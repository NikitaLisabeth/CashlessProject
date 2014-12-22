using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.Models
{
    public class Products
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }
        private double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        private string _category;

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }
        private int _stock;

        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
        
        
    }
}
