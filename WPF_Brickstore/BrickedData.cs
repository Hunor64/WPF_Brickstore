using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Brickstore
{
    internal class BrickedData
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string categoryName { get; set; }
        public string colorName { get; set; }
        public int qty { get; set; }

        public BrickedData(int itemId, string itemName, string categoryName, string colorName, int qty)
        {
            this.itemId = itemId;
            this.itemName = itemName;
            this.categoryName = categoryName;
            this.colorName = colorName;
            this.qty = qty;
        }
    }
}
