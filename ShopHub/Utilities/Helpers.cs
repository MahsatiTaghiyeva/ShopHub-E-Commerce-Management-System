using ShopHub.Models;

namespace ShopHub.Utilities;

public static class Helpers
{
    public static void ShowProduct(Product product)
    {
        Console.WriteLine(
            $"ID: {product.Id}");

        Console.WriteLine(
            $"Name: {product.Name}");

        Console.WriteLine(
            $"Description: {product.Description}");

        Console.WriteLine(
            $"Price: {product.Price} AZN");

        Console.WriteLine(
            $"Stock: {product.Stock}");

        Console.WriteLine(
            $"Category: {product.Category}");

        Console.WriteLine(
            $"Created: {product.CreatedAt}");

        Console.WriteLine(
            $"Info: {product.GetProductInfo()}");
    }

    public static void ShowOrder(Order order)
    {
        Console.WriteLine();
        Console.WriteLine(
            $"Order ID: {order.Id}");

        Console.WriteLine(
            $"Customer: {order.Customer.FullName}");

        Console.WriteLine(
            $"Status: {order.Status}");

        Console.WriteLine(
            $"Created: {order.CreatedAt}");

        Console.WriteLine();
        Console.WriteLine("Items:");

        foreach (OrderItem item in order.Items)
        {
            Console.WriteLine(
                $"{item.Product.Name} | " +
                $"Quantity: {item.Quantity} | " +
                $"Unit Price: {item.UnitPrice} | " +
                $"Total: {item.TotalPrice}");
        }

        Console.WriteLine();
        Console.WriteLine(
            $"Total: {order.TotalPrice} AZN");
    }
}