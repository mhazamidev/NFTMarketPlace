using NFTMarketPlace.Domain.SeedWork;

namespace NFTMarketPlace.Domain.NFTs;

public class NFTId : StronglyTypedId<NFTId>
{
    public NFTId(Guid value) : base(value)
    {
    }
}
