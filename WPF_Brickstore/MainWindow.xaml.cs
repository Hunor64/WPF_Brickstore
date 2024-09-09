using Microsoft.Win32;
using System.Collections.ObjectModel;
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
        List<BrickedData> currentFilter = new();
        ObservableCollection<string> cmbContent = new();

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
                currentFilter = brickData;
                dgBrickstore.ItemsSource = brickData;
            }
            LoadCMBItems();

        }
        public void Filter()
        {
            currentFilter = brickData.Where(x => x.itemName.ToLower().Contains(txtFilter.Text.ToLower()) && x.categoryName.ToLower().Contains(cmbFilterKat.Text.ToLower()) && x.itemId.Contains(txtFilterKod.Text.ToLower())).ToList();
            dgBrickstore.ItemsSource = currentFilter;

        }
        public void LoadCMBItems()
        {
            cmbContent = [];
            currentFilter.ForEach(x =>
            {
                if (x.categoryName.Contains(",") && cmbContent.Contains(x.categoryName.Split(',')[1]) == false)
                {
                    cmbContent.Add(x.categoryName.Split(',')[1].Trim());
                }
                if (cmbContent.Contains(x.categoryName) == false)
                {
                    if (x.categoryName.Contains(",") == false)
                    {
                        cmbContent.Add(x.categoryName.Trim());
                    }
                }
                
            });

            cmbFilterKat.ItemsSource = cmbContent.Distinct().OrderBy(x => x);
        }

        private void cmbFilterKat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Filter(object sender, TextChangedEventArgs e)
        {
            Filter();
            LoadCMBItems();
        }
    }
}