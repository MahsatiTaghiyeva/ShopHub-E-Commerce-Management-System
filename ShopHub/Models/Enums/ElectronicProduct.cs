namespace ShopHub.Models;

public class ElectronicProduct : Product
{
    public string Brand { get; set; }
    public int WarrantyMonths { get; set; }

    public ElectronicProduct(
        string name,
        string description,
        decimal price,
        int stock,
        string category,
        string brand,
        int warrantyMonths)
        : base(
            name,
            description,
            price,
            stock,
            category)
    {
        Brand = brand;
        WarrantyMonths = warrantyMonths;
    }

    public override string GetProductInfo()
    {
        return $"{Name} | {Brand} | {Price} AZN | " +
               $"Stock: {Stock} | Warranty: {WarrantyMonths} months";
    }
}