namespace ShopHub.Models;

public class ClothingProduct : Product
{
    public string Size { get; set; }
    public string Material { get; set; }
    public string Gender { get; set; }

    public ClothingProduct(
        string name,
        string description,
        decimal price,
        int stock,
        string category,
        string size,
        string material,
        string gender)
        : base(
            name,
            description,
            price,
            stock,
            category)
    {
        Size = size;
        Material = material;
        Gender = gender;
    }

    public override string GetProductInfo()
    {
        return $"{Name} | {Size} | {Material} | " +
               $"{Gender} | {Price} AZN | Stock: {Stock}";
    }
}