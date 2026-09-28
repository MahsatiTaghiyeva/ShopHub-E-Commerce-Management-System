using ShopHub.Exceptions;
using ShopHub.Extensions;
using ShopHub.Interfaces;
using ShopHub.Models;
using ShopHub.Models.Enums;
using ShopHub.Reflection;
using ShopHub.Services;
using ShopHub.Utilities;

namespace ShopHub;

public class Program
{
    private static readonly IProductService ProductService =
        new ProductService();

    private static readonly ICustomerService CustomerService =
        new CustomerService();

    private static readonly IOrderService OrderService =
        new OrderService(
            CustomerService,
            ProductService);

    private static readonly IPaymentService PaymentService =
        new CardPayment();

    private static readonly DiscountService DiscountService =
        new DiscountService();

    private static readonly RatingService RatingService =
        new RatingService();

    private static readonly ObjectInspector ObjectInspector =
        new ObjectInspector();

    private static readonly MemoryTest MemoryTest =
        new MemoryTest();

    public static void Main()
    {
        while (true)
        {
            ShowMenu();

            Console.Write("Choose an option: ");
            string? input = Console.ReadLine();

            Console.WriteLine();

            try
            {
                switch (input)
                {
                    case "1":
                        AddCustomer();
                        break;

                    case "2":
                        AddProduct();
                        break;

                    case "3":
                        ShowProducts();
                        break;

                    case "4":
                        SearchProduct();
                        break;

                    case "5":
                        FilterProducts();
                        break;

                    case "6":
                        CreateOrder();
                        break;

                    case "7":
                        AddProductToOrder();
                        break;

                    case "8":
                        RemoveProductFromOrder();
                        break;

                    case "9":
                        ShowOrder();
                        break;

                    case "10":
                        ConfirmOrder();
                        break;

                    case "11":
                        CancelOrder();
                        break;

                    case "12":
                        ShowCustomerOrders();
                        break;

                    case "13":
                        DeleteProduct();
                        break;

                    case "14":
                        RestoreProduct();
                        break;

                    case "15":
                        ShowDeletedProducts();
                        break;

                    case "16":
                        ProductStatistics();
                        break;

                    case "17":
                        ObjectInspectorMenu();
                        break;

                    case "18":
                        MemoryTest.Run();
                        break;

                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine(
                            "Invalid option. Please choose again.");
                        break;
                }
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (OrderNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (OutOfStockException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidOrderException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Unexpected error: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }

    private static void ShowMenu()
    {
        Console.Clear();

        Console.WriteLine("==========================================");
        Console.WriteLine("          SHOPHUB E-COMMERCE");
        Console.WriteLine("==========================================");
        Console.WriteLine("1.  Add Customer");
        Console.WriteLine("2.  Add Product");
        Console.WriteLine("3.  Show Products");
        Console.WriteLine("4.  Search Product");
        Console.WriteLine("5.  Filter Products");
        Console.WriteLine("6.  Create Order");
        Console.WriteLine("7.  Add Product To Order");
        Console.WriteLine("8.  Remove Product From Order");
        Console.WriteLine("9.  Show Order");
        Console.WriteLine("10. Confirm Order");
        Console.WriteLine("11. Cancel Order");
        Console.WriteLine("12. Show Customer Orders");
        Console.WriteLine("13. Delete Product");
        Console.WriteLine("14. Restore Product");
        Console.WriteLine("15. Show Deleted Products");
        Console.WriteLine("16. Product Statistics");
        Console.WriteLine("17. Object Inspector");
        Console.WriteLine("18. Garbage Collection Test");
        Console.WriteLine("0.  Exit");
        Console.WriteLine("==========================================");
    }

    private static void AddCustomer()
    {
        Console.WriteLine("========== ADD CUSTOMER ==========");

        Console.Write("First name: ");
        string firstName = Console.ReadLine() ?? "";

        Console.Write("Last name: ");
        string lastName = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        if (!email.IsValidEmail())
        {
            Console.WriteLine("Invalid email.");
            return;
        }

        Console.Write("Phone: ");
        string phone = Console.ReadLine() ?? "";

        Customer customer = new Customer(
            firstName.ToTitleCase(),
            lastName.ToTitleCase(),
            email,
            phone);

        CustomerService.AddCustomer(customer);

        Console.WriteLine();
        Console.WriteLine("Customer added successfully.");
        Console.WriteLine($"ID: {customer.Id}");
        Console.WriteLine($"Name: {customer.FullName}");
    }

    private static void AddProduct()
    {
        Console.WriteLine("========== ADD PRODUCT ==========");

        Console.WriteLine("1. Electronic");
        Console.WriteLine("2. Clothing");

        Console.Write("Choose product type: ");
        string? type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Description: ");
        string description = Console.ReadLine() ?? "";

        decimal price = ReadDecimal("Price: ");
        int stock = ReadInt("Stock: ");

        Console.Write("Category: ");
        string category = Console.ReadLine() ?? "";

        Product product;

        if (type == "1")
        {
            Console.Write("Brand: ");
            string brand = Console.ReadLine() ?? "";

            int warranty = ReadInt(
                "Warranty months: ");

            product = new ElectronicProduct(
                name,
                description,
                price,
                stock,
                category,
                brand,
                warranty);
        }
        else if (type == "2")
        {
            Console.Write("Size: ");
            string size = Console.ReadLine() ?? "";

            Console.Write("Material: ");
            string material = Console.ReadLine() ?? "";

            Console.Write("Gender: ");
            string gender = Console.ReadLine() ?? "";

            product = new ClothingProduct(
                name,
                description,
                price,
                stock,
                category,
                size,
                material,
                gender);
        }
        else
        {
            Console.WriteLine(
                "Invalid product type.");

            return;
        }

        ProductService.AddProduct(product);

        Console.WriteLine();
        Console.WriteLine("Product added successfully.");
        Console.WriteLine($"ID: {product.Id}");
    }

    private static void ShowProducts()
    {
        Console.WriteLine("========== PRODUCTS ==========");

        List<Product> products =
            ProductService.GetAllProducts();

        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        foreach (Product product in products)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"ID: {product.Id}");
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Price: {product.Price} AZN");
            Console.WriteLine($"Stock: {product.Stock}");
            Console.WriteLine($"Category: {product.Category}");
            Console.WriteLine(product.GetProductInfo());
        }
    }

    private static void SearchProduct()
    {
        Console.WriteLine("========== SEARCH PRODUCT ==========");

        Console.Write("Keyword: ");
        string keyword = Console.ReadLine() ?? "";

        List<Product> products =
            ProductService.SearchProducts(keyword);

        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
            return;
        }

        foreach (Product product in products)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"ID: {product.Id}");
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Price: {product.Price} AZN");
            Console.WriteLine($"Stock: {product.Stock}");
            Console.WriteLine($"Category: {product.Category}");
        }
    }

    private static void FilterProducts()
    {
        Console.WriteLine("========== FILTER PRODUCTS ==========");

        Console.WriteLine("1. By Category");
        Console.WriteLine("2. By Price Range");
        Console.WriteLine("3. Available Products");
        Console.WriteLine("4. Out Of Stock Products");

        Console.Write("Choose: ");
        string? choice = Console.ReadLine();

        List<Product> products;

        switch (choice)
        {
            case "1":
                Console.Write("Category: ");
                string category =
                    Console.ReadLine() ?? "";

                products =
                    ProductService.GetProductsByCategory(
                        category);
                break;

            case "2":
                decimal min =
                    ReadDecimal("Minimum price: ");

                decimal max =
                    ReadDecimal("Maximum price: ");

                products =
                    ProductService.GetProductsByPrice(
                        min,
                        max);
                break;

            case "3":
                products =
                    ProductService.GetAvailableProducts();
                break;

            case "4":
                products =
                    ProductService.GetOutOfStockProducts();
                break;

            default:
                Console.WriteLine("Invalid option.");
                return;
        }

        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
            return;
        }

        foreach (Product product in products)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine(
                $"{product.Id} | " +
                $"{product.Name} | " +
                $"{product.Price} AZN | " +
                $"Stock: {product.Stock}");
        }
    }

    private static void CreateOrder()
    {
        Console.WriteLine("========== CREATE ORDER ==========");

        int customerId =
            ReadInt("Customer ID: ");

        Order order =
            OrderService.CreateOrder(customerId);

        Console.WriteLine(
            $"Order created successfully.");

        Console.WriteLine(
            $"Order ID: {order.Id}");

        Console.WriteLine(
            $"Customer: {order.Customer.FullName}");
    }

    private static void AddProductToOrder()
    {
        Console.WriteLine(
            "========== ADD PRODUCT TO ORDER ==========");

        int orderId =
            ReadInt("Order ID: ");

        int productId =
            ReadInt("Product ID: ");

        int quantity =
            ReadInt("Quantity: ");

        OrderService.AddProductToOrder(
            orderId,
            productId,
            quantity);

        Console.WriteLine(
            "Product added to order successfully.");
    }

    private static void RemoveProductFromOrder()
    {
        Console.WriteLine(
            "========== REMOVE PRODUCT FROM ORDER ==========");

        int orderId =
            ReadInt("Order ID: ");

        int productId =
            ReadInt("Product ID: ");

        OrderService.RemoveProductFromOrder(
            orderId,
            productId);

        Console.WriteLine(
            "Product removed from order.");
    }

    private static void ShowOrder()
    {
        Console.WriteLine("========== SHOW ORDER ==========");

        int orderId =
            ReadInt("Order ID: ");

        Order order =
            OrderService.GetOrder(orderId);

        Helpers.ShowOrder(order);
    }

    private static void ConfirmOrder()
    {
        Console.WriteLine("========== CONFIRM ORDER ==========");

        int orderId =
            ReadInt("Order ID: ");

        OrderService.ConfirmOrder(orderId);

        Console.WriteLine(
            "Order confirmed successfully.");

        Order order =
            OrderService.GetOrder(orderId);

        Console.WriteLine(
            $"Total: {order.TotalPrice} AZN");

        Console.WriteLine();
        Console.WriteLine("Payment:");

        Console.WriteLine(
            $"Payment status: " +
            $"{PaymentService.Pay(order)}");
    }

    private static void CancelOrder()
    {
        Console.WriteLine("========== CANCEL ORDER ==========");

        int orderId =
            ReadInt("Order ID: ");

        OrderService.CancelOrder(orderId);

        Console.WriteLine(
            "Order cancelled successfully.");
    }

    private static void ShowCustomerOrders()
    {
        Console.WriteLine(
            "========== CUSTOMER ORDERS ==========");

        int customerId =
            ReadInt("Customer ID: ");

        List<Order> orders =
            OrderService.GetCustomerOrders(customerId);

        if (orders.Count == 0)
        {
            Console.WriteLine(
                "Customer has no orders.");

            return;
        }

        foreach (Order order in orders)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine(
                $"Order ID: {order.Id}");

            Console.WriteLine(
                $"Status: {order.Status}");

            Console.WriteLine(
                $"Total: {order.TotalPrice} AZN");

            Console.WriteLine(
                $"Items: {order.Items.Count}");
        }
    }

    private static void DeleteProduct()
    {
        Console.WriteLine("========== DELETE PRODUCT ==========");

        int productId =
            ReadInt("Product ID: ");

        ProductService.RemoveProduct(productId);

        Console.WriteLine(
            "Product deleted successfully.");
    }

    private static void RestoreProduct()
    {
        Console.WriteLine("========== RESTORE PRODUCT ==========");

        int productId =
            ReadInt("Product ID: ");

        ProductService.RestoreProduct(productId);

        Console.WriteLine(
            "Product restored successfully.");
    }

    private static void ShowDeletedProducts()
    {
        Console.WriteLine(
            "========== DELETED PRODUCTS ==========");

        List<Product> products =
            ProductService.GetDeletedProducts();

        if (products.Count == 0)
        {
            Console.WriteLine(
                "There are no deleted products.");

            return;
        }

        foreach (Product product in products)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine(
                $"ID: {product.Id}");

            Console.WriteLine(
                $"Name: {product.Name}");

            Console.WriteLine(
                $"Price: {product.Price} AZN");

            Console.WriteLine(
                $"Category: {product.Category}");

            Console.WriteLine(
                $"Deleted: {product.IsDeleted}");
        }
    }

    private static void ProductStatistics()
    {
        Console.WriteLine(
            "========== PRODUCT STATISTICS ==========");

        int count =
            ProductService.GetProductCount();

        decimal average =
            ProductService.GetAveragePrice();

        bool hasOutOfStock =
            ProductService.HasOutOfStockProducts();

        bool validStock =
            ProductService.AreAllProductsInValidStock();

        Product? expensive =
            ProductService.GetMostExpensiveProduct();

        Product? cheapest =
            ProductService.GetCheapestProduct();

        Product? bestSelling =
            OrderService.GetBestSellingProduct();

        Console.WriteLine(
            $"Total products: {count}");

        Console.WriteLine(
            $"Average price: {average:F2} AZN");

        Console.WriteLine(
            $"Has out of stock products: " +
            $"{hasOutOfStock}");

        Console.WriteLine(
            $"All stock values valid: " +
            $"{validStock}");

        Console.WriteLine();

        if (expensive != null)
        {
            Console.WriteLine(
                $"Most expensive: " +
                $"{expensive.Name} - " +
                $"{expensive.Price} AZN");
        }

        if (cheapest != null)
        {
            Console.WriteLine(
                $"Cheapest: " +
                $"{cheapest.Name} - " +
                $"{cheapest.Price} AZN");
        }

        if (bestSelling != null)
        {
            Console.WriteLine(
                $"Best selling: " +
                $"{bestSelling.Name}");
        }

        Console.WriteLine();
        Console.WriteLine("Products by category:");

        Dictionary<string, int> categories =
            ProductService.GetProductCountByCategory();

        foreach (var category in categories)
        {
            Console.WriteLine(
                $"{category.Key}: {category.Value}");
        }
    }

    private static void ObjectInspectorMenu()
    {
        Console.WriteLine(
            "========== OBJECT INSPECTOR ==========");

        int productId =
            ReadInt("Product ID: ");

        Product product =
            ProductService.GetProduct(productId);

        ObjectInspector.Inspect(product);
    }

    private static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value))
            {
                return value;
            }

            Console.WriteLine(
                "Please enter a valid number.");
        }
    }

    private static decimal ReadDecimal(
        string message)
    {
        while (true)
        {
            Console.Write(message);

            string? input = Console.ReadLine();

            if (decimal.TryParse(
                input,
                out decimal value))
            {
                return value;
            }

            Console.WriteLine(
                "Please enter a valid decimal number.");
        }
    }
}