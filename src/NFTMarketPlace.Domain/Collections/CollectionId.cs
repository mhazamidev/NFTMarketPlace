using NFTMarketPlace.Domain.SeedWork;

namespace NFTMarketPlace.Domain.Collections;

public class CollectionId : StronglyTypedId<CollectionId>
{
    public CollectionId(Guid value) : base(value)
    {
    }
}
