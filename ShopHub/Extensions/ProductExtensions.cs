using ShopHub.Models;

namespace ShopHub.Extensions;

public static class ProductExtensions
{
    public static bool IsInStock(
        this Product product)
    {
        return product.Stock > 0 &&
               !product.IsDeleted;
    }

    public static decimal GetFinalPrice(
        this Product product,
        decimal discountPercentage)
    {
        if (discountPercentage < 0 ||
            discountPercentage > 100)
        {
            throw new ArgumentException(
                "Discount percentage must be between 0 and 100.");
        }

        return product.Price -
               product.Price *
               discountPercentage / 100;
    }

    public static bool IsExpensive(
        this Product product,
        decimal limit = 1000)
    {
        return product.Price > limit;
    }
}