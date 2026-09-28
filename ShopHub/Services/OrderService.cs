using ShopHub.Exceptions;
using ShopHub.Interfaces;
using ShopHub.Models;
using ShopHub.Models.Enums;

namespace ShopHub.Services;

public class OrderService : IOrderService
{
    private readonly List<Order> _orders = new();

    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;

    public OrderService(
        ICustomerService customerService,
        IProductService productService)
    {
        _customerService = customerService;
        _productService = productService;
    }

    public Order CreateOrder(int customerId)
    {
        Customer customer =
            _customerService.GetCustomer(customerId);

        Order order = new Order(customer);

        _orders.Add(order);

        return order;
    }

    public void AddProductToOrder(
        int orderId,
        int productId,
        int quantity)
    {
        Order order = GetOrder(orderId);

        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidOrderException(
                "Products can only be added to pending orders.");
        }

        Product product =
            _productService.GetProduct(productId);

        if (quantity <= 0)
        {
            throw new InvalidOrderException(
                "Quantity must be greater than zero.");
        }

        if (product.Stock < quantity)
        {
            throw new OutOfStockException(
                $"Not enough stock for {product.Name}.");
        }

        OrderItem? existingItem = order.Items
            .FirstOrDefault(x =>
                x.Product.Id == product.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            OrderItem item =
                new OrderItem(product, quantity);

            order.Items.Add(item);
        }
    }

    public void RemoveProductFromOrder(
        int orderId,
        int productId)
    {
        Order order = GetOrder(orderId);

        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidOrderException(
                "Products can only be removed from pending orders.");
        }

        OrderItem? item = order.Items
            .FirstOrDefault(x =>
                x.Product.Id == productId);

        if (item == null)
        {
            throw new ProductNotFoundException(
                $"Product with ID {productId} is not in the order.");
        }

        order.Items.Remove(item);
    }

    public void ConfirmOrder(int orderId)
    {
        Order order = GetOrder(orderId);

        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidOrderException(
                "Only pending orders can be confirmed.");
        }

        if (order.Customer.IsDeleted)
        {
            throw new InvalidOrderException(
                "Customer is deleted.");
        }

        if (order.Items.Count == 0)
        {
            throw new InvalidOrderException(
                "Order cannot be empty.");
        }

        foreach (OrderItem item in order.Items)
        {
            if (item.Product.IsDeleted)
            {
                throw new InvalidOrderException(
                    $"Product {item.Product.Name} is deleted.");
            }

            if (item.Product.Stock < item.Quantity)
            {
                throw new OutOfStockException(
                    $"Not enough stock for {item.Product.Name}.");
            }
        }

        foreach (OrderItem item in order.Items)
        {
            item.Product.Stock -= item.Quantity;
        }

        order.Status = OrderStatus.Confirmed;
    }

    public void CancelOrder(int orderId)
    {
        Order order = GetOrder(orderId);

        if (order.Status == OrderStatus.Delivered)
        {
            throw new InvalidOrderException(
                "Delivered orders cannot be cancelled.");
        }

        if (order.Status == OrderStatus.Cancelled)
        {
            throw new InvalidOrderException(
                "Order is already cancelled.");
        }

        order.Status = OrderStatus.Cancelled;
    }

    public Order GetOrder(int orderId)
    {
        Order? order = _orders
            .FirstOrDefault(x =>
                x.Id == orderId &&
                !x.IsDeleted);

        if (order == null)
        {
            throw new OrderNotFoundException(
                $"Order with ID {orderId} was not found.");
        }

        return order;
    }

    public List<Order> GetCustomerOrders(
        int customerId)
    {
        Customer customer =
            _customerService.GetCustomer(customerId);

        return _orders
            .Where(x =>
                x.Customer.Id == customer.Id &&
                !x.IsDeleted)
            .ToList();
    }

    public List<Order> GetAllOrders()
    {
        return _orders
            .Where(x => !x.IsDeleted)
            .ToList();
    }

    public Product? GetBestSellingProduct()
    {
        return _orders
            .Where(x =>
                !x.IsDeleted &&
                x.Status != OrderStatus.Cancelled)
            .SelectMany(x => x.Items)
            .Where(x => !x.Product.IsDeleted)
            .GroupBy(x => x.Product)
            .Select(x => new
            {
                Product = x.Key,
                Quantity = x.Sum(item => item.Quantity)
            })
            .OrderByDescending(x => x.Quantity)
            .Select(x => x.Product)
            .FirstOrDefault();
    }
}