using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagment.ConsoleApp.Services
{
    internal class ApplicationService
    {
        private WarehouseService _warehouseService;

        public ApplicationService()
        {
            _warehouseService = new WarehouseService();
        }
        public void Process (string command)
        {
            if (command.StartsWith("Add"))
            {
                _warehouseService.Add();
            }
            if (command.StartsWith("Remove"))
            {
                _warehouseService.Remove();
            }
            if (command.StartsWith("List"))
            {
                _warehouseService.List();
            }
            if (command.StartsWith("Exit"))
            {
                return;
            }
            Console.WriteLine("Incorrect command");
         
        }
    }
}
