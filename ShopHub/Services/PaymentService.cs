using ShopHub.Interfaces;
using ShopHub.Models;
using ShopHub.Models.Enums;

namespace ShopHub.Services;

public class CashPayment : IPaymentService
{
    private readonly Dictionary<int, PaymentStatus> _payments = new();

    public PaymentStatus Pay(Order order)
    {
        _payments[order.Id] = PaymentStatus.Paid;
        return PaymentStatus.Paid;
    }

    public PaymentStatus Refund(Order order)
    {
        _payments[order.Id] = PaymentStatus.Refunded;
        return PaymentStatus.Refunded;
    }

    public PaymentStatus GetPaymentStatus(int orderId)
    {
        if (_payments.TryGetValue(
            orderId,
            out PaymentStatus status))
        {
            return status;
        }

        return PaymentStatus.Pending;
    }
}

public class CardPayment : IPaymentService
{
    private readonly Dictionary<int, PaymentStatus> _payments = new();

    public PaymentStatus Pay(Order order)
    {
        _payments[order.Id] = PaymentStatus.Paid;
        return PaymentStatus.Paid;
    }

    public PaymentStatus Refund(Order order)
    {
        _payments[order.Id] = PaymentStatus.Refunded;
        return PaymentStatus.Refunded;
    }

    public PaymentStatus GetPaymentStatus(int orderId)
    {
        if (_payments.TryGetValue(
            orderId,
            out PaymentStatus status))
        {
            return status;
        }

        return PaymentStatus.Pending;
    }
}

public class OnlinePayment : IPaymentService
{
    private readonly Dictionary<int, PaymentStatus> _payments = new();

    public PaymentStatus Pay(Order order)
    {
        _payments[order.Id] = PaymentStatus.Paid;
        return PaymentStatus.Paid;
    }

    public PaymentStatus Refund(Order order)
    {
        _payments[order.Id] = PaymentStatus.Refunded;
        return PaymentStatus.Refunded;
    }

    public PaymentStatus GetPaymentStatus(int orderId)
    {
        if (_payments.TryGetValue(
            orderId,
            out PaymentStatus status))
        {
            return status;
        }

        return PaymentStatus.Pending;
    }
}