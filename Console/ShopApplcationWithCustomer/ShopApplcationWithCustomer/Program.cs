
using ShopApplcationWithCustomer.Services;

var applicationService = new ApplicationService();

while (true)
{
    Console.WriteLine("Enter yor command:");
    var command = Console.ReadLine();
    applicationService.Process(command);
}