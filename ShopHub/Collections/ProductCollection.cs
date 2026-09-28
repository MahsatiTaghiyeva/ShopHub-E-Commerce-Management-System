using ShopHub.Models;

namespace ShopHub.Collections;

public class ProductCollection
{
    private readonly Product[] _products;

    public ProductCollection(Product[] products)
    {
        _products = products;
    }

    public Product this[int index]
    {
        get
        {
            if (index < 0 ||
                index >= _products.Length)
            {
                throw new IndexOutOfRangeException(
                    "Invalid product index.");
            }

            return _products[index];
        }
    }

    public Product this[string name]
    {
        get
        {
            Product? product = _products
                .FirstOrDefault(x =>
                    x.Name.Equals(
                        name,
                        StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                throw new KeyNotFoundException(
                    $"Product with name '{name}' was not found.");
            }

            return product;
        }
    }
}