using Bogus;

public class DataSeeder {
    public static IEnumerable<User> GenerateUsers(int count = 10000) {
        var fakeUsers = new Faker<User>()
        .RuleFor(u => u.Name, f => f.Internet.UserName())
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Password, f => "1234")
        .RuleFor(u => u.CreatedAt, f => f.Date.Past());

        return fakeUsers.Generate(count);
    }
    public static IEnumerable<Account> GenerateAccounts(int count = 100000)
    {
        Faker<Account> fakeAccounts = new Faker<Account>()
        .RuleFor(a => a.UserId, f => f.Random.Number(1,10000))
        .RuleFor(a => a.AccountName, f => f.Finance.AccountName())
        .RuleFor(a => a.AccountType, f => f.PickRandom(new[] { "Savings", "Checking", "Business" }))
        .RuleFor(a => a.Balance, f => f.Finance.Amount(0, 10000))
        .RuleFor(a => a.CreatedAt, f => f.Date.Past())
        .RuleFor(a => a.UpdatedAt, f => f.Date.Recent());

        return fakeAccounts.Generate(count);
    }

    public static IEnumerable<Card> GenerateCards(int count = 350000)
    {
        Faker<Card> fakeCards = new Faker<Card>()
        .RuleFor(c => c.UserId, f => f.Random.Number(1,10000))
        .RuleFor(c => c.AccountID, f=>f.Random.Number(1, 100000))
        .RuleFor(c => c.Type, f => f.PickRandom<Card.cardType>())
        .RuleFor(c => c.ExpirationDate, f => DateOnly.FromDateTime(f.Date.Future()))
        .RuleFor(c => c.CVV, f => f.Random.Number(100, 999))
        .RuleFor(c => c.CardNumber, f => f.Finance.CreditCardNumber())
        .RuleFor(c => c.Network, f => f.PickRandom<Card.cardNetwork>())
        .RuleFor(c => c.Contactless, f => f.Random.Bool())
        .RuleFor(c => c.InternationalUsage, f => f.Random.Bool());

        return fakeCards.Generate(count); 
    }
}