using ShopManagmnetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagmnetApp.Services
{
    public class ShopService
    {
        private List<ShopItem> _items;
        public ShopService()
        {
            _items = new List<ShopItem>();

        }
        public void Add(string name, string quantity)
        {
            if (_items.Any(si => si.Name == name))
            {
                throw new ArgumentException("Item already exists");
            }
            var item = new ShopItem()
            {
                Name = name,
                Quantity = quantity
            };        
                _items.Add(item);             
        }
        public void Remove(string name)
        {
            _items =_items.Where(i=>i.Name != name).ToList();
        }
        public List<ShopItem>GetAll()
        {
            return _items;
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
        public void Update(string name, string quantity)
        {
            ShopItem shopItem = _items.First(i => i.Name == name);
            {
                shopItem.Quantity = quantity;
            }
        }
    }
}
