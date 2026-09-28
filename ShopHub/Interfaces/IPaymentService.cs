using ShopHub.Models;
using ShopHub.Models.Enums;

namespace ShopHub.Interfaces;

public interface IPaymentService
{
    PaymentStatus Pay(Order order);
    PaymentStatus Refund(Order order);
    PaymentStatus GetPaymentStatus(int orderId);
}