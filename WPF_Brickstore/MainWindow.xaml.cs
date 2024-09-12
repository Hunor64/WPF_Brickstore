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

        public void LoadXamlFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "BSX Files (*.bsx)|*.bsx";
            if (openFileDialog.ShowDialog() == true)
            {
                brickData = new();
                XDocument xaml = XDocument.Load(openFileDialog.FileName);
                foreach (var elem in xaml.Descendants("Item"))
                {                    
                    brickData.Add(new BrickedData(elem.Element("ItemID").Value, elem.Element("ItemName").Value, elem.Element("CategoryName").Value, elem.Element("ColorName").Value, int.Parse(elem.Element("Qty").Value)));
                }
                currentFilter = brickData;
                dgBrickstore.ItemsSource = brickData;
            }
            LoadCMBItems();

        }
        private void LoadXamlFolder(object sender, RoutedEventArgs e)
        {

        }
        public void Filter()
        {
            string filterText = txtFilter.Text.ToLower();
            string filterKodText = txtFilterKod.Text.ToLower();
            string filterKatText = cmbFilterKat.Text.ToLower();

            currentFilter = brickData.Where(x => x.itemName.ToLower().StartsWith(filterText) && x.itemId.ToLower().Contains(filterKodText)).ToList();
            if (cmbFilterKat.SelectedIndex == 0)
            {
                dgBrickstore.ItemsSource = currentFilter;
            }
            else
            {
                dgBrickstore.ItemsSource = currentFilter.Where(x => x.categoryName.ToLower().Contains(filterKatText));
            }
        }
        public void LoadCMBItems()
        {
            cmbContent = ["-Nincs Filter-"];
            currentFilter.ForEach(x =>
            {
                if (x.categoryName.Contains(","))
                {
                    cmbContent.Add(x.categoryName.Split(',')[0].Trim());
                    cmbContent.Add(x.categoryName.Split(',')[1].Trim());
                }
                else
                {
                    cmbContent.Add(x.categoryName.Trim());
                }


            });

            cmbFilterKat.ItemsSource = cmbContent.Distinct().OrderBy(x => x);
            cmbFilterKat.SelectedIndex = 0;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
            LoadCMBItems();

        }

        private void cmbFilterKat_DropDownClosed(object sender, EventArgs e)
        {
            Filter();
        }
        
        private void cmbFileSelect_DropDownClosed(object sender, EventArgs e)
        {
            OpenFolderDialog openFolderDialog = new();
            if (openFolderDialog.ShowDialog() == true)
            {
                cmbFileSelect.ItemsSource = openFolderDialog.FolderName;
            }

        }
    }
}