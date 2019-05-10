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
            String x = this.SearchBox.Text;
            String line;
            int counter = 0;

            if(x != null)
            {
                try
                {   // Open the text file using a stream reader.
                    StreamReader sr = new StreamReader("products.txt");
                        // Read the stream to a string, and write the string to the console.
                        while ((line = sr.ReadLine()) != null)
                        {
                            String[] parts1 = line.Split('\n');
                            String[] parts2 = parts1[0].Split(':');
                        foreach (String s in parts2){
                            if (s == x) { }
                                
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
    }
}
