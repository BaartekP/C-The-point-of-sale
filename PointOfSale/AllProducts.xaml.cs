using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace PointOfSale
{
    /// <summary>
    /// Logika interakcji dla klasy AllProducts.xaml
    /// </summary>
    public partial class AllProducts : Window
    {
        public AllProducts()
        {
            InitializeComponent();

            String line;
            int counter = 0;
            try
            {   // Open the text file using a stream reader.
                StreamReader sr = new StreamReader("products.txt");
                //if (SearchResoultGrid.Items.Count != 0) SearchResoultGrid.Items.Clear();
                // Read the stream to a string, and write the string to the console.
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Replace('.', ',');
                    if (counter == 0) { counter++; continue; }
                    String[] parts1 = line.Split('\n');
                    String[] parts2 = parts1[0].Split(';');
                    Product p = new Product();
                    p.set_barcode(parts2[0]);
                    p.set_name(parts2[1]);
                    p.set_price(Convert.ToDouble(parts2[2]));

                    var data = new All { name = p.get_name(), barcode = p.get_barcode(), price = p.get_price() };
                    allProducts.Items.Add(data);
                    
                    counter++;
                }
            }
            catch (IOException a)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(a.Message);
            }


        }
    }

    public class All
    {
        public string name { get; set; }
        public string barcode{ get; set; }
        public double price { get; set; }
    }
}
