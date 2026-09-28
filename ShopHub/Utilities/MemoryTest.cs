using ShopHub.Models;

namespace ShopHub.Utilities;

public class MemoryTest
{
    public void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== GARBAGE COLLECTION TEST ==========");

        long before =
            GC.GetTotalMemory(false);

        long allocatedBefore =
            GC.GetAllocatedBytesForCurrentThread();

        List<OrderItem> items = new();

        for (int i = 0; i < 100000; i++)
        {
            Product product =
                new ElectronicProduct(
                    "Test Product",
                    "Memory Test",
                    100,
                    10,
                    "Electronics",
                    "TestBrand",
                    12);

            items.Add(
                new OrderItem(product, 1));
        }

        long after =
            GC.GetTotalMemory(false);

        long allocatedAfter =
            GC.GetAllocatedBytesForCurrentThread();

        Console.WriteLine(
            $"Memory before: {before} bytes");

        Console.WriteLine(
            $"Memory after: {after} bytes");

        Console.WriteLine(
            $"Allocated bytes: {allocatedAfter - allocatedBefore}");

        Console.WriteLine(
            $"Generation of first object: " +
            $"{GC.GetGeneration(items[0])}");

        items = null!;

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long afterCollection =
            GC.GetTotalMemory(true);

        Console.WriteLine(
            $"Memory after GC: {afterCollection} bytes");
    }
}