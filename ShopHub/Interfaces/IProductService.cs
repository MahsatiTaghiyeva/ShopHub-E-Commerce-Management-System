using ShopHub.Models;

namespace ShopHub.Interfaces;

public interface IProductService
{
    void AddProduct(Product product);
    void RemoveProduct(int id);
    void RestoreProduct(int id);
    Product GetProduct(int id);
    List<Product> GetAllProducts();
    List<Product> SearchProducts(string keyword);

    bool TryRemoveFromStock(
        Product product,
        int quantity,
        out decimal totalPrice);

    void ApplyDiscount(
        ref decimal price,
        decimal percentage);

    Product? GetMostExpensiveProduct();
    Product? GetCheapestProduct();
    List<Product> GetAvailableProducts();
    List<Product> GetOutOfStockProducts();
    List<Product> GetProductsByPrice(
        decimal min,
        decimal max);

    List<Product> GetProductsByCategory(
        string category);

    Product? GetProductByName(string name);

    int GetProductCount();
    decimal GetAveragePrice();
    bool HasOutOfStockProducts();
    bool AreAllProductsInValidStock();

    Dictionary<string, int> GetProductCountByCategory();
}