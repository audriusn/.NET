using System;
using WarehouseManagment.ConsoleApp.Services;

namespace WarehouseManagment.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var applicationService = new ApplicationService();

                Console.WriteLine("Enter your command:");
                var command = Console.ReadLine();

                applicationService.Process(command);
            }      
        }
    }
}
