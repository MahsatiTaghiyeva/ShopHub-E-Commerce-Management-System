using ShopHub.Models;

namespace ShopHub.Interfaces;

public interface ICustomerService
{
    void AddCustomer(Customer customer);
    Customer GetCustomer(int id);
    List<Customer> GetAllCustomers();
    void RemoveCustomer(int id);
    void RestoreCustomer(int id);
}