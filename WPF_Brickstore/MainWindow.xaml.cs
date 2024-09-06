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
            LoadXaml();
            dgBrickstore.ItemsSource = brickData;

        }
        public void LoadXaml()
        {
            XDocument xaml = XDocument.Load("brickstore_parts_4643-1-power-boat-transporter.bsx");
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
        }
    }
}