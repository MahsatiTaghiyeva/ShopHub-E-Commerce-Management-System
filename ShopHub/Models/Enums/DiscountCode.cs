namespace ShopHub.Models;

public class DiscountCode
{
    public string Code { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public decimal MinimumOrderAmount { get; set; }

    public DiscountCode(
        string code,
        decimal discountPercentage,
        DateTime expirationDate,
        decimal minimumOrderAmount)
    {
        Code = code;
        DiscountPercentage = discountPercentage;
        ExpirationDate = expirationDate;
        MinimumOrderAmount = minimumOrderAmount;
        IsActive = true;
    }
}