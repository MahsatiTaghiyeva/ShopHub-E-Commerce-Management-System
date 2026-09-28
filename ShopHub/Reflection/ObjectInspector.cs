using System.Reflection;

namespace ShopHub.Reflection;

public class ObjectInspector
{
    public void Inspect(object obj)
    {
        Type type = obj.GetType();

        Console.WriteLine();
        Console.WriteLine("========== OBJECT INSPECTOR ==========");
        Console.WriteLine($"Class: {type.Name}");

        Console.WriteLine();
        Console.WriteLine("Properties:");

        PropertyInfo[] properties =
            type.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            Console.WriteLine(
                $"{property.Name} : {property.PropertyType.Name}");
        }

        Console.WriteLine();
        Console.WriteLine("Methods:");

        MethodInfo[] methods =
            type.GetMethods(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly);

        foreach (MethodInfo method in methods)
        {
            Console.WriteLine(
                $"{method.Name} : {method.ReturnType.Name}");
        }

        Console.WriteLine();
    }

    public object? GetProperty(
        object obj,
        string propertyName)
    {
        PropertyInfo? property =
            obj.GetType().GetProperty(propertyName);

        if (property == null)
        {
            return null;
        }

        return property.GetValue(obj);
    }
}