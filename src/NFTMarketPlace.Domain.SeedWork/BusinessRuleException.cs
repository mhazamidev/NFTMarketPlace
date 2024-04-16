namespace NFTMarketPlace.Domain.SeedWork;

public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
}
