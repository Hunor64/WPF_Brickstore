using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPF_Brickstore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<BrickedData> brickData = new();
        public MainWindow()
        {
            InitializeComponent();

        }
        public void LoadXaml(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "XML Files (*.bsx)|*.bsx";
            if (openFileDialog.ShowDialog() == true)
            {
                brickData = new();
                XDocument xaml = XDocument.Load(openFileDialog.FileName);
                foreach (var elem in xaml.Descendants("Item"))
                {
                    string itemId = elem.Element("ItemID").Value;
                    string itemName = elem.Element("ItemName").Value;
                    string categoryName = elem.Element("CategoryName").Value;
                    string colorName = elem.Element("ColorName").Value;
                    int qty = int.Parse(elem.Element("Qty").Value);

                    BrickedData brick = new BrickedData(itemId, itemName, categoryName, colorName, qty);
                    brickData.Add(brick);
                }
                dgBrickstore.ItemsSource = brickData;
            }

        }
        public void Filter(object sender, TextChangedEventArgs e)
        {
            string filter = txtFilter.Text;
            if (filter == "")
            {
                dgBrickstore.ItemsSource = brickData;
            }
            else
            {
                dgBrickstore.ItemsSource = brickData.Where(x => x.itemName.ToLower().Contains(filter.ToLower()) || x.itemId.Contains(filter.ToLower()));
            }

        }
    }
}