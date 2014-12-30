using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Naam is verplicht!")]
        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }
        [Required(ErrorMessage = "Prijs is verplicht!")]
        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        [Required(ErrorMessage = "Categorie is verplicht!")]
        private string _category;

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }
        [Required(ErrorMessage = "Voorraad is verplicht!")]
        private int _stock;

        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
        
        
    }
}
