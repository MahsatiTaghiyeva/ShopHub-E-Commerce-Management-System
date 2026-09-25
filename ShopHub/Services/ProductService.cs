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
        Product product = _products
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
            .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

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
        keyword = keyword.Trim().ToLower();

        return _products
            .Where(x => !x.IsDeleted)
            .Where(x =>
                x.Name.ToLower().Contains(keyword) ||
                x.Description.ToLower().Contains(keyword) ||
                x.Category.ToLower().Contains(keyword) ||
                (
                    x is ElectronicProduct electronic &&
                    electronic.Brand.ToLower().Contains(keyword)
                ))
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
        if (percentage < 0 || percentage > 100)
        {
            throw new ArgumentException(
                "Discount percentage must be between 0 and 100.");
        }

        price -= price * percentage / 100;
    }
}