namespace ShopHub.Models;

public class Customer
{
    private static int _idCounter = 1000;

    public int Id { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public Customer(
        string firstName,
        string lastName,
        string email,
        string phone)
    {
        Id = ++_idCounter;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        IsDeleted = false;
        CreatedAt = DateTime.Now;
    }
}