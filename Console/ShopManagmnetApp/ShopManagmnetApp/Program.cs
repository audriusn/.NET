using ShopManagmnetApp.Services;
using System;

namespace ShopManagmnetApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var applicationService = new ApplicationService();
            while (true)
            {
                Console.WriteLine("Enter your command:");
                var command = Console.ReadLine().ToLower();
                applicationService.Process(command);
            }
        }
    }
}
