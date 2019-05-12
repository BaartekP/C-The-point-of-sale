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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {           
                String x = SearchBox.Text;
                if(x != "") x = x.First().ToString().ToUpper() + x.Substring(1);
                String line;
                int counter = 0;
                QuantityBox.Text = "1";

                
                    try
                    {   
                        StreamReader sr = new StreamReader("products.txt");
                        if (SearchResoultGrid.Items.Count != 0) SearchResoultGrid.Items.Clear();
                     
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
                            if (p.get_barcode().Contains(x) || p.get_name().Contains(x) || x == null)
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

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = SearchResoultGrid.SelectedItem as Prod1;
            var itemsCount = SearchResoultGrid.Items.Count;
           
            if(itemsCount != 0)
            {
                var last = SearchResoultGrid.Items.GetItemAt(itemsCount - 1) as Prod1;
                Double y;
                Prod1 data;

                Double x = Convert.ToDouble(QuantityBox.Text);
                if(selectedItem!= null)
                {
                    y = Math.Round(selectedItem.s_price * x,2);
                    data = new Prod1 { s_name = selectedItem.s_name, s_quantity = x, s_price = y };
                }
                else
                {
                    y = Math.Round(last.s_price * x,2);
                    data = new Prod1 { s_name = last.s_name, s_quantity = x, s_price = y };
                }               
                ProductsGrid.Items.Add(data);
                summ.Text = (Convert.ToDouble(summ.Text) + y).ToString();
            }                
            else
                MessageBox.Show("Select the product from SearchGrid");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ProductsGrid.SelectedItem as Prod1;
            var itemsCount = ProductsGrid.Items.Count;
            var lastItem = ProductsGrid.Items.GetItemAt(itemsCount-1) as Prod1;

            if (selectedItem != null)
            {
                ProductsGrid.Items.Remove(selectedItem);
                summ.Text = (Convert.ToDouble(summ.Text) - selectedItem.s_price).ToString();
            }
            else if (itemsCount != 0)
            {
                ProductsGrid.Items.RemoveAt(itemsCount - 1);
                summ.Text = (Convert.ToDouble(summ.Text) - lastItem.s_price).ToString();
            }
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
            // StringBuilder strBuilder = new StringBuilder();
            //strBuilder.Append("Name\tQuantity\tPrice\n");

            using (StreamWriter file = new StreamWriter(file_name, true))
            {
                DateTime date = DateTime.Now;

                file.WriteLine($"{date.Day}.{date.Month}.{date.Year}");
                file.WriteLine("Name;Quantity;Price");

                for (int i = 0; i < ProductsGrid.Items.Count; i++)
                {
                    Prod1 product = (Prod1)ProductsGrid.Items[i];
                    file.WriteLine(product.s_name + ";" + product.s_quantity + ";" + product.s_price);
                    //strBuilder.Append(product.s_name + "\t" + product.s_quantity + "\t" + product.s_price);
                }
                file.WriteLine($"Summ;{summ.Text}");
                file.WriteLine("");
                // File.WriteAllText(file_name, strBuilder.ToString());
            }
        }

    }

    public class Prod1
    {
        public string s_name { get; set; }
        public double s_quantity { get; set; }
        public double s_price { get; set; }
    }
}

