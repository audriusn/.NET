using ShopManagmnetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagmnetApp.Services
{
    public class ApplicationService
    {
        private ShopService _shopService;

        public ApplicationService()
        {
            _shopService = new ShopService();   
        }
        public void Process (string command)
        {
            try
            {
                if (command.StartsWith("add"))
                {
                    string[] splitCommand = command.Split(" ");
                    _shopService.Add(splitCommand[1], splitCommand[2]);
                }

                else if (command.StartsWith("remove"))
                {
                    string[] splitComamnd = command.Split(" ");
                    _shopService.Remove(splitComamnd[1]);
                }
                else if (command.StartsWith("show"))
                {
                    List<ShopItem> items = _shopService.GetAll();
                    foreach (ShopItem item in items)
                    {
                        Console.WriteLine($"ItemName: {item.Name} ItemPrice: {item.Quantity}");
                    }
                }
                else if (command.StartsWith("set"))
                {
                    string[] splitCommand = command.Split(" ");
                    _shopService.Update(splitCommand[1], splitCommand[2]);
                }
                
                else if (command.StartsWith("exit"))
                {
                    _shopService.Exit();
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Bad command name.You can only use 'Add', 'Remove', 'Show', 'Set' or 'Exit'");
            }
        }
    }
}
