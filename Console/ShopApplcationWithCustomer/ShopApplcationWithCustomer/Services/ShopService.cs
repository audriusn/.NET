using ShopApplcationWithCustomer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplcationWithCustomer.Services
{
    public class ShopService
    {
        private List<ShopItem> _items;
        private List<ShopItem> _cart;
        private Customer _customer = new Customer();

        public ShopService()
        {
            _items = new List<ShopItem>();
            _cart = new List<ShopItem>();
        }

        public void Add(string name, decimal price, int quantity)
        {
            if (_items.Any(si=>si.Name ==name))
                {
                throw new ArgumentException("Item is aleady exist");
            }
            var item = new ShopItem()
            {
                Name = name,
                Price = price,
                Quantity = quantity
            };
            _items.Add(item);
        }
        public void Remove(string name)
        {
            _items= _items.Where(si=>si.Name !=name).ToList();
        }
        public List<ShopItem>GetAll()
        {
            return _items;
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
        public void Update(string name, decimal price, int quantity )
        {
            ShopItem item = _items.First(si =>si.Name ==name);
            {
                item.Quantity = quantity;
                item.Price = price;
            }
        }
        public decimal Balance()
        {
            return _customer.Wallet;
        }
        public void Topup(decimal addmoney)
        {
            _customer.Wallet += addmoney;
        }

        public void Buy(string name, int quantity)
        {
            try
            {
                var item = _items.First(i => i.Name == name);
                if (item.Quantity >= quantity)
                {
                    if (item.Price * quantity <= _customer.Wallet)
                    {
                        item.Quantity -= quantity;
                        _customer.Wallet -= (item.Price * quantity);

                        var items = new ShopItem()
                        {
                            Name = name,
                            Quantity = quantity
                        };
                        _cart.Add(items);
                    }
                    else
                    {
                        Console.WriteLine("Customer does not have enough funds");
                    }
                }
                else
                {
                    Console.WriteLine("Shop does not have enaugth units");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Shop item is not found");
            }

        }
        public List<ShopItem> ShowCart()
        {
            return _cart;
        }
    }
}
