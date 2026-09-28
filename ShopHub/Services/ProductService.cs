using ShopHub.Exceptions;
using ShopHub.Interfaces;
using ShopHub.Models;

namespace ShopHub.Services;

public class ProductService : IProductService
{
    private readonly List<Product> _products = new();

    public void AddProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        _products.Add(product);
    }

    public void RemoveProduct(int id)
    {
        Product product = GetProduct(id);

        product.IsDeleted = true;
    }

    public void RestoreProduct(int id)
    {
        Product? product = _products
            .FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            throw new ProductNotFoundException(
                $"Product with ID {id} was not found.");
        }

        product.IsDeleted = false;
    }

    public Product GetProduct(int id)
    {
        Product? product = _products
            .FirstOrDefault(x =>
                x.Id == id &&
                !x.IsDeleted);

        if (product == null)
        {
            throw new ProductNotFoundException(
                $"Product with ID {id} was not found.");
        }

        return product;
    }

    public List<Product> GetAllProducts()
    {
        return _products
            .Where(x => !x.IsDeleted)
            .ToList();
    }

    public List<Product> SearchProducts(string keyword)
    {
        keyword = keyword
            .Trim()
            .ToLower()
            .Replace("-", " ");

        string[] keywords = keyword
            .Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries);

        return _products
            .Where(x => !x.IsDeleted)
            .Where(x =>
                keywords.Any(word =>
                    x.Name.ToLower().Contains(word) ||
                    x.Name.ToLower().StartsWith(word) ||
                    x.Name.ToLower().EndsWith(word) ||
                    x.Description.ToLower().Contains(word) ||
                    x.Category.ToLower().Contains(word) ||
                    (
                        x is ElectronicProduct electronic &&
                        electronic.Brand
                            .ToLower()
                            .Contains(word)
                    )))
            .ToList();
    }

    public bool TryRemoveFromStock(
        Product product,
        int quantity,
        out decimal totalPrice)
    {
        totalPrice = 0;

        if (product == null)
        {
            return false;
        }

        if (product.IsDeleted)
        {
            return false;
        }

        if (quantity <= 0)
        {
            return false;
        }

        if (product.Stock < quantity)
        {
            return false;
        }

        product.Stock -= quantity;
        totalPrice = product.Price * quantity;

        return true;
    }

    public void ApplyDiscount(
        ref decimal price,
        decimal percentage)
    {
        if (percentage < 0 ||
            percentage > 100)
        {
            throw new ArgumentException(
                "Discount percentage must be between 0 and 100.");
        }

        price -= price * percentage / 100;
    }

    public Product? GetMostExpensiveProduct()
    {
        return _products
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.Price)
            .FirstOrDefault();
    }

    public Product? GetCheapestProduct()
    {
        return _products
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Price)
            .FirstOrDefault();
    }

    public List<Product> GetAvailableProducts()
    {
        return _products
            .Where(x =>
                !x.IsDeleted &&
                x.Stock > 0)
            .ToList();
    }

    public List<Product> GetOutOfStockProducts()
    {
        return _products
            .Where(x =>
                !x.IsDeleted &&
                x.Stock == 0)
            .ToList();
    }

    public List<Product> GetProductsByPrice(
        decimal min,
        decimal max)
    {
        return _products
            .Where(x =>
                !x.IsDeleted &&
                x.Price >= min &&
                x.Price <= max)
            .OrderBy(x => x.Price)
            .ToList();
    }

    public List<Product> GetProductsByCategory(
        string category)
    {
        category = category
            .Trim()
            .ToLower();

        return _products
            .Where(x =>
                !x.IsDeleted &&
                x.Category.ToLower() == category)
            .ToList();
    }

    public Product? GetProductByName(string name)
    {
        return _products
            .Where(x => !x.IsDeleted)
            .Where(x =>
                x.Name.Equals(
                    name,
                    StringComparison.OrdinalIgnoreCase))
            .SingleOrDefault();
    }

    public int GetProductCount()
    {
        return _products
            .Count(x => !x.IsDeleted);
    }

    public decimal GetAveragePrice()
    {
        List<Product> products = _products
            .Where(x => !x.IsDeleted)
            .ToList();

        if (products.Count == 0)
        {
            return 0;
        }

        return products
            .Average(x => x.Price);
    }

    public bool HasOutOfStockProducts()
    {
        return _products
            .Where(x => !x.IsDeleted)
            .Any(x => x.Stock == 0);
    }

    public bool AreAllProductsInValidStock()
    {
        return _products
            .Where(x => !x.IsDeleted)
            .All(x => x.Stock >= 0);
    }

    public Dictionary<string, int> GetProductCountByCategory()
    {
        return _products
            .Where(x => !x.IsDeleted)
            .GroupBy(x => x.Category)
            .ToDictionary(
                x => x.Key,
                x => x.Count());
    }
}