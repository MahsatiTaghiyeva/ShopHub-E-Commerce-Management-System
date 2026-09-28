namespace ShopHub.Models;

public abstract class Product
{
    private static int _idCounter = 1000;

    public int Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }

    protected Product(
        string name,
        string description,
        decimal price,
        int stock,
        string category)
    {
        Id = ++_idCounter;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Category = category;
        IsDeleted = false;
        CreatedAt = DateTime.Now;
    }

    public abstract string GetProductInfo();

    public virtual decimal CalculateDiscount(decimal percentage)
    {
        if (percentage < 0 || percentage > 100)
        {
            throw new ArgumentException(
                "Discount percentage must be between 0 and 100.");
        }

        return Price - (Price * percentage / 100);
    }
}