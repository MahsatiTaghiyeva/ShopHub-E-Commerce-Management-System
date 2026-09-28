namespace ShopHub.Exceptions;

public class InvalidOrderException : Exception
{
    public InvalidOrderException(string message)
        : base(message)
    {
    }
}