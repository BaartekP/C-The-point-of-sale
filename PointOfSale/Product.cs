using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale
{
    public class Product
    {
        private String name;
        private String barcode;
        private double price;

        //Get
        public String get_name()    { return this.name; }
        public String get_barcode() { return this.barcode; }
        public double get_price()   { return this.price; }

        //Set
        public void set_name(String name)       { this.name = name; }
        public void set_barcode(String barcode) { this.barcode = barcode; }
        public void set_price(double price)     { this.price = price; }

        
    }
}
