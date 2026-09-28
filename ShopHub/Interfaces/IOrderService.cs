using ShopHub.Models;

namespace ShopHub.Interfaces;

public interface IOrderService
{
    Order CreateOrder(int customerId);

    void AddProductToOrder(
        int orderId,
        int productId,
        int quantity);

    void RemoveProductFromOrder(
        int orderId,
        int productId);

    void ConfirmOrder(int orderId);
    void CancelOrder(int orderId);

    Order GetOrder(int orderId);

    List<Order> GetCustomerOrders(int customerId);
    List<Order> GetAllOrders();

    Product? GetBestSellingProduct();
}