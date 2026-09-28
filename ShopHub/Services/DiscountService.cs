using ShopHub.Models;

namespace ShopHub.Services;

public class DiscountService
{
    private readonly List<DiscountCode> _discountCodes = new();

    public void AddDiscountCode(
        DiscountCode discountCode)
    {
        _discountCodes.Add(discountCode);
    }

    public decimal ApplyDiscount(
        string code,
        decimal orderAmount)
    {
        DiscountCode? discountCode = _discountCodes
            .FirstOrDefault(x =>
                x.Code.Equals(
                    code,
                    StringComparison.OrdinalIgnoreCase));

        if (discountCode == null)
        {
            throw new ArgumentException(
                "Discount code does not exist.");
        }

        if (!discountCode.IsActive)
        {
            throw new ArgumentException(
                "Discount code is inactive.");
        }

        if (DateTime.Now > discountCode.ExpirationDate)
        {
            throw new ArgumentException(
                "Discount code has expired.");
        }

        if (orderAmount <
            discountCode.MinimumOrderAmount)
        {
            throw new ArgumentException(
                "Minimum order amount was not reached.");
        }

        return orderAmount -
               orderAmount *
               discountCode.DiscountPercentage / 100;
    }
}