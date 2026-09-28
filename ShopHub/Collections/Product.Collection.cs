using ShopHub.Models;
namespace ShopHub.Collections;
public class ProductCollection
{
    private readonly Product[] _products;
    public ProductCollection(Product[] products)
    {
        products = products;
    }
    public Product this[int index]
    {
        get
        {
            if(index< 0 || index >= _products.Length)
            {
                throw new IndexOutOfRangeException("No pruduct with such index.");
            }
            return _products[index];
        }

    }
}