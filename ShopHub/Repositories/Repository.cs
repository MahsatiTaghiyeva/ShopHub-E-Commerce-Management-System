using ShopHub.Interfaces;

namespace ShopHub.Repositories;

public class Repository<T> : IRepository<T>
{
    private readonly List<T> _items = new();

    public void Add(T item)
    {
        _items.Add(item);
    }

    public T? GetById(int id)
    {
        var property = typeof(T)
            .GetProperty("Id");

        if (property == null)
        {
            return default;
        }

        return _items
            .FirstOrDefault(x =>
            (int)property.GetValue(x)! == id);
    }

    public List<T> GetAll()
    {
        return _items.ToList();
    }

    public void Update(T item)
    {
        var property = typeof(T)
            .GetProperty("Id");

        if (property == null)
        {
            return;
        }

        int id = (int)property.GetValue(item)!;

        int index = _items.FindIndex(x =>
            (int)property.GetValue(x)! == id);

        if (index != -1)
        {
            _items[index] = item;
        }
    }

    public void Delete(int id)
    {
        var property = typeof(T)
            .GetProperty("Id");

        if (property == null)
        {
            return;
        }

        T? item = _items
            .FirstOrDefault(x =>
                (int)property.GetValue(x)! == id);

        if (item != null)
        {
            _items.Remove(item);
        }
    }
}