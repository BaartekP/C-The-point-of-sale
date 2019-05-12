using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace PointOfSale
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Product> spList = new List<Product>();
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text != null)
            {
                String x = SearchBox.Text;
                x = x.First().ToString().ToUpper() + x.Substring(1);
                String line;
                int counter = 0;
                QuantityBox.Text = "1";

                if (x != null)
                {
                    try
                    {   // Open the text file using a stream reader.
                        StreamReader sr = new StreamReader("products.txt");
                        if (SearchResoultGrid.Items.Count != 0) SearchResoultGrid.Items.Clear();
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
                            if (p.get_barcode().Contains(x) || p.get_name().Contains(x))
                            {
                                var data = new Prod1 { s_name = p.get_name(), s_price = p.get_price(), s_quantity = 1 };
                                SearchResoultGrid.Items.Add(data);
                            }
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
            else
                MessageBox.Show("SearchBox is empty!!!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var data = new Test { Test1 = "Test1", Test2 = "Test2" };

            // DataGridTest.Items.Add(data);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var pers1 = SearchResoultGrid.SelectedItem as Prod1;
            var lastItem = SearchResoultGrid.Items.Count;
           
            if (pers1 != null)
            {
                Double x = Convert.ToDouble(QuantityBox.Text);
                Double y = pers1.s_price * x;
                var data = new Prod1 { s_name = pers1.s_name, s_quantity = x, s_price = y };
                ProductsGrid.Items.Add(data);
                summ.Text = (Convert.ToDouble(summ.Text) + y).ToString();
                //   QuantityBox.Text = pers1.s_quantity.ToString();
            }
            else if(lastItem != 0)
            {
                var last = SearchResoultGrid.Items.GetItemAt(lastItem - 1) as Prod1;
                Double x = Convert.ToDouble(QuantityBox.Text);
                Double y = last.s_price * x;
                var data = new Prod1 { s_name = last.s_name, s_quantity = x, s_price = y };
                ProductsGrid.Items.Add(data);
                summ.Text = (Convert.ToDouble(summ.Text) + y).ToString();
            }
                
            else
                MessageBox.Show("Select the product from SearchGrid");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ProductsGrid.SelectedItem;
            var lastItem = ProductsGrid.Items.Count;

            if (selectedItem != null)
            {
                ProductsGrid.Items.Remove(selectedItem);
            }
            else if (lastItem != 0)
                ProductsGrid.Items.RemoveAt(lastItem-1);
            else
                MessageBox.Show("ProductList is empty!!!");


        }

        private void AllProducts_Click(object sender, RoutedEventArgs e)
        {
            AllProducts allProducts = new AllProducts();
            allProducts.Show();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string file_name = "ProductsList.txt";
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("Name\tQuantity\tPrice\n");

            for (int i = 0; i < ProductsGrid.Items.Count; i++)
            {
                Prod1 prsn = (Prod1)ProductsGrid.Items[i];
                strBuilder.Append(prsn.s_name+"\t"+prsn.s_quantity+"\t"+prsn.s_price+"\n");
            }
            File.WriteAllText(file_name, strBuilder.ToString());
        }
    }


    public class Test
    {
        public string Test1 { get; set; }
        public string Test2 { get; set; }
    }

    public class Prod1
    {
        public string s_name { get; set; }
        public double s_quantity { get; set; }
        public double s_price { get; set; }
    }
}

