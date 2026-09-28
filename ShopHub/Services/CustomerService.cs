using ShopHub.Exceptions;
using ShopHub.Interfaces;
using ShopHub.Models;

namespace ShopHub.Services;

public class CustomerService : ICustomerService
{
    private readonly List<Customer> _customers = new();

    public void AddCustomer(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        _customers.Add(customer);
    }

    public Customer GetCustomer(int id)
    {
        Customer? customer = _customers
            .FirstOrDefault(x =>
                x.Id == id &&
                !x.IsDeleted);

        if (customer == null)
        {
            throw new CustomerNotFoundException(
                $"Customer with ID {id} was not found.");
        }

        return customer;
    }

    public List<Customer> GetAllCustomers()
    {
        return _customers
            .Where(x => !x.IsDeleted)
            .ToList();
    }

    public void RemoveCustomer(int id)
    {
        Customer customer = GetCustomer(id);

        customer.IsDeleted = true;
    }

    public void RestoreCustomer(int id)
    {
        Customer? customer = _customers
            .FirstOrDefault(x => x.Id == id);

        if (customer == null)
        {
            throw new CustomerNotFoundException(
                $"Customer with ID {id} was not found.");
        }

        customer.IsDeleted = false;
    }
}