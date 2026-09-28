using ShopHub.Exceptions;
using ShopHub.Models;

namespace ShopHub.Services;

public class RatingService
{
    private readonly List<ProductRating> _ratings = new();

    public void AddRating(
        Product product,
        Customer customer,
        int score,
        string comment,
        List<Order> orders)
    {
        bool purchased = orders
            .Where(x =>
                x.Customer.Id == customer.Id &&
                x.Status != Models.Enums.OrderStatus.Cancelled)
            .SelectMany(x => x.Items)
            .Any(x => x.Product.Id == product.Id);

        if (!purchased)
        {
            throw new InvalidOrderException(
                "Customer can only rate a product they purchased.");
        }

        ProductRating rating =
            new ProductRating(
                product,
                customer,
                score,
                comment);

        _ratings.Add(rating);
    }

    public double GetAverageRating(
        int productId)
    {
        return _ratings
            .Where(x =>
                x.Product.Id == productId)
            .Select(x => x.Score)
            .DefaultIfEmpty()
            .Average();
    }

    public int GetHighestRating(
        int productId)
    {
        return _ratings
            .Where(x =>
                x.Product.Id == productId)
            .Select(x => x.Score)
            .DefaultIfEmpty()
            .Max();
    }

    public int GetLowestRating(
        int productId)
    {
        return _ratings
            .Where(x =>
                x.Product.Id == productId)
            .Select(x => x.Score)
            .DefaultIfEmpty()
            .Min();
    }

    public List<ProductRating> GetProductRatings(
        int productId)
    {
        return _ratings
            .Where(x =>
                x.Product.Id == productId)
            .ToList();
    }
}