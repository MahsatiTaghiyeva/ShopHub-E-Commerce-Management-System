using ShopHub.Models;
using ShopHub.Services;

ProductService productService = new ProductService();

Product iphone = new ElectronicProduct(
    "iPhone 15",
    "Apple smartphone",
    2200,
    10,
    "Smartphone",
    "Apple",
    24);

Product headphones = new ElectronicProduct(
    "Headphones",
    "Wireless headphones",
    250,
    5,
    "Audio",
    "Sony",
    12);

Product tshirt = new ClothingProduct(
    "T-Shirt",
    "Cotton T-Shirt",
    40,
    20,
    "Clothing",
    "L",
    "Cotton",
    "Unisex");

productService.AddProduct(iphone);
productService.AddProduct(headphones);
productService.AddProduct(tshirt);

Console.WriteLine("ALL PRODUCTS");

foreach (Product product in productService.GetAllProducts())
{
    Console.WriteLine(product.GetProductInfo());
}

Console.WriteLine();

Console.WriteLine("SEARCH:");

List<Product> results =
    productService.SearchProducts("sony");

foreach (Product product in results)
{
    Console.WriteLine(product.GetProductInfo());
}

Console.WriteLine();

Console.WriteLine("STOCK TEST:");

decimal totalPrice;

bool success = productService.TryRemoveFromStock(
    headphones,
    2,
    out totalPrice);

Console.WriteLine($"Success: {success}");
Console.WriteLine($"Total price: {totalPrice} AZN");
Console.WriteLine($"Remaining stock: {headphones.Stock}");

Console.WriteLine();

Console.WriteLine("DISCOUNT TEST:");

decimal price = 1000;

productService.ApplyDiscount(ref price, 15);

Console.WriteLine($"Final price: {price} AZN");

Console.WriteLine();

Console.WriteLine("SOFT DELETE:");

productService.RemoveProduct(iphone.Id);

foreach (Product product in productService.GetAllProducts())
{
    Console.WriteLine(product.GetProductInfo());
}

Console.WriteLine();

Console.WriteLine("RESTORE:");

productService.RestoreProduct(iphone.Id);

foreach (Product product in productService.GetAllProducts())
{
    Console.WriteLine(product.GetProductInfo());
}