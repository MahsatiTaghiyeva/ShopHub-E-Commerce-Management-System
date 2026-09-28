namespace ShopHub.Models;

public class ProductRating
{
    private static int _idCounter = 1000;

    public int Id { get; private set; }
    public Product Product { get; set; }
    public Customer Customer { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    public ProductRating(
        Product product,
        Customer customer,
        int score,
        string comment)
    {
        if (score < 1 || score > 5)
        {
            throw new ArgumentException(
                "Score must be between 1 and 5.");
        }

        Id = ++_idCounter;
        Product = product;
        Customer = customer;
        Score = score;
        Comment = comment;
        CreatedAt = DateTime.Now;
    }
}