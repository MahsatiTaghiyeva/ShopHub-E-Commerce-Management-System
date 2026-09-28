namespace ShopHub.Collections;

public class CustomCollection<T>
{
    private T[] _items;

    public int Count { get; private set; }

    public CustomCollection()
    {
        _items = new T[4];
    }

    public void Add(T item)
    {
        if (Count == _items.Length)
        {
            IncreaseCapacity();
        }

        _items[Count] = item;
        Count++;
    }

    public void Remove(T item)
    {
        int index = GetIndex(item);

        if (index == -1)
        {
            return;
        }

        for (int i = index;
             i < Count - 1;
             i++)
        {
            _items[i] = _items[i + 1];
        }

        _items[Count - 1] = default!;
        Count--;
    }

    public T Get(int index)
    {
        if (index < 0 ||
            index >= Count)
        {
            throw new IndexOutOfRangeException(
                "Invalid index.");
        }

        return _items[index];
    }

    public bool Contains(T item)
    {
        return GetIndex(item) != -1;
    }

    public void Clear()
    {
        for (int i = 0; i < Count; i++)
        {
            _items[i] = default!;
        }

        Count = 0;
    }

    private int GetIndex(T item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(
                _items[i],
                item))
            {
                return i;
            }
        }

        return -1;
    }

    private void IncreaseCapacity()
    {
        T[] newItems =
            new T[_items.Length * 2];

        for (int i = 0;
             i < _items.Length;
             i++)
        {
            newItems[i] = _items[i];
        }

        _items = newItems;
    }

    public T this[int index]
    {
        get
        {
            return Get(index);
        }

        set
        {
            if (index < 0 ||
                index >= Count)
            {
                throw new IndexOutOfRangeException(
                    "Invalid index.");
            }

            _items[index] = value;
        }
    }
}