using ShopHub.Models.Enums;

namespace ShopHub.Models;

public class Order
{
    private static int _idCounter = 1000;

    public int Id { get; private set; }
    public Customer Customer { get; set; }
    public List<OrderItem> Items { get; set; }

    public decimal TotalPrice
    {
        get
        {
            return Items.Sum(x => x.TotalPrice);
        }
    }

    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public Order(Customer customer)
    {
        Id = ++_idCounter;
        Customer = customer;
        Items = new List<OrderItem>();
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.Now;
        IsDeleted = false;
    }
}