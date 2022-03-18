using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineDemo.Contracts;

namespace VendingMachineDemo
{
    public class BiscuitItem : IBiscuitItem
    {
        public char ItemCode { get; set; }
        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public int Stock { get; set; }

        private List<BiscuitItem> _BiscuitItems = new List<BiscuitItem>();

        public void SetItems()
        {
            _BiscuitItems.Add(new BiscuitItem { ItemCode = 'A', ItemName = "Bikuit", ItemPrice = 6000, Stock = 2 });
            _BiscuitItems.Add(new BiscuitItem { ItemCode = 'B', ItemName = "Chips", ItemPrice = 8000, Stock = 1 });
            _BiscuitItems.Add(new BiscuitItem { ItemCode = 'C', ItemName = "Oreo", ItemPrice = 10000, Stock = 0 });
            _BiscuitItems.Add(new BiscuitItem { ItemCode = 'D', ItemName = "Tango", ItemPrice = 12000, Stock = 2 });
            _BiscuitItems.Add(new BiscuitItem { ItemCode = 'E', ItemName = "Cokelat", ItemPrice = 15000, Stock = 1 });
        }

        public void UpdateItem(char itemCode)
        {
            var data = _BiscuitItems.Where(x => x.ItemCode.Equals(itemCode)).FirstOrDefault();
            data.Stock -= 1;
        }

        public List<BiscuitItem> GetItems()
        {
            return _BiscuitItems;
        }
    }
}
