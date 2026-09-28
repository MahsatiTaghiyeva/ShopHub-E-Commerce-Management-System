namespace ShopHub.Models;

public class OrderItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal TotalPrice
    {
        get
        {
            return Quantity * UnitPrice;
        }
    }

    public OrderItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
        UnitPrice = product.Price;
    }
}