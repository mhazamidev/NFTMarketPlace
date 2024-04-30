using NFTMarketPlace.Domain.SeedWork;

namespace NFTMarketPlace.Domain.Holders;

public class HolderId : StronglyTypedId<HolderId>
{
    public HolderId(Guid value) : base(value)
    {
    }
}
