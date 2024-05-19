using ShopApplcationWithCustomer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplcationWithCustomer.Services
{
    public class ApplicationService
    {
        private ShopService _shopService;
        public object item;

        public ApplicationService()
        {
            _shopService = new ShopService();
        }
       public void Process (string command)
        {
            try
            {
                command = command.ToLower();
                string[] splitCommand = command.Split(" ");

                if (command.StartsWith("add"))
                {              
                    _shopService.Add(splitCommand[1], decimal.Parse(splitCommand[2]), int.Parse(splitCommand[3]));                   
                }          
                else if (command.StartsWith("remove"))
                {                 
                    _shopService.Remove(splitCommand[1]);
                }
                else if (command.StartsWith("show"))
                {
                    List<ShopItem> items = _shopService.GetAll();
                    foreach (ShopItem item in items)
                    {
                        Console.WriteLine($"Item name: {item.Name} Item Price: {item.Price} Item Quantity: {item.Quantity}");
                    }
                }
                else if (command.StartsWith("exit"))
                {
                    _shopService.Exit();
                }
                else if (command.StartsWith("set"))
                {
                    decimal price = decimal.Parse(splitCommand[2]);
                    int quantity = int.Parse(splitCommand[3]);
                    _shopService.Update(splitCommand[1], price, quantity);
                }
                else if (command.StartsWith("balance"))
                {
                   Console.WriteLine("Your balance is: {0}", _shopService.Balance());
                }
                else if (command.StartsWith("topup"))
                {
                    decimal sum = decimal.Parse(splitCommand[1]);
                    _shopService.Topup(sum);
                    Console.WriteLine("Top up done. Your balance is: {0}", _shopService.Balance());
                }
                else if (command.StartsWith("buy"))
                {
                    int qty=int.Parse(splitCommand[2]);
                    _shopService.Buy(splitCommand[1], qty);
                }
                else if (command.StartsWith("cart"))
                {
                    List<ShopItem> items = _shopService.ShowCart();
                    foreach (ShopItem item in items)
                    {
                        Console.WriteLine($"Item name: {item.Name} Item quntity: {item.Quantity}");
                    }
                }
            }
            catch
            {
                ArgumentException ex;
                Console.WriteLine("Incorrect command. Please try again");
                            }
        }
    }
}
