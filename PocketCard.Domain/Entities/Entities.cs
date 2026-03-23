public class User
{

    public int Id { get; private set; }
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedAt { get; private set; }
}

public class Account
{
    public int Id { get; private set; }
    public int UserId { get; set; }
    public string AccountName { get; set; } = null!;
    public string AccountType { get; set; } = null!;
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class Card
{
    public int Id { get; private set; }
    public int UserId { get; set; }
    public int AccountID { get; set; }
    public enum cardType { Debit, Credit, Physical, Virtual, Prepaid }
    public cardType Type { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public int CVV { get; set; }
    //Card Status
    public string CardNumber { get; set; } = null!;
    public enum cardNetwork { VISA, MasterCard }
    public cardNetwork Network { get; set; }
    public bool Contactless { get; set; }
    public bool InternationalUsage { get; set; }
}