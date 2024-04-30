using NFTMarketPlace.Domain.SeedWork;

namespace NFTMarketPlace.Domain.Wallets;

public class WalletId : StronglyTypedId<WalletId>
{
    public WalletId(Guid value) : base(value)
    {
    }
}
