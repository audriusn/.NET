using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagment.ConsoleApp.Services
{
    public class WarehouseService
    {
        private List<WarehouseItem> _items;
        public WarehouseService()
        {
            _items = new List<WarehouseItem>();
        }
        public void Add(string name, string price)
        {
            var item = new WarehouseItem()
            { 
              Name = name,
              Price = price
            };
            _items.Add(item);
        }
        public void Remove(string name)
        {
            _items.Where(i => i.Name != name).ToList();
        }
        public List<WarehouseItem> GetAll()  
        {
            return _items;
        }
    }
}

